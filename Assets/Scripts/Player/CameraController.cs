using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraController : MonoBehaviour
{
    Vector3 cameraPosition;
    public Camera playerCamera;
    float speed = 1.0f;
    float zoomSpeed = 40.0f;
    float maxHeight = 60.0f;
    float minHeight = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();

    }

    void CameraMovement()
    {
        float hSpeed = speed * Input.GetAxis("Horizontal");
        float vSpeed = speed * Input.GetAxis("Vertical");
        float scrollSpeed = -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        Vector3 verticalMove = new Vector3(0, scrollSpeed, 0);
        Vector3 lateralMove = hSpeed * transform.right;
        Vector3 forwardMove = vSpeed * transform.up;
        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;
    }
}
