using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Elements
{
    public enum Type { Normal, Fire, Ice };
}

public class ArrowScript : MonoBehaviour
{
    public int damage;
    public Rigidbody arrowRB;
    private bool isColliding = false;
    public string type;
    private int arrowType;
    private EnemyAI enemyai;

    private AudioSource arrowImpactAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        //GetComponent<Rigidbody>().useGravity = false;
        //if (isColliding) return;
        //isColliding = true;
        arrowRB.constraints = RigidbodyConstraints.FreezeAll;
        //GetComponent<Collider>().isTrigger = false;
        Debug.Log("hit: " + other.gameObject.name);

        if (string.Equals(other.gameObject.tag, "Bow") || string.Equals(other.gameObject.tag, "Player") || string.Equals(other.gameObject.name, "LeftHandAnchor"))
        {
            //transform.parent = null;
            //isColliding = false;
            return;
        }
        
        if (other.GetComponent<EnemyState>())
        {
            arrowImpactAudioSource.Play();
            // getting enemy script
            // Applying arrow effects
            if (arrowType == (int) Elements.Type.Ice && other.GetComponent<EnemyState>().demonType != (int)Elements.Type.Ice)
            {

                enemyai = other.GetComponent<EnemyAI>();
                if (enemyai)
                {
                    enemyai.freeze = true;
                    enemyai.freeze_timer = 0f;
                    enemyai.speed = 0f;
                }
                

            }
            else if (arrowType == (int)Elements.Type.Fire)
            {
                other.GetComponent<EnemyState>().onFire = true;
            }

            //Apply extra damage based on type matchups

            
           
                // make arrow stick
                arrowRB.constraints = RigidbodyConstraints.FreezeAll;
                EnemyState state = other.GetComponent<EnemyState>();
                state.addToCollision(gameObject);
                state.hit(damage, arrowType);
                transform.parent = other.transform;
            
            

            

            
            // to prevent from calling twice from same arrow
            

            
            

            //Debug.Log("health :" + state.health.ToString());
            
        } else if (other.gameObject.layer == 12)
        {
            //environment
            // make arrow stick
            transform.parent = null;
            Debug.Log("hit: envi" + other.gameObject.name);
            arrowRB.constraints = RigidbodyConstraints.FreezeAll;
            damage = 0;
            gameObject.GetComponent<RetrieveArrowScript>().arrowInGround = true;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // getting scripts
        SetType(type);

        

        arrowImpactAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
        transform.forward = Vector3.Slerp(gameObject.transform.forward, gameObject.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime);

    }

    public void SetType(string t)
    {
        if (string.Equals(t, "Fire"))
        {
            arrowType = (int)Elements.Type.Fire;
        }
        else if (string.Equals(t, "Ice"))
        {
            arrowType = (int)Elements.Type.Ice;

        }
        else
        {
            arrowType = (int)Elements.Type.Normal;

        }
    }
}
