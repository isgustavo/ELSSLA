using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootButtonBehaviour : MonoBehaviour {

	public static bool isShooting = false;


	public void ShootTrigger (bool value) {

		isShooting = value;
	}
}
