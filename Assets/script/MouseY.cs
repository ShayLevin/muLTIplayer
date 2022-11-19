using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseY : MonoBehaviour
{
    public float speed = 2f;
    float mouseY = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseY -= Input.GetAxis("Mouse Y") * speed;
        mouseY = Mathf.Clamp(mouseY, -90, 60);
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x = mouseY;
        transform.localEulerAngles = newRotation;
    }
}
