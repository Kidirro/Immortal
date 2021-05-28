using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Rigidbody2D DoorLeft;
    Vector2 doorLeftClosedPos;
    Vector2 doorLeftOpenedPos;

    public Rigidbody2D DoorRight;
    Vector2 doorRightClosedPos;
    Vector2 doorRightOpenedPos;

    public float OpenSize;

    bool isDoorOpened = false;
    bool isDoorRequest = false;

    float timeLeft;

    int entityInTheZone = 0;

    private void Awake()
    {
        DoorLeft.isKinematic = true;
        doorLeftClosedPos = DoorLeft.transform.position;
        doorLeftOpenedPos = DoorLeft.transform.position - DoorLeft.transform.up.normalized*OpenSize;

        DoorRight.isKinematic = true;
        doorRightClosedPos = DoorRight.transform.position;
        doorRightOpenedPos = DoorRight.transform.position - DoorRight.transform.up.normalized * OpenSize;

        StartCoroutine(SlidingProcess());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            entityInTheZone++;
            Debug.Log(entityInTheZone + " " + isDoorRequest);
            if (entityInTheZone == 1)
            {
                isDoorRequest = true;
            }
        };
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            entityInTheZone--;
            Debug.Log(entityInTheZone + " " + isDoorRequest);
            if (entityInTheZone == 0)
            {
                isDoorRequest = true;
            }
        };
    }

    IEnumerator SlidingProcess()
    {
        while (true)
        {
            if (isDoorRequest)
            {
                isDoorRequest = false;
                if (isDoorOpened)
                {
                    timeLeft = new Vector2((doorRightClosedPos.x) - DoorRight.transform.position.x, (doorRightClosedPos.y) - DoorRight.transform.position.y).magnitude / OpenSize;

                    DoorRight.velocity = DoorRight.transform.up * OpenSize;
                    DoorLeft.velocity = DoorLeft.transform.up * OpenSize;

                    yield return new WaitForSeconds(timeLeft);

                    DoorRight.velocity = Vector2.zero;
                    DoorLeft.velocity = Vector2.zero;

                }
                else
                {
                    timeLeft = new Vector2((doorLeftOpenedPos.x) - DoorLeft.transform.position.x, (doorLeftOpenedPos.y) - DoorLeft.transform.position.y).magnitude / OpenSize;

                    DoorRight.velocity = -DoorRight.transform.up * OpenSize;
                    DoorLeft.velocity = -DoorLeft.transform.up * OpenSize;

                    yield return new WaitForSeconds(timeLeft);

                    DoorRight.velocity = Vector2.zero;
                    DoorLeft.velocity = Vector2.zero;
                }
                isDoorOpened = !isDoorOpened;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
