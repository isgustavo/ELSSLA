using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToJoinBehaviour : MonoBehaviour {

	void OnMouseUp() {
		GameManagerBehaviour.Instance.StartAsAClient ();
	}
}
