using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponAPI
{
    public string enemyTag;
    public float pushForce = 60;


    GameObject enemy = null;
    float attackFrequency;
    float lastAttackTime;


    void Start()
    {
        this.attackFrequency = 1 / this.attackRate;
    }

    public override void Attack()
    {
        if (this.enemy == null)
        {
            return;
        }

        Health enemyHealth = this.enemy.GetComponent<Health>();

        if (Time.time - this.lastAttackTime < this.attackFrequency)
        {
            return;
        }

        this.lastAttackTime = Time.time;

        StartCoroutine(this.AttackEffects());
        enemyHealth.TakeDamage(this.damage);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(this.enemyTag))
        {
            this.enemy = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(this.enemyTag))
        {
            this.enemy = null;
        }
    }

    IEnumerator AttackEffects()
    {
        this.audioSource.PlayOneShot(audioClip);
        this.animator.SetBool("IsAttacking", true);

        Rigidbody2D enemyRigidbody = this.enemy.GetComponent<Rigidbody2D>();

        if (enemyRigidbody != null)
        {
            enemyRigidbody.AddForce(this.transform.up * this.pushForce);
        }

        yield return new WaitForSeconds(0.73f);

        this.animator.SetBool("IsAttacking", false);
    }
}
