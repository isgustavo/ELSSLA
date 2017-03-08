using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FragmentManagerBehaviour: NetworkBehaviour {

	public bool inUse = false;
	private Vector3 noUsePosition = new Vector3(0, 0, -20f);

	public event PushDelegate pushDelegate;

	public GameObject[] fragments;

	void Awake () {

		transform.position = noUsePosition;
		transform.rotation = Quaternion.identity;

	}

	void Update () {

		if (inUse) {
			foreach (GameObject fragment in fragments) {

				if (fragment.GetComponent<FragmentBehaviour> ().inUse) {
					return;
				}
			}

			OnChangeUse (false, noUsePosition, Quaternion.identity);
		}
	}

	public void OnChangeUse (bool value, Vector3 position, Quaternion rotation) {
		
		if (!isServer)
			return;

		if (value == false) {
			pushDelegate (gameObject);
			transform.position = position;


		} else {

			transform.position = position;
			transform.rotation = rotation;
			foreach (var frag in fragments) {

				frag.GetComponent<FragmentBehaviour>().OnChangeUse (value);
			}
		}

		inUse = value; 

	}

}

