using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayBehaviour : MonoBehaviour {

	private NetworkManagerBehaviour networkManager;

	void Start () {

		networkManager = GameObject.FindGameObjectWithTag ("NetworkManager").GetComponent<NetworkManagerBehaviour> ();
	}

	void OnMouseUp() {
		Debug.Log ("OnMauseUp");
		networkManager.StartAsAHost ();

	}
}
