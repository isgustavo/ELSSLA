using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToJoinBehaviour : MonoBehaviour {

	[SerializeField]
	private NetworkManagerBehaviour networkManager;

	void OnMouseUp() {
		networkManager.StartAsAClient ();

	}
		
}
