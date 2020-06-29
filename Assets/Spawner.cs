using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private float timer;
    public float wavedelay;
    public GameObject door;
    public GameObject enemy;
    private float enemynumber;
    private float totalenemy;
    public Text wavecounter;
    private float wave;
    public bool opendoor;

    // Start is called before the first frame update
    void Start()
    {
        totalenemy = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        wavecounter.text = wave.ToString();
        timer += Time.deltaTime;
        if (timer > wavedelay)
        {
            timer = 0f;
            opendoor = true;
            totalenemy += 3f;
            enemynumber = totalenemy;
            wave += 1f;

            while (enemynumber > 0f)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
                enemynumber -= 1f;
            }

        }

        if (opendoor == true)
        {
            door.transform.position = new Vector3(0f, 1.7f, 0f);

            if (timer > 2f)
            {
                door.transform.position = new Vector3(0f, 0f, 0f);
                opendoor = false;
            }
        }
    }
}
