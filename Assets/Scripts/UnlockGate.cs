using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnlockGate : MonoBehaviour {

    public GameObject player;
    public Text key_display;
    private int num_keys = 0;

	// Use this for initialization
	void Start () {
        num_keys = 0;
    }
	
	// Update is called once per frame
	void Update () {
        num_keys = player.GetComponent<PlayerStatus>().keys;
        key_display.text = "Keys: " + num_keys + ". Need 2 keys.";
        if (num_keys == 2)
        {
            Collider gateCollider = GetComponent<Collider>();
            gateCollider.isTrigger = true;
            key_display.text = "Congrats!";
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Victory");
    }
}
