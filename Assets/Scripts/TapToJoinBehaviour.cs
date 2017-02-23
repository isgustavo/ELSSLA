using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToJoinBehaviour : MonoBehaviour {

	public static int ConnectionTesterStatus = 0;

	[SerializeField]
	private NetworkManagerBehaviour networkManager;

	void OnMouseUp() {
		ConnectionTesterStatus = 1;
		networkManager.StartAsAClient ();

	}
		
}
