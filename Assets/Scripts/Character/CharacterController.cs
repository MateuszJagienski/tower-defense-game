using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Character
{
    public class CharacterController : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private int maxJumps = 2;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float dashDistance = 5f;
        [SerializeField] private float groundDistance = 0.2f;

        private int jumps;
        private bool isGrounded;
        private Transform groundChecker;
        private Vector3 dashVelocity;

        private Vector3 inputs = Vector3.zero;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            groundChecker = transform.GetChild(0);
        }

        // Update is called once per frame
        void Update()
        {
            inputs = Vector3.zero;
            inputs.x = Input.GetAxis("Horizontal");
            inputs.z = Input.GetAxis("Vertical");

            isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, 1 << 8, QueryTriggerInteraction.Ignore);

            if (inputs != Vector3.zero)
                transform.forward = inputs;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }

            if (!Input.GetKeyDown(KeyCode.X)) return;
            dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime)));
            rb.AddForce(dashVelocity, ForceMode.VelocityChange);
        }

        private void FixedUpdate()
        {
            rb.MovePosition((rb.position + inputs * speed * Time.fixedDeltaTime));
        }

        private void OnCollisionEnter(Collision collision)
        {
            jumps = 0;
        }
    }
}