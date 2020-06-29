using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
{
    private float rotate;
    [Range(0, 200)]
    public float rotationSpeed;

    private float tonguePower;
    [Range(0,100)]
    public float PowerMultiplyer;
    public Rigidbody rb;
    public bool spin;
    public bool movetotarget;
    private bool collisionStop;
    public LineRenderer tongue;
    public State state = State.SPINNING;
    public float movementspeed;
    private float speedrotateset;
    public LayerMask wall;
    public int health;
    public Text healthtext;
    public Text gameover;
    private float timer2;
    public ParticleSystem flames;
    public Renderer bull;
  


    public Color colorStart;
    public Color colorEnd;
    public enum State
    {
        SPINNING,
        READY,
        MOVING
    }

    void Start()
    {
        spin = true;
        movetotarget = false;
        speedrotateset = rotationSpeed;
        gameover.enabled = false;

        var emission = flames.emission;
        emission.rateOverTime = 0f;

    }

    void Update()
    {
        healthtext.text = health.ToString();
        timer2 += Time.deltaTime;
  

        float lerp = (tonguePower / 10);
        bull.material.color = Color.Lerp(colorStart, colorEnd, lerp);

        switch (state)
        {
            case State.SPINNING:
                if (Input.GetKey(KeyCode.Z))
                {
                    
                }
                break;
            case State.READY:
                if (!Input.GetKey(KeyCode.Z))
                {

                }
                break;
        }
        if (Input.GetKey((KeyCode.Z)))
        {
            tonguePower += ((Time.deltaTime * PowerMultiplyer)/8);
            spin = false ;
            movetotarget = true;
            RaycastHit hit;
            rotationSpeed = 0f;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, wall))
            {
                 Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                 tongue.SetPosition(0, transform.position);
                 tongue.SetPosition(1, hit.point);
                 state = State.READY;
            }
        }

        

        if (!Input.GetKey(KeyCode.Z) && (spin == true))
        {
        
            rotationSpeed = speedrotateset;
            rotate += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, rotate * rotationSpeed, 0f);
            tonguePower = 0f;
            var emission = flames.emission;
            emission.rateOverTime = 0f;
        }

        if (health < 1)
        {
            gameover.enabled = true;
            Time.timeScale = 0.25f;
            if (timer2 > 1f)
            {
                SceneManager.LoadScene("StartScreen");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.Z) && movetotarget)
        {
            tongue.SetPosition(0, transform.position);
            state = State.MOVING;
            rb.velocity = rb.transform.forward * (movementspeed + tonguePower);
            collisionStop = false;

            var emission = flames.emission;
            emission.rateOverTime = 100f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "wall") && (collisionStop == false))
        {
            spin = true;
            movetotarget = false;
            collisionStop = true;

            float flip = (transform.rotation.y);
            transform.rotation = Quaternion.Euler(0f, flip, 0f);

            tongue.SetPosition(0, transform.position);
            tongue.SetPosition(1, transform.position);
            state = State.SPINNING;

        }

        if (collision.gameObject.tag == "enemy")
        {
            health -= 1;
            timer2 = 0f;
        }
    }
}
