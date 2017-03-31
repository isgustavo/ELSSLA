using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalStarsBehaviour : MonoBehaviour {

	public float speed;
	private Transform playerTransform;


	void Update () {

		if (playerTransform == null)
			return;

		if (transform.position.z < 0) {
			transform.position = Vector3.Lerp (transform.position, playerTransform.position * -1, Time.deltaTime * speed);
		}

	}

	public void SetPlayer(Transform transform) {

		this.playerTransform = transform;
	}

}
