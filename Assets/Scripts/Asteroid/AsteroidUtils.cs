using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidUtils : MonoBehaviour {


	public static Vector3 GetRandomPosition () {

		return new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5), -6);

	}

	public static Vector3 GetRandomVelocity () {

		return new Vector3 (Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
	}

}
