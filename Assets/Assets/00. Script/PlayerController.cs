using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController ch;

    public float speed = 5f;
    public float gravity = -9.8f; 
    //public float jumpHeight = 1f;

    private Vector3 velocity;

    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;

    private void Start()
    {
        ch = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (ch.isGrounded)
            velocity.y = 0f;

        // 점프
        //if(Input.GetKeyDown(KeyCode.Space) && ch.isGrounded)
        //    velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        moveDir = transform.right * moveX + transform.forward * moveZ;

        ch.Move(moveDir * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        ch.Move(velocity * Time.deltaTime);
    }


}
