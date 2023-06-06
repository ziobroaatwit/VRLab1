using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float speed = 2.0f;
    private Transform playerTransform;
    private Vector3 direction;
    [SerializeField] GameController controller;


    public GameObject player;
    public NPCState currentState = NPCState.Patrol;
    private float distanceToPlayer;
    public float attackDistance = 10f;
    public float retreatDistance = 2.5f;
    public enum NPCState
    {
        Patrol,
        Attack,
        Retreat
    }
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Define AI behavior for the characters
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        stateMachine();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Cube")
        {
            other.gameObject.SetActive(false);
            controller.updateScore(0);
        }
    }
    void stateMachine()
    {
        switch (currentState)
        {
            case NPCState.Patrol:
                // Code to move the NPC in a patrol pattern here

                if (distanceToPlayer <= attackDistance)
                {
                    currentState = NPCState.Attack;
                    Debug.Log("Attack State");
                }
                break;

            case NPCState.Attack:
                // Code to make the NPC attack the player here
                direction = (playerTransform.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                if (distanceToPlayer > attackDistance)
                {
                    currentState = NPCState.Patrol;
                    Debug.Log("Patrol State");
                }
                else if (distanceToPlayer <= retreatDistance)
                {
                    currentState = NPCState.Retreat;
                    Debug.Log("Retreat State");
                }
                break;

            case NPCState.Retreat:
                // Code to make the NPC move away from the player here
                direction = (playerTransform.position - transform.position).normalized;
                transform.position += -direction * speed * Time.deltaTime;
                if (distanceToPlayer > retreatDistance)
                {
                    currentState = NPCState.Patrol;
                    Debug.Log("Patrol State");
                }
                break;
        }
    }
}
