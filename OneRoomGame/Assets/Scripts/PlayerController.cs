using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public UnityEngine.AI.NavMeshAgent agent;

    public float runSpeed;
    public float walkSpeed;

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
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            clicked++;

            if (clicked == 1)
            {
                clickTime = Time.time;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.speed = walkSpeed;

                if (clicked > 1 && Time.time - clickDelay < clickTime)
                {
                    agent.speed = runSpeed;
                    //clicked = 0;
                    //clickTime = 0;
                }
                agent.SetDestination(hit.point);
            }
        }
    }
}
