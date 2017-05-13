using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {
	public float speed;
	public float runSpeed;
	public float jumpSpeed;
	public float gravity;

	private CharacterController controller;
	private Vector3 moveDirection = Vector3.zero;
	private Rigidbody rb;
	private Animation anim;

	private void Start(){
		controller = GetComponent<CharacterController> ();
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();
		anim.Play ("Idle");
	}

	private void Update(){
		if (isLocalPlayer) {
			controller = GetComponent<CharacterController> ();
			if (controller.isGrounded){

				moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
				moveDirection = transform.TransformDirection (moveDirection);
				if (Input.GetKey (KeyCode.LeftShift)) {
					moveDirection *= runSpeed;
				} else {
					moveDirection *= speed;
				}

				if (Input.GetButton ("Jump")) {
					moveDirection.y = jumpSpeed;
				}

			}

			if (moveDirection.magnitude < 1f) {

			} else {
				moveDirection.y -= gravity * Time.deltaTime;
				controller.Move (moveDirection * Time.deltaTime);
				anim.CrossFade ("Walk");
				anim.Play ();
			}
		}


	}

	public void OnCollisionEnter(Collision other){
		Debug.Log (other.gameObject.name);
	}


}
