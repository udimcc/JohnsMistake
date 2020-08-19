using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRelativeRotation : MonoBehaviour
{
    public float worldRotationX = 0;
    public float worldRotationY = 0;
    public float worldRotationZ = 0;


    void Update()
    {
        this.transform.rotation = Quaternion.Euler(this.worldRotationX, this.worldRotationY, this.worldRotationZ);
    }
}
