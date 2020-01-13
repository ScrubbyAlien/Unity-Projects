using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public LayerMask collisionMask;
    public float speed = 10;

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hitInfo);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        print(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);
    }
}
