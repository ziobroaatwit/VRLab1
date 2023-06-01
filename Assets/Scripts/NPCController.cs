using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float speed = 2.0f;
    private Transform playerTransform;
    private Vector3 direction;
    [SerializeField] GameController controller;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Define AI behavior for the characters
    void Update()
    {
        direction = (playerTransform.position - transform.position).normalized;
        transform.position+=direction*speed*Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Cube")
        {
            other.gameObject.SetActive(false);
            controller.updateScore(0);
        }
    }
}
