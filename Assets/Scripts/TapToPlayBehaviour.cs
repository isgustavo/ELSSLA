using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayBehaviour : MonoBehaviour {

	[SerializeField]
	private NetworkManagerBehaviour networkManager;

	void OnMouseUp() {
		networkManager.StartAsAHost ();

	}
		
		
}
