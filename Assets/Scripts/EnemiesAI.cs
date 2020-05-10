using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesAI : MonoBehaviour {
    public bool player_spotted;
    public float timer;
    public float help_dist = 25;
    void Start()
    {
        timer = 0;
        player_spotted = false;
    }
    public void child_update_player_spotted(Vector3 child_position)
    {
        player_spotted = true;
        foreach (Transform child in transform)
        {
            float dist = Vector3.Distance(child_position, child.transform.position);
            if (dist > 1 && dist < help_dist)
            {
                //make sure it's not the child that called this function
                //is other child that's within help_dist
                child.GetComponent<EnemyAI>().mode = 3;
                child.GetComponent<EnemyAI>().m3_destination = child_position;
            }
        }
    }

    void reset_mode()
    {
        foreach (Transform child in transform)
        {
            int child_mode = child.GetComponent<EnemyAI>().mode;
            if (child_mode == 3)
            {
                child.GetComponent<EnemyAI>().mode = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player_spotted)
        {
            timer += Time.deltaTime;
        }
        if (timer > 120)
        {
            player_spotted = false;
            reset_mode();
            timer = 0;
        }
    }
}
