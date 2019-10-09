using System.Collections;

using UnityEngine;


[RequireComponent (typeof (CharacterController))]

public class Player : MonoBehaviour {

    public float speed;

    public float jumpSpeed;

    public float gravity;



    private bool isWaiting = false;



    public GameObject firePit;

    public GameObject ground;



    private CharacterController _controller;



    public void Start () {

        _controller = GetComponent<CharacterController> ();

    }



    private Vector3 moveDirection = Vector3.zero;

    public void Update () {

        if (!isWaiting) {

            if (_controller.isGrounded) {

                moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));

                moveDirection = transform.TransformDirection (moveDirection);

                moveDirection *= speed;

                if (Input.GetButton ("Jump"))

                    moveDirection.y = jumpSpeed;

            }

            moveDirection.y -= gravity * Time.deltaTime;

            _controller.Move (moveDirection * Time.deltaTime);

        }

    }



    public void OnControllerColliderHit (ControllerColliderHit hit) {

        if (hit.collider.gameObject.name.Contains ("PaperBox")) {

            StartCoroutine (Wait ());

        } else if (hit.collider.gameObject.name.Contains ("Fire")) {

            Application.LoadLevel ("1");

        }

    }



    IEnumerator Wait () {

        isWaiting = true;

        yield return new WaitForSeconds (3);

        isWaiting = false;

    }

}