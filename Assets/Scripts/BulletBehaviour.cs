using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletBehaviour : NetworkBehaviour {

	public float lifetime = 2.0f;
	public float speed = 5.0f;

	void Start () {
		Destroy (gameObject, lifetime);
		
	}


	void Update () {


		if (isServer) {
			Debug.Log ("isServer");
		} else {
			Debug.Log ("client");
		}
		transform.Translate (Vector3.up * Time.deltaTime * speed);
		//transform.position += transform.forward * Time.deltaTime * speed;
	}
}
