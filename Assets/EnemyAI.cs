using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public State state;
    public bool spawing;
    public GameObject Center;
    private Rigidbody rb;
    public float AiMovementSpeed;
    public GameObject player;
    public Transform target;
    public bool runningatplayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        runningatplayer = true;
        
    }

    public enum State
    {
        LEAVINGGATE,
        WANDERING,
        CHASINGPLAYER
    }

    void Update()
    {
        switch (state)
        {
            case State.LEAVINGGATE:
                break;

            case State.WANDERING:
                break;

            case State.CHASINGPLAYER:
                break;

        }

        if (spawing == true)
        {
           rb.velocity =  rb.transform.forward * AiMovementSpeed;
        }

    }

    private void FixedUpdate()
    {
        if (runningatplayer == true)
        {
            transform.LookAt(target);
            rb.velocity = rb.transform.forward * AiMovementSpeed;
        }
    }

    private void OnEnable()
    {
        player = GameObject.Find("Player");
        target = player.transform;
    }

}
