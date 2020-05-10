using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    private WeaponSelect select;
    public ParticleSystem lootget;

    // Use this for initialization
    void Start()
    {
        select = player.GetComponent<WeaponSelect>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger"));
        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") > 0.2f)
        {
            if (gameObject.transform.parent.CompareTag("Sword"))
            {
                //anim.SetBool("Grabbed", true);
                //anim.enabled = false;


                //select.equippedMelee.transform.SetPositionAndRotation(GameObject.FindWithTag("Sword").transform.position, GameObject.FindWithTag("Sword").transform.rotation);

                select.equippedMelee = GameObject.FindWithTag("Sword");

                select.Invoke("equipSword", 0);
                select.equippedBow.SetActive(false);
                select.equippedMelee.SetActive(true);
                lootget.Play();
                gameObject.SetActive(false);
            }
            else if (gameObject.transform.parent.CompareTag("Spear"))
            {
                anim.SetBool("Grabbed", true);
                anim.enabled = false;

                select.equippedMelee = GameObject.FindWithTag("Spear");

                select.Invoke("equipSpear", 0);
                select.equippedBow.SetActive(false);
                select.equippedMelee.SetActive(true);
                lootget.Play();
                gameObject.SetActive(false);
            }
            else if (gameObject.transform.parent.CompareTag("Axe"))
            {
                //anim.SetBool("Grabbed", true);
                //anim.enabled = false;

                select.equippedMelee = GameObject.FindWithTag("Axe");

                select.Invoke("equipAxe", 0);
                select.equippedBow.SetActive(false);
                select.equippedMelee.SetActive(true);
                lootget.Play();
                gameObject.SetActive(false);
            }
            else if (gameObject.transform.parent.CompareTag("Knife"))
            {
                //anim.SetBool("Grabbed", true);
                //anim.enabled = false;

                select.equippedMelee = GameObject.FindWithTag("Knife");

                select.Invoke("equipKnife", 0);
                select.equippedBow.SetActive(false);
                select.equippedMelee.SetActive(true);
                lootget.Play();
                gameObject.SetActive(false);
            }
        }
    }
}
