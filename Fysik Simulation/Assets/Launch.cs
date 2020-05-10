using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launch : MonoBehaviour
{
    public float intialXVelocity;
    public GameObject inputField;

    float intialYVelocity;
    Vector2 intialVelocity;
    Rigidbody2D mRigidbody;

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            intialYVelocity = float.Parse(inputField.GetComponent<Text>().text);
            intialVelocity = new Vector2(intialXVelocity, intialYVelocity);

            mRigidbody.AddForce(intialVelocity, ForceMode2D.Impulse);

        }
    }
}
