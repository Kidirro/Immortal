using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float ClosingVelocity;

    float angleDefault;
    Rigidbody2D rb2D;

    bool doorOpenEnable = false;
    bool doorOnStartPos = true;

    public bool DoorClosed
    {
        get { return _doorClosed; }
        set
        {
            rb2D.velocity = Vector2.zero;
            rb2D.angularVelocity = 0;
            rb2D.isKinematic = value;
            _doorClosed = value;
        }
    }

    private bool _doorClosed;

    // Start is called before the first frame update
    void Start()
    {
        angleDefault = transform.eulerAngles.z;
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_doorClosed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DoorClosed = true;
            }
        }
        else {

            float currentAngle = (transform.eulerAngles.z > 180) ? transform.eulerAngles.z - 360 : transform.eulerAngles.z;
            doorOnStartPos = Mathf.Abs(angleDefault - currentAngle) < 10;
            if (Input.GetKeyDown(KeyCode.E) & doorOpenEnable)
            {
                if (doorOnStartPos)
                {
                    rb2D.velocity = (transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).normalized * 10;
                }
                else
                {
                    rb2D.angularVelocity = (angleDefault - currentAngle) * 5;
                }
            }
            else if (doorOnStartPos) {
                rb2D.angularVelocity = (angleDefault - currentAngle) * 2;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            doorOpenEnable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            doorOpenEnable = false;
        }
    }

}
