using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody myRBD;
    [SerializeField] private float velocityModifier = 5.0f;
    [SerializeField] private int damage = 10;
    private Transform currentPositionTarget;
    public PlayerMovement player;
    private int patrolPos = 0;

    private void Start()
    {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update()
    {
        CheckNewPoint();
    }

    void ReturnDamage()
    {
        
    }

    private void CheckNewPoint()
    {
        if (Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25)
        {
            patrolPos = (patrolPos + 1) % checkpointsPatrol.Length;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
        }
    }
}
