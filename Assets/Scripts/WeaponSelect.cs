using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour { //this script should be attached to the player controller

    /*
     * Wepons are selected by holding X which brings up an interface around the user's left hand.
     * The user rotates their hand to hover over the desired weapon and upon release of X, the weapon is selected.
     * If Y is pressed while holding X then a potion may be selected. 
     */

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject ui;
    public GameObject playerHead;

    public GameObject equippedMelee;
    public GameObject equippedBow;

    //sections of interface to allow for choosing --> each should have a text child and a light child (exception potion)
    public GameObject normalArrow;
    public GameObject fireArrow;
    public GameObject iceArrow;
    public GameObject melee;
    public GameObject potion;
    public bool active; //enables/disables this script
    public int selected; // 0-melee, 1-normal, 2-fire, 3-ice

    //public KeyCode toggleInterface; //X button
    private bool displayUI;
    private Vector3 freezeLeftHand;
    private Quaternion freezeUILocalRotation;
    private Quaternion freezeUIRotation;
    private Quaternion leftHandRotation;
    public PlayerStatus playerStat;

    void equipBow()
    {

        equippedBow.transform.SetParent(leftHand.transform);
        equippedBow.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        equippedBow.transform.localPosition = new Vector3(0.0f, -0.03f, -0.03f);
        equippedBow.transform.localRotation = Quaternion.Euler(10, 0, 0);
    }
    void equipSword()
    {

        equippedMelee.transform.SetParent(rightHand.transform);
        equippedMelee.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        equippedMelee.transform.localPosition = new Vector3(0.02f, -0.1f, -0.05f);
        equippedMelee.transform.localRotation = Quaternion.Euler(0, 90, -165);
    }
    void equipSpear()
    {
        equippedMelee.transform.SetParent(rightHand.transform);
        equippedMelee.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        equippedMelee.transform.localPosition = new Vector3(0.02f, 0.42f, 0.13f);
        equippedMelee.transform.localRotation = Quaternion.Euler(20, 0, 0);
    }
    void equipAxe()
    {
        equippedMelee.transform.SetParent(rightHand.transform);
        equippedMelee.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        equippedMelee.transform.localPosition = new Vector3(0.02f, 0.42f, 0.13f);
        equippedMelee.transform.localRotation = Quaternion.Euler(20, 0, 0);
    }
    void equipKnife()
    {
        equippedMelee.transform.SetParent(rightHand.transform);
        equippedMelee.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        equippedMelee.transform.localPosition = new Vector3(0.02f, 0.07f, 0.0f);
        equippedMelee.transform.localRotation = Quaternion.Euler(20, 0, 0);
    }

    // Use this for initialization
    void Start () {
        ui.SetActive(false);
        displayUI = false;
        freezeUILocalRotation = ui.transform.localRotation;
        active = true;
        equippedMelee = new GameObject();
    }
	
	// Update is called once per frame
	void Update () {
        if (!active)
        {
            return;
        }
        if (Input.GetKeyDown("joystick button 2")) //X button
        {
            displayUI = true;
            ui.SetActive(true);
            //freezeLeftHand = leftHand.transform.position;
            leftHandRotation.eulerAngles = new Vector3 (leftHand.transform.rotation.eulerAngles.x, leftHand.transform.rotation.eulerAngles.y, 0);
        }
        if (displayUI) //displays selection interface when the x button is held
        {
            //leftHand.transform.position = freezeLeftHand;
            //hand rotation
            //leftHand.transform.position = freezeLeftHand;
            freezeLeftHand = leftHand.transform.position;
            ui.transform.rotation = leftHandRotation * freezeUILocalRotation;
            float handRotation = leftHand.transform.rotation.eulerAngles.z;

            Debug.Log("Hand Rotation: " + handRotation);

			//MELEE
            if ((handRotation < 120) && (handRotation > 30))
            {
                Debug.Log("melee highlighted");
                melee.GetComponent<Renderer>().material.color = Color.red;
                selected = 0;
            }
            else { melee.GetComponent<Renderer>().material.color = Color.black; }

			//NORMAL ARROW
            if (gameObject.GetComponent<PlayerStatus>().normalArrow < 1)
            {
                normalArrow.SetActive(false);
            } else { normalArrow.SetActive(true);
                if ((handRotation < 30) && (handRotation > 0) || ((handRotation < 360) && (handRotation > 350)))
                {
                    Debug.Log("normal highlighted");
                    normalArrow.GetComponent<Renderer>().material.color = Color.red;
                    selected = 1;
                } else { normalArrow.GetComponent<Renderer>().material.color = Color.black; }
            }

			//FIRE ARROW
            if (gameObject.GetComponent<PlayerStatus>().fireArrow < 1)
            {
                fireArrow.SetActive(false);
            } else { fireArrow.SetActive(true);
                if (((handRotation < 350) && (handRotation > 310))) //fire
                {
                    Debug.Log("fire highlighted");
                    fireArrow.GetComponent<Renderer>().material.color = Color.red;
                    selected = 2;
                }
                else { fireArrow.GetComponent<Renderer>().material.color = Color.black; }
            }

			//ICE ARROW
            if (gameObject.GetComponent<PlayerStatus>().iceArrow < 1)
            {
                iceArrow.SetActive(false);
            } else { iceArrow.SetActive(true);
                if ((handRotation < 310) && (handRotation > 240)) //ice
                {
                    Debug.Log("ice highlighted");
                    iceArrow.GetComponent<Renderer>().material.color = Color.red;
                    selected = 3;
                }
                else { iceArrow.GetComponent<Renderer>().material.color = Color.black; }
            }

			//POTION
            if (gameObject.GetComponent<PlayerStatus>().potion < 1)
            {
                potion.SetActive(false);
            } else //using a potion
            {
                if (Input.GetKeyDown("joystick button 3")) //Y button
                {
                    //Debug.Log("Used a Potion");
                    gameObject.GetComponent<PlayerStatus>().usePotion = true;
                }
            }         
        }

        if (Input.GetKeyUp("joystick button 2")) //X button
        {
            //Debug.Log("released");
            if (selected == 0)
            {
                Debug.Log("melee");
                equippedBow.SetActive(false);
                equippedMelee.SetActive(true);
                switch (equippedMelee.tag)
                {
                    case "Sword":
                        equipSword();
                        break;
                    case "Spear":
                        equipSpear();
                        break;   
                }

            }
            else if (selected == 1)
            {
                Debug.Log("normal arrow");
                equippedMelee.SetActive(false);
                equippedBow.SetActive(true);
                equipBow();
                playerStat.arrowType = 0;
            }
            else if (selected == 2)
            {
                Debug.Log("fire arrow");
                equippedMelee.SetActive(false);
                equippedBow.SetActive(true);
                equipBow();
                playerStat.arrowType = 1;
            }
            else if (selected == 3)
            {
                Debug.Log("ice arrow");
                equippedMelee.SetActive(false);
                equippedBow.SetActive(true);
                equipBow();
                playerStat.arrowType = 2;
            }
            ui.SetActive(false);
            displayUI = false;
        }
	}
}
