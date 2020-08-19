using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour, Killable
{
    public void OnDeath()
    {
        Destroy(this.gameObject);
    }
}
