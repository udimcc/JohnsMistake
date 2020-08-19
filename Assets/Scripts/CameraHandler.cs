using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCameraPlayer;
    public CinemachineVirtualCamera virtualCameraSpectator;
    public GameObject player;

    PlayerMovement playerMovement;
    PlayerShooting playerShooting;


    void Start()
    {
        this.playerMovement = this.player.GetComponent<PlayerMovement>();    
        this.playerShooting = this.player.GetComponent<PlayerShooting>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isGamePaused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            this.virtualCameraPlayer.gameObject.SetActive(this.virtualCameraSpectator.gameObject.activeSelf);
            this.virtualCameraSpectator.gameObject.SetActive(!this.virtualCameraPlayer.gameObject.activeSelf);

            this.playerMovement.enabled = !this.playerMovement.enabled;
            this.playerShooting.enabled = !this.playerShooting.enabled;
        }
    }
}
