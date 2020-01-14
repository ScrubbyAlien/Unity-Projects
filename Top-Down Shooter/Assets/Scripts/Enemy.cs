using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{

    NavMeshAgent pathfinder;
    Transform target;

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath());

    }

    IEnumerator UpdatePath()
    {

        float refreshrate = 0.25f;

        while (target != null)
        {
            Vector3 targetPostion = new Vector3(target.position.x, 0, target.position.z);
            if (!dead)
            {
                pathfinder.SetDestination(targetPostion);
            }
            yield return new WaitForSeconds(refreshrate);
        }
    }
}
