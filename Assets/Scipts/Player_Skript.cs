using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skript : MonoBehaviour
{

    bool Running = false;
    bool CanShoot=true;

    [Tooltip("Скорость ходьбы")]
    public float WalkingSpeed;
    [Tooltip("Скорость бега")]
    public float RunningSpeed;

    [Tooltip("Скорость стрельбы")]
    public float ShootRate;

    HashSet<GameObject> obj = new HashSet<GameObject>();

    public GameObject Bullet;

    Rigidbody2D rb2D;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation_z = Mathf.Acos(difference.y/Mathf.Sqrt(Mathf.Pow(difference.x,2)+ Mathf.Pow(difference.y, 2))) / Mathf.PI *180;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z*Mathf.Sign(-difference.x));


        Running = Input.GetKey(KeyCode.LeftShift);
        rb2D.velocity = (Running) ? new Vector2(Input.GetAxis("Horizontal") * RunningSpeed, Input.GetAxis("Vertical") * RunningSpeed) : new Vector2(Input.GetAxis("Horizontal") * WalkingSpeed, Input.GetAxis("Vertical") * WalkingSpeed);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shooting();
        }

    }


    void Shooting()
    {
        if (CanShoot)
        {
            CanShoot = false;
            //Some code...
            StartCoroutine(Shooting_rate());
        }
    }

    IEnumerator Shooting_rate()
    {
        yield return new WaitForSeconds(ShootRate);
        var bul = Instantiate(Bullet);
        bul.transform.position = transform.position;
        bul.GetComponent<Rigidbody2D>().AddForce(transform.up * 10, ForceMode2D.Impulse);
        CanShoot = true;

    }
}
