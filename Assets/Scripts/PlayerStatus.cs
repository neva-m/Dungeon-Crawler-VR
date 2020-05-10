using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour { //should be attached to the player controller

    public int health;
    public int armor;
    //number of each loot element player has at any given point in the game
    public int normalArrow;
    public int fireArrow;
    public int iceArrow;
    public int potion;
    public bool haveBow;
    public bool hasArmor;
    public int keys; //need 2 to unlock boss

    public bool usePotion; //after selecting potion should set this to true
    public bool gameOver; //if you die in game

    public Text fireArrowText, iceArrowText, normalArrowText, potionText;
    public int arrowType;

    // Use this for initialization
    void Start () {
        health = 100;
        normalArrow = 50;
        fireArrow = 50;
        iceArrow = 50;
        potion = 10;
        keys = 0;
        armor = 0;
        usePotion = false;
        SetInventoryText();
        arrowType = 0;


    }
	
	// Update is called once per frame
	void Update () {
	    if (usePotion) {
            if (potion != 0) {
                if (health < 100)
                {
                    heal(20);
                    SetInventoryText();
                    potion--;
                }
            }
            usePotion = false;
        }
        if (gameOver)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        }
        //Debug.Log(health);

    }

    public void hit(int damage)
    {
        if ((armor - damage) <= 0)
        {
            damage -= armor;
            armor = 0;

            health -= damage;

            if (health <= 0)
            {
                gameOver = true;
            }

        }
        else
        {
            armor -= damage;
        }
    }

    public void heal(int healBy)
    {
        health += healBy;
        Mathf.Min(health, 100);
    }

    public void RetrieveArrow(GameObject arrow, int type) {
        if (type == 1) //fire arrow
        {
            fireArrow++;
        }
        else if (type == 2) //ice arrow
        {
            iceArrow++;
        }
        else //normal arrow
        {
            normalArrow++;
        }
        //remove arrow from the scene 
        arrow.SetActive(false); //delete arrow
        SetInventoryText();
    }
    public void UseArrow()
    {
        switch (arrowType)
        {
            case 0:
                normalArrow--;
                break;
            case 1:
                fireArrow--;
                break;
            case 2:
                iceArrow--;
                break;
            
        }
        SetInventoryText();
    }

    public void GetKey(GameObject key)
    {
        keys++;
        key.SetActive(false);
    }

    public void GetArmor(GameObject shield, int strength)
    {
        armor += strength;
        shield.SetActive(false);
    }

    public void SetInventoryText()
    {
        fireArrowText.text = fireArrow.ToString();
        iceArrowText.text = iceArrow.ToString();
        normalArrowText.text = normalArrow.ToString();
        potionText.text = potion.ToString();
    }
}