using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsWeapon : WeaponAPI
{
    public float bulletSpeed;
    public Transform gunEndPoint;
    public GameObject gunFire;
    public GameObject bullet;
    public float shootTime;

    [SerializeField] LayerMask hitableLayerMask = new LayerMask();
    float lastFireTime;
    float fireFrequency;


    void Start()
    {
        this.fireFrequency = 1 / this.attackRate;
    }

    public override void Attack()
    {
        if (Time.time - this.lastFireTime < this.fireFrequency)
        {
            return;
        }

        this.lastFireTime = Time.time;

        
        StartCoroutine(this.ShootEffects());

        GameObject bullet = Instantiate(this.bullet, this.gunEndPoint.position, this.gunEndPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript == null)
        {
            Debug.LogError("Bullet doesn't have bullet script");
            return;
        }

        bulletScript.Initialize(this.damage, this.bulletSpeed, this.hitableLayerMask);
        Destroy(bullet, 10);
    }

    IEnumerator ShootEffects()
    {
        GameObject gunFireInstance = Instantiate(this.gunFire, this.gunEndPoint);
        this.audioSource.PlayOneShot(this.audioClip);
        this.animator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(shootTime);

        this.animator.SetBool("IsAttacking", false);
        Destroy(gunFireInstance);
    }
}
