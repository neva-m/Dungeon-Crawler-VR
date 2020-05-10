using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetArmor : MonoBehaviour {

    public Collider player;

    private int armorStrength = 10; //how much the armor protects

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            player.gameObject.GetComponent<PlayerStatus>().GetArmor(gameObject, armorStrength);
        }
    }
}
