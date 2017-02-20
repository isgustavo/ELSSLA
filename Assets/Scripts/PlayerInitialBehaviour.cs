using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitialBehaviour : MonoBehaviour {

	void Update () {

		Vector3 newPosition = transform.position;

		if (transform.position.z > -5.9f) {
			newPosition.z = Mathf.Lerp (transform.position.z, -6f, Time.deltaTime * 2f);
			transform.position = newPosition;
		} else {
			newPosition.z = -6f;
			transform.position = newPosition;
			gameObject.GetComponent<PlayerInitialBehaviour> ().enabled = false;
		}
	}
}
