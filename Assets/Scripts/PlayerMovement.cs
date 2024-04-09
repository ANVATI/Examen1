using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10f;

    [Header("Raycast Properties")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private Color rayDebugColor = Color.red;

    private Vector3 movement;
    public Rigidbody myRB;
    private bool canJump;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //movement = new Vector3(Input.GetAxisRaw("Horizontal"), myRB.velocity.y, Input.GetAxisRaw("Vertical"));

        canJump = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayers);

        Debug.DrawRay(transform.position, Vector3.down * rayDistance, rayDebugColor);

    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movement = context.ReadValue<Vector3>();
            myRB.velocity = Vector3.Scale(movement, new Vector3(speed, 1, speed));
        }
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canJump)
            {
                myRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
    
    private void FixedUpdate()
    {
        //myRB.velocity = Vector3.Scale(movement, new Vector3(speed, 1, speed));
    /*
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            myRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    */
    }

}
