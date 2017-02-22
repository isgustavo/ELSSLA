using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InitialBehaviour : MonoBehaviour {

	public float speed = 2f;
	public float zFinalPosition = -6f;
	private float limit = 0;

	void Start () {

		limit = zFinalPosition + 0.01f;

	}

	void Update () {

		Vector3 newPosition = transform.position;

		if (transform.position.z > limit) {
			newPosition.z = Mathf.Lerp (transform.position.z, zFinalPosition, Time.deltaTime * speed);
			transform.position = newPosition;
		} else {
			newPosition.z = zFinalPosition;
			transform.position = newPosition;
			gameObject.GetComponent<InitialBehaviour> ().enabled = false;
		}
	}
}
