using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyState : MonoBehaviour
{
    public int health = 100;
    public bool onFire = false;
    float timer = 0f;
    float stopFireTimer = 0f;
    List<GameObject> currCollisions = new List<GameObject>();
    public int demonType = -1;
    public string type;

    /*private void OnTriggerEnter(Collider other)
    {
        if (string.Equals(other.gameObject.tag, "Arrow"))
        {
            health -= 10;
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        if (string.Equals(type, "Fire"))
        {
            demonType = (int)Elements.Type.Fire;
        }
        else if (string.Equals(type, "Ice"))
        {
            demonType = (int)Elements.Type.Ice;
        }
        else
        {
            demonType = (int)Elements.Type.Normal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (onFire)
        {
            timer += Time.deltaTime;
            stopFireTimer += Time.deltaTime;
            if (timer > 1f)
            {

                

                if (demonType == (int) Elements.Type.Ice)
                {
                    hit(2);
                }
                else if (demonType == (int)Elements.Type.Fire)
                {
                    heal(1);
                }
                
                Debug.Log("on fire health: " + health.ToString());
                timer = 0f;
            }
            if (stopFireTimer > 10f)
            {
                onFire = false;
                stopFireTimer = 0f;
                timer = 0f;
            }
        }   
    }

    public void heal(int healBy)
    {
        health += healBy;
        Mathf.Min(health, 100);
    }

    public void hit(int damage, int type = (int)Elements.Type.Normal)
    {
        // Deal damage twice if type match ups
        if ((type == (int)Elements.Type.Fire && demonType == (int)Elements.Type.Ice) || (type == (int)Elements.Type.Ice && demonType == (int)Elements.Type.Fire))
        {
            Debug.Log("double dmg");
            health -= damage;
        }

        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            foreach (GameObject arr in currCollisions)
            {
                //arr.GetComponent<Rigidbody>().useGravity = true;
                arr.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            }
        }
        Debug.Log("enemy health: " + health.ToString());
    }

    public void addToCollision(GameObject arrow)
    {
        currCollisions.Add(arrow);
    }
}
