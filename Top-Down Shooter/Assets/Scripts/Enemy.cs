using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{

    public enum State { Idle, Attacking, Chasing };
    State currentState;

    NavMeshAgent pathfinder;
    Transform target;
    Material skinMaterial;

    Color originalColor;

    float attackDistanceThreshold = 0.5f;
    float timeBetweenAttacks = 1;
    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());
    }

    public void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqtDstToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqtDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }

    }

    IEnumerator Attack()
    {
        pathfinder.enabled = false;
        currentState = State.Attacking;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

        float attackSpeed = 2f;

        skinMaterial.color = Color.red;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);


            yield return null;
        }

        skinMaterial.color = originalColor;

        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {

        float refreshrate = 0.25f;

        while (target != null)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPostion = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                if (!dead)
                {
                    pathfinder.SetDestination(targetPostion);
                }
            }
            yield return new WaitForSeconds(refreshrate);
        }
    }
}
