using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

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

