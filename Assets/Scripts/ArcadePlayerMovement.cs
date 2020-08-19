using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadePlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float jumpForce = 30f;
    public float gravity = -9.81f;
    public GameObject groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    CharacterController controller;
    public Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        this.controller = this.GetComponent<CharacterController>();
        this.groundCheck = new GameObject("GroundCheck");
        this.groundCheck.transform.parent = this.transform;
        this.groundCheck.transform.localPosition = new Vector3(0, -(this.controller.height / 2) + (this.groundDistance / 2), 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.isGrounded = Physics.CheckSphere(this.groundCheck.transform.position, this.groundDistance, this.groundMask);
        
        Vector3 movement = this.transform.right * Input.GetAxis("Horizontal") +
                           this.transform.forward * Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && (this.isGrounded))
        {
            this.velocity.y += Mathf.Sqrt(this.jumpForce * -2f * this.gravity);
        }

        velocity.y += this.gravity * Time.deltaTime;

        if ((this.isGrounded) && (velocity.y < 0))
        {
            this.velocity.y = 0f;
        }

        this.controller.Move(movement * this.speed * Time.deltaTime);
        this.controller.Move(velocity * Time.deltaTime);
    }
}
