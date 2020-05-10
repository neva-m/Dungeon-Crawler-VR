using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    
            if (OVRInput.GetUp(OVRInput.Button.One))
            {
                SceneManager.LoadScene("MainScene");
            }
            else if (OVRInput.GetDown(OVRInput.Button.Two))
            {

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }

        }
    
}
