using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineBall : MonoBehaviour {

	public float speed;
	public bool dir;//true derecha false izquierda
	public Rigidbody rb;
	public float limit;
	public Vector3 origin;
	public float offsetPos;
	public float offsetNeg;

	// Use this for initialization
	void Start () {
		origin = transform.position;
		offsetPos = origin.x+limit;
		offsetNeg = origin.x-limit;

		rb = GetComponent<Rigidbody> ();
		dir = true;
		rb.velocity = new Vector3 (1f, 0f, 0f)*speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > limit || transform.position.x < -limit) {
			changeDir ();
		}
	}

	private void changeDir(){
		
		if (transform.position.x > offsetPos) {
			rb.velocity = Vector3.zero;
			rb.velocity = new Vector3 (-1f, 0f, 0f) * speed;
		} else if (transform.position.x < offsetNeg) {
			rb.velocity = Vector3.zero;
			rb.velocity = new Vector3 (1f, 0f, 0f) * speed;
		}
	}

	private void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals ("Player")) {
			other.gameObject.transform.position = new Vector3 (0, 0, 0);
			Debug.Log ("Dead");
		}
	}
}
