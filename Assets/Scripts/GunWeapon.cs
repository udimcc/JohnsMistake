using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : WeaponAPI
{
    public float pushForce = 60;
    public Transform gunEndPoint;
    public GameObject gunFire;

    [SerializeField] LayerMask hitableLayerMask = new LayerMask();
    LineRenderer lineOfShot;
    float lastFireTime;
    float fireFrequency;

    protected override void Awake()
    {
        base.Awake();
        this.lineOfShot = this.GetComponent<LineRenderer>();
    }

    void Start()
    {
        this.fireFrequency = 1 / this.attackRate;
        this.lineOfShot.useWorldSpace = true;
        this.lineOfShot.enabled = false;
        this.fireLight.SetActive(false);
    }

    void Update()
    {
        RaycastHit2D hit = this.GetWeaponHit();
        this.lineOfShot.SetPosition(0, this.gunEndPoint.transform.position);

        if (hit.collider != null)
        {
            this.lineOfShot.SetPosition(1, hit.point);
        }
        else
        {
            this.lineOfShot.SetPosition(1, this.transform.position + this.transform.up * 100);
        }
    }

    RaycastHit2D GetWeaponHit()
    {
        Vector2 gunEndPos = this.gunEndPoint.transform.position;
        return Physics2D.Raycast(gunEndPos, this.transform.up, float.PositiveInfinity, this.hitableLayerMask);
    }

    public override void Attack()
    {
        if (Time.time - this.lastFireTime < this.fireFrequency)
        {
            return;
        }

        this.lastFireTime = Time.time;

        StartCoroutine(ShootEffects());

        RaycastHit2D raycast = this.GetWeaponHit();

        if (raycast.collider != null)
        {
            Health health = raycast.transform.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(this.damage);
            }

            if (raycast.rigidbody != null)
            {
                raycast.rigidbody.AddForce(this.transform.up * this.pushForce);
            }
        }
    }

    IEnumerator ShootEffects()
    {
        GameObject gunFireInstance = Instantiate(this.gunFire, this.gunEndPoint);
        this.audioSource.PlayOneShot(audioClip);
        this.lineOfShot.enabled = true;
        this.animator.SetBool("IsAttacking", true);
        this.fireLight.SetActive(true);

        yield return new WaitForSeconds(0.14f);

        this.lineOfShot.enabled = false;
        this.animator.SetBool("IsAttacking", false);
        Destroy(gunFireInstance);
        this.fireLight.SetActive(false);
    }
}
