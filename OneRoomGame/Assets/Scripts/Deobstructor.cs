using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deobstructor : MonoBehaviour
{
    public Transform ObstructingWalls;

    public float downScale;
    Vector3 originalScale;

    bool wallsDown;

    void Start()
    {
        wallsDown = false;
        originalScale = ObstructingWalls.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            if (wallsDown)
            {
                ObstructingWalls.localScale = originalScale;
                wallsDown = false;
            }
            else
            {
                ObstructingWalls.localScale = new Vector3(originalScale.x, downScale, originalScale.z);
                wallsDown = true;
            }
        }
    }
}
