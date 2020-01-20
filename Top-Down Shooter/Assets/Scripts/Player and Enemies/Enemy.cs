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
    LivingEntity targetEntity;
    Material skinMaterial;

    Color originalColor;

    float attackDistanceThreshold = 1f;
    float timeBetweenAttacks = 1;
    float nextAttackTime;
    float damage = 1;

    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            currentState = State.Chasing;
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(UpdatePath());
        }

    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    public void Update()
    {
        if (hasTarget)
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

    }

    IEnumerator Attack()
    {
        pathfinder.enabled = false;
        currentState = State.Attacking;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

        float attackSpeed = 1.7f;

        skinMaterial.color = Color.red;

        bool hasAppliedDamage = false;
        bool isCollidingWithTarget = false;

        float percent = 0;
        while (percent <= 1)
        {
            if (hasTarget && (target.position - transform.position).magnitude < myCollisionRadius + targetCollisionRadius)
            {
                isCollidingWithTarget = true;
            }

            if (isCollidingWithTarget && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }

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

        while (hasTarget)
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
