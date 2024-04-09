using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10f;

    [Header("Raycast Properties")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private Color rayDebugColor = Color.red;

    private Vector3 movement;
    private Rigidbody myRB;
    private bool canJump;
    private int currentPlayerIndex = 0;
    private GameObject[] players;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        players = GameObject.FindGameObjectsWithTag("Player");
        SetActivePlayer(currentPlayerIndex);
    }

    void Update()
    {
        canJump = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayers);
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, rayDebugColor);

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            SwitchPlayer(-1);
        }
        else if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            SwitchPlayer(1);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movement = context.ReadValue<Vector2>();
            myRB.velocity = new Vector3(movement.x * speed, myRB.velocity.y, movement.y * speed);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            myRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void SwitchPlayer(int direction)
    {
        players[currentPlayerIndex].SetActive(false);
        currentPlayerIndex = (currentPlayerIndex + direction + players.Length) % players.Length;
        SetActivePlayer(currentPlayerIndex);
    }

    private void SetActivePlayer(int index)
    {
        players[index].SetActive(true);
        myRB = players[index].GetComponent<Rigidbody>();
    }
}
