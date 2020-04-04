using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSchiesser : MonoBehaviour
{
    public Camera firstPersonKamera;

    private Transform RaycastSchiessen()
    {
        Ray strahl = firstPersonKamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit treffer;

        if (Physics.Raycast(strahl, out treffer))
        {
            Transform transformTreffer = treffer.transform;
            if (transformTreffer != null)
            {
                return transformTreffer;
            }
        }
        return null;
    }

    public Grab_Object GetGrab_Object()
    {
        return RaycastSchiessen().GetComponent<Grab_Object>();
    }
}
