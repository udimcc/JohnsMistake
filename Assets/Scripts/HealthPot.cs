using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : MonoBehaviour
{
    public float healthAmount;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return;
        }

        Health health = other.transform.GetComponent<Health>();

        if (health == null)
        {
            Debug.LogError("Player doesn't have health script");
            return;
        }

        health.GainHealth(this.healthAmount);

        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
