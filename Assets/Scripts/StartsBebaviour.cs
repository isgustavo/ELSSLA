using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartsBebaviour : MonoBehaviour {
	

	void Update () {
		
		Vector3 newPosition = transform.position;

		if (transform.position.z > 5.1f) {
			newPosition.z = Mathf.Lerp (transform.position.z, 5, Time.deltaTime * 1f);
			transform.position = newPosition;
		} else {
			newPosition.z = 5f;
			transform.position = newPosition;
			gameObject.GetComponent<StartsBebaviour> ().enabled = false;
		}

	}
}
