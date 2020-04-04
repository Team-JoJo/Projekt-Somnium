using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Nutzt Raycasts um ein Objekt zu erfassen und bietet dann die Möglichkeit dieses zu greifen, zu platzieren und zu schießen
 * Zur Nutzung werden die Komponente RaycastSchiesser im selben Objekt benötigt
 */

[RequireComponent(typeof(RaycastSchiesser))]
public class Grab_Host : MonoBehaviour
{
    public static Grab_Host instanz;
    private Grab_Object lastObject;
    public Transform head;
    public RaycastSchiesser raycast;
    public float schussStärke;
    public float erlaubteGreifDistanz;

    private bool isGrabbed;

    void Awake()
    {
        if (instanz == null)
        {
            instanz = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isGrabbed = false;
        raycast = GetComponent<RaycastSchiesser>();
        if (raycast == null)
        {
            print("Es wurde kein RaycastSchiesser am selben Objekt wie Grab_Host hinzugefügt");
        }
    }

    public void DropObject()
    {
        if (lastObject != null && isGrabbed)
        {
            lastObject.Drop();
            isGrabbed = false;
            lastObject = null;
        }
    }

    private void ShootObject()
    {
        if (lastObject != null && isGrabbed)
        {
            lastObject.Shoot(schussStärke, head.forward);
            isGrabbed = false;
            lastObject = null;
        }
    }

    private void UseGrab()
    {
        if (lastObject != null && !isGrabbed)
        {
            isGrabbed = true;
            lastObject.SetGrabbed(head);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            if (!isGrabbed)
            {
                if (raycast == null)
                {
                    raycast.GetComponent<RaycastSchiesser>();
                }
                lastObject = raycast.GetGrab_Object();
                if (lastObject != null)
                {
                    
                    if (Vector3.Distance(lastObject.transform.position, transform.position) < erlaubteGreifDistanz)
                    {
                        UseGrab();
                    }
                }
                
            }
            else
            {
                if (lastObject != null)
                {
                    DropObject();
                }
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            print("fir1");
            if (isGrabbed && lastObject!=null)
            {
                ShootObject();
            }
        }

    }
}
