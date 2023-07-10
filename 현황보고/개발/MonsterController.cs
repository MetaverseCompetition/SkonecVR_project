using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    private NavMeshAgent ag;
    private Transform playerTr;
    
    private float mSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ag = GetComponent<NavMeshAgent>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ag.SetDestination(playerTr.position);
    }
}
