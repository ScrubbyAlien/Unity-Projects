using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gubbe : MonoBehaviour
{
    private Animator mAnimator;
    public GameObject pistol;
    bool holding;
    public float turnDistance;
    public float turnSpeed;

    void Start()
    {
        pistol.gameObject.SetActive(holding);
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            holding = !holding;
            mAnimator.SetBool("holding", holding);
            pistol.gameObject.SetActive(holding);
            if (holding)
            {
                StartCoroutine("turn", 1);
            }
            else
            {
                StartCoroutine("turn", -1);
            }
        }
    }

    IEnumerator turn(int turnDirection)
    {
        float turnAmountInDeg = (turnDistance - transform.eulerAngles.y) * turnDirection;
        float originalRotation = transform.eulerAngles.y;
        Vector3 target = new Vector3(0, originalRotation + turnAmountInDeg, 0);
        while (transform.eulerAngles.y / Mathf.Abs(target.y) >= 0.05f)
        {
            transform.eulerAngles += Vector3.RotateTowards(transform.eulerAngles, target, turnAmountInDeg * Mathf.Deg2Rad, turnSpeed * Time.deltaTime);
        }
    }
}
