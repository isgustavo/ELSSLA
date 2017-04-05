using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUtil : MonoBehaviour {

	public const int WORLD_RADIUS = 35;
	public const float Z_FINAL_POSITION = -5.6f;
	public const float Z_DEAD_POSITION = -10f;
	public const float POSITION_SPEED = 10f;
	public static Vector3 NO_USE_POSITION = new Vector3 (0, 0, -20);

	public static bool VerifyInsideWorld (Vector3 position) {

		float dx = Mathf.Abs (position.x);
		float dy = Mathf.Abs (position.y);

		if (dx + dy <= WORLD_RADIUS) {
			return true;
		} else {
			return false;
		}

	}

	public static void AjustZPosition (Rigidbody rb) {

		if (rb.position.z != GameUtil.Z_FINAL_POSITION) {
			Vector3 newPosition = rb.position;
			newPosition.z = Mathf.Lerp (rb.position.z, GameUtil.Z_FINAL_POSITION, Time.deltaTime * POSITION_SPEED);
			rb.position = newPosition;
		} 
	}

	public Vector3 AjustZPosition (Vector3 position) {

		if (position.z != GameUtil.Z_FINAL_POSITION) {
			Vector3 newPosition = position;
			newPosition.z = Mathf.Lerp (position.z, GameUtil.Z_FINAL_POSITION, Time.deltaTime * POSITION_SPEED);
			return newPosition;
		} 

		return position;
	}

	public static Vector3 GetRandomPosition () {

		return new Vector3 (Random.Range (-15, 15), Random.Range (-15, 15), Z_FINAL_POSITION);

	}

	public static Vector3 GetRandomVelocity () {

		return new Vector3 (Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
	}

	public IEnumerator FadeTextToFullAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeTextToZeroAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}


}
