using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRockDestruction : MonoBehaviour, Killable
{
    public GameObject rockParts;
    public float destructionForce;

    public void OnDeath()
    {
        GameObject rockPartsIns = Instantiate(this.rockParts, this.transform.position, this.transform.rotation);
        rockPartsIns.transform.localScale = this.transform.localScale;

        foreach (Transform rockPart in rockPartsIns.transform)
        {
            Rigidbody2D rb = rockPart.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(rockPart.localPosition.normalized * this.destructionForce, ForceMode2D.Impulse);
            }
        }
        
        Destroy(this.gameObject);
    }
}
