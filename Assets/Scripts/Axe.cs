using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    // Use this for initialization
    public int damage;
    private EnemyAI enemyai;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyState>())
        {
            EnemyState state = other.GetComponent<EnemyState>();
            state.hit(damage);
        }

    }
}
