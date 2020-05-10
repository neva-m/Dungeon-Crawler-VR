using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    private NavMeshAgent navMesh;

    public GameObject player;
    //followed tutorial: https://medium.com/otavio-henrique/simple-enemy-ai-system-for-unity3d-ed22f389974c

    private Vector3 st_point;
    private Vector3 end_point;
    public Transform st_pos;
    public Transform end_pos;

    private Vector3 player_last_seen_position;
    private Vector3 destination;
    public Vector3 m3_destination;//used in mode 3;
    public float playerDist;
    public float destDist;


    public int mode;
    public float speed;
    public bool freeze;
    public float freeze_timer = 0f;

    private float timer;
    private int perceptionDist = 15;
    private int chaseDist = 5;
    private float half_field_of_view_angle = 55;


    /*mode: 
     * 0: unaware: enemy hasn't spotted player. Enemy only detect forward. 
     * 
     * transition: 0 -> 1: enemy saw player. But player isn't in sight or is too far away right now. 
     * 1: suspicious: change direction and start walking towards last direction he saw the player.
     * 
     * transition: 0 or 1 -> 2: enemy sees player and player within chase distance
     * 2: hostile: enemy chase player.
     * 
     * 2-> 1: players is out of chase distance, or 20 sec passed. 
     * 1-> 0: deosn't see player, 20 sec passed. 
     * 
     * 3: other enemy nearby spotted the player, go to the enemy and help. 
     * 3-> 0: reached m3_destination and still have not spotted player
     * 3-> 1 or 2 : same transition as 0 -> 1 or 2
     * x -> 3: controlled by parent script
     */


    // Use this for initialization
    void Start()
    {
        st_point = st_pos.position;
        end_point = end_pos.position;
        mode = 0;
        timer = 0;
        speed = 1f;
        freeze = false;
        freeze_timer = 0f;
        destination = end_point;
        navMesh = transform.GetComponent<NavMeshAgent>();
        //Debug.Log(navMesh.destination);
    }

    void move()
    {
        //Debug.Log("mode: " + mode + "; speed: " + navMesh.speed);
        destDist = Vector3.Distance(destination, transform.position);
        if (destDist < 2)
        {
            if (Vector3.Distance(end_point, transform.position) < 2)
            {
                destination = st_point;
            }
            else
            {
                destination = end_point;
            }
        }
        
        if (mode == 0)
        {
            navMesh.speed = speed;
            navMesh.destination = destination;
        }
        else if (mode == 1)
        {
            navMesh.speed = speed;
            navMesh.destination = player_last_seen_position;
        }
        else if (mode == 2)
        {
            navMesh.speed = speed;
            navMesh.acceleration = 0.2f;
            navMesh.destination = player.transform.position;
        }
        else
        {
            navMesh.speed = speed;
            navMesh.destination = m3_destination;
        }
        GetComponent<BulletAttack>().attack_mode = mode;
        //Debug.Log("destination:" + navMesh.destination  + ", end_point" + end_point + ", last_seen: " + player_last_seen_position);
    }

    bool player_spotted()
    {
        RaycastHit hit;
        playerDist = Vector3.Distance(player.transform.position, transform.position);
        Vector3 playerDirection = player.transform.position - transform.position;
        float playerAngle = Vector3.Angle(playerDirection, transform.forward);
        if (playerAngle < half_field_of_view_angle && playerDist < perceptionDist)
        {
            //Debug.Log("player within view");
            //Vector3 halfHeight = new Vector3(0, transform.position.y / 2, 0);
            if (Physics.Raycast(transform.position, playerDirection.normalized, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    //Debug.Log("hit");
                    player_last_seen_position = player.transform.position;
                    if (mode == 0)
                    {
                        transform.parent.GetComponent<EnemiesAI>().child_update_player_spotted(transform.position);
                    }
                    return true;
                }
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        playerDist = Vector3.Distance(player.transform.position, transform.position);
        if (mode == 0 || mode == 3)
        {
            timer += Time.deltaTime;
            if (player_spotted())
            {
                if (playerDist > chaseDist)
                {
                    mode = 2;
                }
                else
                {
                    mode = 1;
                }
                timer = 0;
            }
        }
        else if (mode == 1)
        {
            timer += Time.deltaTime;
            if (player_spotted() && playerDist <= chaseDist)
            {
                mode = 2;
                timer = 0;
            }
            if (timer >= 20)
            {
                mode = 0;
                timer = 0;
            }
        }
        else if (mode == 2)
        {
            //FireBullet
            //Debug.Log("mode" + mode + "; attack: " + GetComponent<BulletAttack>().attack_mode);
            timer += Time.deltaTime;
            if (playerDist < 2)
            {
                //close to player, change to attack later. 
                mode = 0;
                timer = 0;
            }
            else if (timer >= 20 || playerDist > chaseDist)
            {
                player_last_seen_position = player.transform.position;
                mode = 1;
                timer = 0;
            }
        }

        

        if (speed < 1f)
        {
            Debug.Log("freeze");
            Debug.Log("speed: " + speed.ToString());

            GetComponent<BulletAttack>().fireRate = 1.5f;
            
            speed = speed + (Time.deltaTime/10);
            navMesh.speed = speed;
            
            freeze_timer += Time.deltaTime;
            if (freeze_timer > 10) {
                freeze = false;
                freeze_timer = 0f;
                
            }
        }
        else {
            move();
        }
        
    }

    public void incSpeed(float sec)
    {
        speed += .01f;
        //yield return new WaitForSeconds(sec);
    }
}
