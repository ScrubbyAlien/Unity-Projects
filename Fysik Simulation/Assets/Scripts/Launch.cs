using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launch : MonoBehaviour
{
    public GameObject inputFieldY;
    public GameObject inputFieldX;
    public GameObject timeText;
    public GameObject distanceText;

    public Collider2D floorColl;
    public Transform floorT;

    Vector2 initialPosition;
    Vector2 initialVelocity;

    bool hasLaunched;

    float initialXVelocity;
    float initialYVelocity;

    Rigidbody2D mRigidbody;
    Collider2D mCollider;

    float startTime;

    void Start()
    {
        transform.position = Vector2.zero;
        mRigidbody = GetComponent<Rigidbody2D>();
        mCollider = GetComponent<Collider2D>();
    }

    public void Launch_()
    {
        if (!hasLaunched)
        {
            if (IsFloat(inputFieldX.GetComponent<Text>().text) && IsFloat(inputFieldY.GetComponent<Text>().text))
            {
                hasLaunched = true;

                initialYVelocity = float.Parse(inputFieldY.GetComponent<Text>().text);
                initialXVelocity = float.Parse(inputFieldX.GetComponent<Text>().text);
                initialVelocity = new Vector2(initialXVelocity, initialYVelocity);

                mRigidbody.AddForce(initialVelocity, ForceMode2D.Impulse);
                StartCoroutine(TimerDistanceCounter());
            }
        }
    }

    public void Reset()
    {
        transform.position = Vector2.zero;
        transform.eulerAngles = Vector2.zero;

        floorT.position = new Vector2(0, -2.5f);

        mRigidbody.velocity = Vector2.zero;
        timeText.GetComponent<Text>().text = "Time: ";
        distanceText.GetComponent<Text>().text = "Distance: ";
    }

    IEnumerator TimerDistanceCounter()
    {
        initialPosition = transform.position;
        startTime = Time.time;
        bool seenLaunched = false;

        while (true)
        {
            if (!mCollider.IsTouching(floorColl))
            {
                seenLaunched = true;
                timeText.GetComponent<Text>().text = "Time: " + Mathf.Round((Time.time - startTime) * 100) / 100;
                distanceText.GetComponent<Text>().text = "Distance: " + Mathf.Round((transform.position.x - initialPosition.x) * 100) / 100;
            }
            else if (seenLaunched)
            {
                hasLaunched = false;
                break;
            }
            yield return null;
        }
    }

    public bool IsFloat(string value)
    {
        float floatValue;
        return float.TryParse(value, out floatValue);
    }
}
