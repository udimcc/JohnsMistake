using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damage;
    [SerializeField] LayerMask hitableLayerMask = new LayerMask();

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((this.hitableLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            Health health = collision.transform.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(this.damage);
            }
        }
    }
}
