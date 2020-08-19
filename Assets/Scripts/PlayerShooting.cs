using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerShooting : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float shakeAmptitudeGain = 0.7f;
    public float shakeTime = 0.1f;

    CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    GunWeapon weapon;
    bool shakeCamera = false;
    float shakeStartTime;

    // Start is called before the first frame update
    void Awake()
    {
        this.virtualCameraNoise = this.virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        this.weapon = this.GetComponent<GunWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            this.weapon.Attack();
            this.shakeCamera = true;
            this.shakeStartTime = Time.time;
        }

        if (this.shakeCamera)
        {
            if (Time.time - this.shakeStartTime > this.shakeTime)
            {
                this.shakeCamera = false;
                this.virtualCameraNoise.m_AmplitudeGain = 0;
            }
            else
            {
                this.virtualCameraNoise.m_AmplitudeGain = this.shakeAmptitudeGain;
            }
        }
    }
}
