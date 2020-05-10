using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveArrowScript : MonoBehaviour { //should be attached to an arrow

    public Collider player;
    public int arrowType;

    private BoxCollider arrowCollider; //box collider associated with the arrow, should be seperate from the one that kills enemies --> enemy killing one should be disabled when in the ground or set to be the not in ground behavior
    private Vector3 initSize; //initial size of the box collider 
    public bool arrowInGround;
    private bool getArrow;
    private bool arrowMove; //condition to move the arrow towards the player

	// Use this for initialization
	void Start () {
        arrowCollider = gameObject.GetComponent<BoxCollider>();
        initSize = arrowCollider.size;
        getArrow = false;
        arrowCollider = GetComponent<BoxCollider>();
        arrowCollider.isTrigger = false;
        arrowMove = false;
    }

    // Update is called once per frame
    void Update () {
		if (arrowInGround)
        {
            GetComponent<ArrowScript>().enabled = false;
            arrowCollider.isTrigger = true;
            arrowCollider.size = new Vector3(50, 20, 50); //change the size of the box collider so we can retrieve it
        }
        else
        {
            arrowCollider.size = initSize;
        }

        if (arrowMove)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.2f);
        }

        if (getArrow)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            dist = Mathf.Abs(dist);
            if (dist <= 1.5f)
            {
                player.gameObject.GetComponent<PlayerStatus>().RetrieveArrow(gameObject, arrowType);
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if ((other == player) && (arrowInGround)) {
            getArrow = true;
            arrowMove = true;
        }
    }

    public void SetArrowInGround(bool setVal)
    {
        arrowInGround = setVal;
    }
}
