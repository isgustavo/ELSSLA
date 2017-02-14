using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapBehaviour : MonoBehaviour {


	void OnMouseDown() { 
		Debug.Log ("OnMouseDown");
	}

	void OnMouseUp() {
		gameObject.SetActive (false);
	}

}
