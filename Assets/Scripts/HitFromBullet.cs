using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFromBullet : MonoBehaviour {
    public PlayerStatus currPlayer;
    public int dmg;


    private void OnTriggerEnter(Collider other)
    {



        if (string.Equals(other.tag, "Player"))
        {
            currPlayer.hit(dmg);
            Debug.Log("ouch! " + currPlayer.health.ToString());
        }
    }

    // Use this for initialization
    void Start () {
        currPlayer = GameObject.Find("OVRPlayerController").GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
