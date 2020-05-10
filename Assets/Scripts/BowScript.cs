using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    private float charge;
    public float chargeMax;
    public float force;

    private bool stringPulled; 
    private bool stringLetGo;

    public Transform spawn;
    public Rigidbody arrowObj;
    public GameObject bow;

    private float startingx;
    private Rigidbody arrow;
    private Vector3 origPos;
    private Quaternion origRot;
    public OVRGrabbable grabbableScript;
     
    public GameObject stringParent;
    public GameObject anchor;
    public Rigidbody stringRigidBody;
    public GameObject rightHandAnchor;
    public GameObject ArrowEndPoint;
    private Quaternion newRotation;

    private float startingz = 0;

    private Transform origStringTrans;
    private Quaternion origStringRot;
    private AudioSource shootingSourse;
    public PlayerStatus playerAmmo;

    public WeaponSelect weaponSelect;
    private int selectedType;
    private Vector3 stringStart;
    public Material fireMaterial, iceMaterial, normalMaterial;
     

    // Start is called before the first frame update
    void Start()
    {
        startingx = transform.localPosition.x;
        origPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        origRot = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
        stringPulled = false;
        stringLetGo = false;
        origStringTrans = transform;
        grabbableScript = gameObject.GetComponent<OVRGrabbable>();
        origStringRot = transform.rotation;
        shootingSourse = GetComponent<AudioSource>();

        selectedType = weaponSelect.selected;
    }

    // Update is called once per frame
    void Update()
    {
        selectedType = weaponSelect.selected;
        if (arrow)
        {
            arrow.transform.position = new Vector3(anchor.transform.position.x, anchor.transform.position.y, anchor.transform.position.z);
            //transform.position = new Vector3(anchor.transform.position.x, anchor.transform.position.y, anchor.transform.position.z);
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y, arrow.transform.localPosition.z + (float) 3);
            //newRotation = Quaternion.RotateTowards(transform.rotation, spawn.rotation, 100);
            //newRotation.Set(newRotation.x + 180, newRotation.y, newRotation.z, newRotation.w);
            //arrow.transform.rotation = Quaternion.LookRotation(ArrowEndPoint.transform.position - transform.position);
            //arrow.transform.rotation = bow.transform.rotation;
            //arrow.transform.LookAt(ArrowEndPoint.transform);
            //arrow.transform.

        }
        if (!stringPulled)
        {
            

        }

        if (grabbableScript.isGrabbed && arrow == null)
        {
            stringPulled = true;
            startingz = anchor.transform.localPosition.z;
            arrow = Instantiate(arrowObj, spawn.position, spawn.rotation) as Rigidbody;
            

            switch (selectedType)
            {
                case (1):
                    arrow.gameObject.GetComponent<ArrowScript>().SetType("Normal");
                    arrow.gameObject.GetComponent<MeshRenderer>().material = normalMaterial;
                    break;
                case (2):
                    arrow.gameObject.GetComponent<ArrowScript>().SetType("Fire");
                    arrow.gameObject.GetComponent<MeshRenderer>().material = fireMaterial;

                    break;
                case (3):
                    arrow.gameObject.GetComponent<ArrowScript>().SetType("Ice");
                    arrow.gameObject.GetComponent<MeshRenderer>().material = iceMaterial;

                    break;
                default:
                    Debug.Log("selected weapon is weird");
                    break;
            }
            //Debug.Log("selected" + selectedType.ToString());
            
            arrow.transform.parent = spawn;

            arrow.transform.position = new Vector3(anchor.transform.position.x, anchor.transform.position.y, anchor.transform.position.z);

            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y, arrow.transform.localPosition.z + (float)3);
            stringStart = arrow.transform.localPosition;
            //Debug.Log("starting arrow:" + arrow.transform.localPosition.ToString());
            arrow.transform.LookAt(arrow.transform.position + arrow.transform.GetComponent<Rigidbody>().velocity);
            //Debug.Log("I grabbed" + stringPulled.ToString());
        }

        if (stringPulled && !grabbableScript.isGrabbed)
        {
            stringLetGo = true;
            //Debug.Log("I shot" + stringPulled.ToString());
            playerAmmo.UseArrow();
        }


        if (stringPulled)
        {
            //Debug.Log("x: " + arrow.transform.localPosition.x.ToString() + " y: " + arrow.transform.localPosition.y.ToString() + " z: " + arrow.transform.localPosition.z.ToString());
            if ((startingz - anchor.transform.localPosition.z) < 0)
            {
                int x;
                x = 0;
                x++;
            }
            //charge = Mathf.Max((startingz - anchor.transform.localPosition.z), 0) * force;
            charge = Mathf.Max(stringStart.z - arrow.transform.localPosition.z, 0) * force;

        }


        if (stringLetGo)
        {
            //Debug.Log("charge: " + charge.ToString());
            //transform.localPosition = origStringTrans.localPosition;
            //transform.localRotation = origStringTrans.localRotation;
            
            transform.parent = stringParent.transform;
            //Debug.Log(charge.ToString());
            Debug.Log("arrow type" + arrow.gameObject.GetComponent<ArrowScript>().type.ToString());
            // change force to charge
            arrow.AddForce(arrow.transform.forward * charge, ForceMode.Impulse);
            arrow.useGravity = true;
            arrow.gameObject.transform.parent = null;

            stringLetGo = false;
            stringPulled = false;
            arrow = null;
            transform.rotation = origStringRot;

            transform.localPosition = origPos;
            transform.localRotation = origRot;
            //stringRigidBody.constraints = RigidbodyConstraints.FreezeAll;

            shootingSourse.Play();
        }
    }
}
