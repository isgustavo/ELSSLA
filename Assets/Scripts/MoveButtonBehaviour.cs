using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonBehaviour : MonoBehaviour {

	public static bool isMoveOn = false;


	public void MoveTrigger (bool value) {
		
		isMoveOn = value;
	}


}
