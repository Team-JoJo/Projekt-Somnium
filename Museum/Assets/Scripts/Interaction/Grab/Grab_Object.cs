using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* bietet Zugriff auf die physikalischen Eigenschaften eines Objektes durch den Rigidbody
 * dies ist das Gegenstück zu Grab_Host, der auf diese Funktionen zugreift
 * benötigt sowohl Rigidbody, als auch 2 Collider(wobei einer ein Trigger sein muss)
 */

[RequireComponent(typeof(Rigidbody))]
public class Grab_Object : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player;

    private static float triggerRange = 5f;
    private bool isPlayerNearby;
    private bool isGrabbed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            print(gameObject.name + " wurde kein Rigidbody hinzugefügt!");
        }
    }

    public void Shoot(float amount, Vector3 direction)
    {
        Drop();
        rb.AddForce(direction * amount);
    }

    public void Drop()
    {
        Vector3 velocity = rb.velocity;
        isGrabbed = false;
        rb.isKinematic = false;
        rb.transform.parent = null;
        rb.AddForce(velocity);
    }

    public void SetGrabbed(Transform head)
    {
        isGrabbed = true;
        rb.isKinematic = true;
        rb.transform.parent = head;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isGrabbed)
        {
            Grab_Host.instanz.DropObject();
        }  
    }
}
