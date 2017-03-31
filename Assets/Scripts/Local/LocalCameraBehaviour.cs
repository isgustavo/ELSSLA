using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraBehaviour : MonoBehaviour {

	private const float CAMERA_Z_POSITION = -10f;
	private Transform playerTransform;

	void LateUpdate () {

		if (playerTransform == null)
			return;

		Vector3 newPosition = new Vector3 (playerTransform.position.x, playerTransform.position.y, CAMERA_Z_POSITION);

		transform.position = newPosition;
	}

	public void SetPlayer(Transform transform) {

		this.playerTransform = transform;
	}
}
