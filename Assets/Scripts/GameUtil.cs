using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtil : MonoBehaviour {


	public const float Z_FINAL_POSITION = -5f;
	public const float Z_DEAD_POSITION = -10f;
	public const float POSITION_SPEED = 10f;
	public static Vector3 NO_USE_POSITION = new Vector3 (0, 0, -20);

	public static void VerifyZPosition (Rigidbody rb) {

		if (rb.position.z != GameUtil.Z_FINAL_POSITION) {
			Vector3 newPosition = rb.position;
			newPosition.z = Mathf.Lerp (rb.position.z, GameUtil.Z_FINAL_POSITION, Time.deltaTime * POSITION_SPEED);
			rb.position = newPosition;
		} 
	}

	public static Vector3 GetRandomPosition () {

		return new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5), Random.Range (5, 15));

	}

	public static Vector3 GetRandomVelocity () {

		return new Vector3 (Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
	}

}
