using UnityEngine;

namespace Assets.Scripts.Player
{
    public class CameraController : MonoBehaviour
    {
        public Camera PlayerCamera;
        float speed = 1.0f;
        float zoomSpeed = 40.0f;

        // Update is called once per frame
        void Update()
        {
            CameraMovement();
        }

        void CameraMovement()
        {
            var hSpeed = speed * Input.GetAxis("Horizontal");
            var vSpeed = speed * Input.GetAxis("Vertical");
            var scrollSpeed = -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

            var verticalMove = new Vector3(0, scrollSpeed, 0);
            var lateralMove = hSpeed * transform.right;
            var forwardMove = vSpeed * transform.up;
            var move = verticalMove + lateralMove + forwardMove;

            transform.position += move;
        }
    }
}
