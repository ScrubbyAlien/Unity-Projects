using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    bool IsPicked;
    public Rigidbody2D mRigidbody;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mRigidbody.gravityScale = 1;
            IsPicked = false;
        }

        if (IsPicked)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }

    void OnMouseDown()
    {
        mRigidbody.gravityScale = 0;
        IsPicked = true;
    }
}
