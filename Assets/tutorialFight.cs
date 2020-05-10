using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialFight : MonoBehaviour {

    // Use this for initialization
    public GameObject demon1;
    public GameObject demon2;
    public GameObject demon3;
    public GameObject player;
    private int goals= 0;
    private bool done1, done2, done3;
    void Start () {
        done1 = false;
        done2 = false;
        done3 = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!demon1 && !done1)
        {
            goals++;
            done1 = true;
        }
        if (!demon2 && !done2)
        {
            goals++;
            done2 = true;
        }
        if (!demon3 && !done3)
        {
            goals++;
            done3 = true;
        }
        //Debug.Log(goals);
        if(goals == 3)
        {
            player.transform.SetPositionAndRotation(new Vector3(-56.1f, 2.7f, -24.6f), Quaternion.Euler(0, 90, 0));
            Destroy(gameObject);
        }
    }
}
