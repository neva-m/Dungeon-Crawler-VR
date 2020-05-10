using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour {
    //followed tutorial: https://www.youtube.com/watch?v=FD9HZB0Jn1w
    public GameObject emitter;
    public GameObject bullet;
    public float forward_force;
    public int attack_mode;

    public float fireRate = 0.5f;//fire bullet every n seconds.
    public int damage;
    private float timer = 0;


    // Use this for initialization
    void Start()
    {
        attack_mode = 0;
    }

    void fire() {
        GameObject temp_bullet_handler;
        temp_bullet_handler = Instantiate(bullet, emitter.transform.position, emitter.transform.rotation) as GameObject;

        //temp_bullet_handler.transform.Rotate(Vector3.left*90);

        Rigidbody temp_rb;
        temp_rb = temp_bullet_handler.GetComponent<Rigidbody>();

        temp_rb.AddForce(transform.forward * forward_force);
        Destroy(temp_bullet_handler, 5.0f);
    }

    void fire_multiple() {
        if (attack_mode == 2)
        {
            fire();
            fire();
        }
        else {
            fire();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate > .5f)
        {
            fireRate -= (Time.deltaTime/10);
        }

        timer += Time.deltaTime;
        if (attack_mode > 0) {
            if (timer >= fireRate) {
                fire_multiple();
                timer = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerStatus>())
        {
            other.GetComponent<PlayerStatus>().hit(damage);

            //Debug.Log("health :" + state.health.ToString());

        }
    }
}
