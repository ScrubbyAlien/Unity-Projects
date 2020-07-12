using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{

    private int randX;
    private int randY;
    private float waitTime;

    void Start()
    {
        StartCoroutine(RandomWalking());
    }

    IEnumerator RandomWalking()
    {
        while (true)
        {
            randX = (int)Random.Range(-5, 5);
            randY = (int)Random.Range(-5, 5);
            waitTime = Random.Range(2, 5);

            if (transform.position.y + randY > 5 || transform.position.y + randY < -5)
            {
                randY = 0;
            }

            if (transform.position.x + randX > 8 || transform.position.x + randX < -8)
            {
                randX = 0;
            }

            transform.Translate(new Vector2(randX, randY), Space.World);
            yield return new WaitForSecondsRealtime(waitTime);
        }
    }
}
