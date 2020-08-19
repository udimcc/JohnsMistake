using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public WeaponAPI[] weapons;
    public GameObject turretPlatform;
    public GameObject cannon;

    GameObject player;
    bool isPlayerInTigger;
    LayerMask hitableLayerMask;
    Animator animator;


    private void Awake()
    {
        this.animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        this.hitableLayerMask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Obstacle"));
    }

    private void Update()
    {
        bool canSeePlayer = this.CanSeePlayer();
        this.animator.SetBool("CanSeePlayer", canSeePlayer);

        if (canSeePlayer)
        {
            Vector3 dir = this.player.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            this.turretPlatform.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (this.IsAimingOnPlayer())
            {
                foreach (WeaponAPI w in this.weapons)
                {
                    w.Attack();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == this.player)
        {
            this.isPlayerInTigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == this.player)
        {
            this.isPlayerInTigger = false;
        }
    }

    bool CanSeePlayer()
    {
        if (!this.isPlayerInTigger)
        {
            return false;
        }

        Vector2 dir = this.player.transform.position - this.transform.position;
        RaycastHit2D raycast = Physics2D.Raycast(this.transform.position, dir, float.PositiveInfinity, this.hitableLayerMask);

        if (raycast.collider == null)
        {
            return false;
        }

        return raycast.collider.gameObject == this.player;
    }

    bool IsAimingOnPlayer()
    {
        RaycastHit2D raycast = Physics2D.Raycast(this.transform.position, this.cannon.transform.up, float.PositiveInfinity, this.hitableLayerMask);

        if (raycast.collider == null)
        {
            return false;
        }

        return raycast.collider.gameObject == this.player;
    }
}
