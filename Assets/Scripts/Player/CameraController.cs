using System;
using UnityEngine;
namespace Assets.Scripts.Player
{

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float zoomSpeed = 40.0f;
        [SerializeField] private GameObject player;


        
        private int counter;

        private void Start()
        {
            player = GameObject.Find("player");
        }
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F)) counter++;
            switch (counter % 2)
            {
                case (0):
                    FollowPlayerMovement();
                    break;
                case (1):
                {
                    var h = Input.GetAxis("Horizontal");
                    var v = Input.GetAxis("Vertical");
                    var s = Input.GetAxis("Mouse ScrollWheel");
                    if (h != 0 || v != 0 || s != 0) CameraMovement(h, v, s);
                    break;
                }
            }
        }

        private void CameraMovement(float h, float v, float s)
        {
            var hSpeed = speed * h;
            var vSpeed = speed * v;
            var scrollSpeed = -zoomSpeed * s;
            
            var verticalMove = new Vector3(0, scrollSpeed, 0);
            var lateralMove = hSpeed * transform.right;
            var forwardMove = vSpeed * transform.up;
            var move = verticalMove + lateralMove + forwardMove;

            transform.position += move;
        }

        private void FollowPlayerMovement()
        {
            if (player == null) return;
            var playerPosition = player.transform.position;
            var cameraPosition = transform.position;
            var cameraOffset = new Vector3(0, 10, -10);
            var newPosition = playerPosition + cameraOffset;
            transform.position = newPosition;
            transform.LookAt(player.transform);
            Debug.Log($"camera position{transform.position}");
        }
    }
}
