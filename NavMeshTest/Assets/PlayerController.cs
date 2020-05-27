using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    private int clicked;
    private float clickTime;
    private float clickDelay;
    private bool coroutineAllowed;
    private bool doubleClicked;

    void Start()
    {
        clicked = 0;
        clickTime = 0;
        clickDelay = 0.5f;
    }

    void Update()
    {
        if (clicked > 0)
        {
            Debug.Log(clicked);
            if (Time.time - clickDelay >= clickTime)
            {
                clicked = 0;
                clickTime = 0;
                Debug.Log("reset");
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("knapp nere");
            clicked++;

            if (clicked == 1)
            {
                clickTime = Time.time;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("raycast hit");
                agent.speed = 1.5f;

                if (clicked > 1 && Time.time - clickDelay < clickTime)
                {
                    Debug.Log("dubbel");
                    agent.speed = 3;
                    //clicked = 0;
                    //clickTime = 0;
                }
                agent.SetDestination(hit.point);
            }
        }
    }
}
