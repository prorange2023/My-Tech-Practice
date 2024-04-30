using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace CreatingCharacter.Player
//{
//    [RequireComponent(typeof(Rigidbody))]
//    public class PlayerMovementController : MonoBehaviour
//    {
//        [SerializeField] private float movementSpeed;
//        [SerializeField] private float jumpForce;

//        private Rigidbody rb;
//        private float distanceToFeet;

//        private void Awake()
//        {
//            rb = GetComponent<Rigidbody>();
//            distanceToFeet = GetComponent<Collider>().bounds.extents.y;
//        }

//        private void Update()
//        {
            
//        }
//        private void Move()
//        {
//            Vector2 movementInput = new Vector2(Input.GetAxis("Horizonal"), Input.GetAxis("Vertical")).normalized;
//            movementInput *= movementSpeed*Time.deltaTime;
//            transform.Translate(new Vector3(movementInput.x, 0f, movementInput.y));
//        }

//        private void Jump()
//        {
//            //if (Input.GetKeyDown(KeyCode.Space))
//            //{
//            //    if(IsGrounded())
//            //    {
//            //        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//            //    }
//            //}
//        }
//    }
//}

