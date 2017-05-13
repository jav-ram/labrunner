using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ThirdpersonCamera : NetworkBehaviour {

	public Transform lookAt;
	public Transform camTransform;

	private Camera cam;

	public float distance = 5f;
	private float currentX = 0f;
	private float currentY = 0f;
	private float sensitivityX = 4f;
	private float sensitivityY = 1f;

	private void Start(){
		
	}

	private void Update(){
		
		if (GetComponentInParent<NetworkIdentity>().isLocalPlayer) {
			currentX += Input.GetAxis ("Mouse X");
			currentY += Input.GetAxis ("Mouse Y");
			currentY = Mathf.Clamp (currentY, 20f, 30f);
		}
	}

	private void FixedUpdate(){
	}

	private void LateUpdate(){
		if (GetComponentInParent<NetworkIdentity>().isLocalPlayer) {
			Vector3 dir = new Vector3 (0, 0, -distance);
			Quaternion rotation = Quaternion.Euler (20f, currentX, 0);
			camTransform.position = (lookAt.position + new Vector3(0f,.5f,0f)) + rotation * dir;
			camTransform.LookAt (lookAt.position + new Vector3(0f,.5f,0f));
		}
	}

}
