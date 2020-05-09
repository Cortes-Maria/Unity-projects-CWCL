using System.Collections;
using System.Collections.Generic;
// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{

    public Transform goal;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        goal = player.transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }
}
