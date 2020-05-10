using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Over_Script : MonoBehaviour {
    public GameObject menu;
    private bool gameOver;
    public bool active = true;
    // Use this for initialization
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }
        gameOver = GetComponent<PlayerStatus>().gameOver;
        if (gameOver){
            GetComponent<WeaponSelect>().active = false;
            menu.SetActive(true);
        }
        if (gameOver)
        {


            if (Input.GetKeyDown("joystick button 2")) //X button
            {
                //reload level

            }
            else if (Input.GetKeyDown("joystick button 1")) //A button
            {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

            }
        }
    }
}
