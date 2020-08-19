using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float speed;
    LayerMask hitableLayerMask;
    bool initialized = false;

    Rigidbody2D rb;

    void Awake()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    public void Initialize(float damage, float speed, LayerMask hitableLayerMask)
    {
        this.damage = damage;
        this.speed = speed;
        this.hitableLayerMask = hitableLayerMask;
        this.initialized = true;

        this.rb.AddForce(this.transform.up * this.speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.initialized)
        {
            Debug.LogError("Bullet not initialized");
            return;
        }

        if ((this.hitableLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            Health health = collision.transform.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(this.damage);
            }
        }

        Destroy(this.gameObject);
    }
}
