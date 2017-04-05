using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraBehaviour : MonoBehaviour {

	private const float CAMERA_Z_POSITION = -10f;
	private PlayerBehaviour player;
	public bool isDead;
	void LateUpdate () {

		if (player == null)
			return;

		if (!player.isDead) {
			Vector3 newPosition = new Vector3 (player.transform.position.x, player.transform.position.y, CAMERA_Z_POSITION);

			gameObject.transform.position = newPosition;
		} else {

			Vector3 position = gameObject.transform.position;

			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(0f,0f, -20f), Time.deltaTime * 10);
		}

	}

	public void SetPlayer(PlayerBehaviour player) {

		this.player = player;
	}
		
}
