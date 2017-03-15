using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnButtonBehaviour : MonoBehaviour {

	public static bool isHold = false;

	private float timeToHold = .3f;
	private float timeHolding = 0.0f;

	public void HoldTrigger (bool value) {

		isHold = value;
	}

	void Update () {

		if (!isHold)
			return;


		if (timeHolding < timeToHold) {

			timeHolding += Time.deltaTime;
		} else {



		}




	}


}
