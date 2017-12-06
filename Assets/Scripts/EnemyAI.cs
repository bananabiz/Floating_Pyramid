using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private GameObject endPoint;
    private NavMeshAgent nav;

    // Use this for initialization
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (endPoint != null)
        {
            nav.SetDestination(endPoint.transform.position);
        }
        else
        {
            Debug.LogError("Set Your EndPoint Prefab!");
        }
        if (!nav.pathPending)
        {
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                {
                    Destroy(gameObject);
                    
                }
            }
        }

    }
}
