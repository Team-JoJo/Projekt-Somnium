using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public float mouseSensitivity;
    private float xRotation = 0f;
    public Transform playerBody;
    public Transform head;
    public Texture2D headCursor;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.SetCursor(headCursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

        AdjustCamera();
    }

    private void AdjustCamera()
    {
        float mouseX;
        float mouseY;
        mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (mouseX < 0.1 && mouseX > -0.1)
        {
            mouseX = 0;
        }
        if (mouseY < 0.1 && mouseY > -0.1)
        {
            mouseY = 0;
        }


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75, 75f);

        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
