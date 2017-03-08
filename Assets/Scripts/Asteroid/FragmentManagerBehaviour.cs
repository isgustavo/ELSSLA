using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FragmentManagerBehaviour: NetworkBehaviour {

	//[SyncVar]
	public bool inUse = false;
	private Vector3 noUsePosition = new Vector3(0, 0, -20f);

	public event PushDelegate pushDelegate;

	public GameObject[] fragments;

	void Update () {

		if (inUse) {
			foreach (GameObject fragment in fragments) {

				if (fragment.GetComponent<FragmentBehaviour> ().inUse) {
					return;
				}
			}

			OnChangeUse (false);
		}
	}

	public void OnChangeUse (bool value) {
		Debug.Log ("FragmentManager cahnge Use");
		if (!isServer)
			return;

		if (value == false) {
			transform.position = noUsePosition;
			pushDelegate (gameObject);

		} else if (value == true) {

			foreach (var frag in fragments) {

				frag.GetComponent<FragmentBehaviour>().OnChangeUse (value);
			}
		}

		inUse = value; 

	}

}

