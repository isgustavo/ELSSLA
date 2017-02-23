using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBehaviour : MonoBehaviour {

	public GameObject target;
	
	void LateUpdate () {

		if (target != null) {
			Vector3 position = new Vector3 (target.transform.position.x ,
				                  target.transform.position.y ,
				                  -10f);
			
			transform.position = position;
		}
	}
}
