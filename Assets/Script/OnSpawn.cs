using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnSpawn : NetworkBehaviour {

	public Camera cam;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
			cam.transform.parent = this.transform;
			cam.GetComponent<ThirdpersonCamera> ().lookAt = this.transform;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
