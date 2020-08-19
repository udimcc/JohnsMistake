using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    Interactable lastInteractable = null;


    void Update()
    {
        if (PauseMenu.isGamePaused)
        {
            return;
        }

        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit))
        {
            Interactable interactable = hit.transform.gameObject.GetComponent<Interactable>();

            if ((interactable != null) && (interactable.canPlayerInteract))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.OnInteract();
                }
                else if (this.lastInteractable != interactable)
                {
                    interactable.OnHoverStart();
                }

                this.lastInteractable = interactable;
            }
            else if (this.lastInteractable != null)
            {
                this.lastInteractable.OnHoverStop();
                this.lastInteractable = null;
            }
        }
    }
}
