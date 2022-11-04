using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 250f;

    public Transform playerBody;

    float XRotation = 0f;

    // Flash Light
    private GameObject flashLight;
    private bool lightOn;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        flashLight = transform.GetChild(1).gameObject;
        lightOn = true;
        
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if(Input.GetKeyDown(KeyCode.F))
        {
            flashLight.SetActive(!lightOn);
            lightOn = !lightOn;
            
        }

    }
}
