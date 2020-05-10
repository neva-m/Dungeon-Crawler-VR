using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject arrow;
    public GameObject rotate;

    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Hides mouse
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        Quaternion oldRotation = transform.rotation;
        transform.rotation = rotate.transform.rotation;
        transform.Translate(straffe, 0, translation);
        transform.rotation = oldRotation;


    }
}
