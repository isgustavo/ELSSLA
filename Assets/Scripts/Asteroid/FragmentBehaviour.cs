using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FragmentBehaviour : MonoBehaviour {

	//[SyncVar(hook = "OnChangeUse")]
	public bool inUse = false;
	private Vector3 noUsePosition = new Vector3(0, 0, -20f);
	public Vector3 startPosition = new Vector3 (-0.254f, 0, 0);

	public NetworkIdentity identity;

	private Rigidbody _rigidbory;
	[SerializeField]
	private BoxCollider _collider;

	void Start () {

		Debug.Log ("Start" + identity.isServer);
		if (!identity.isServer)
			return;
		
		_rigidbory = GetComponent<Rigidbody> ();
		//_collider = GetComponent<BoxCollider> ();
	}

	void OnCollisionEnter(Collision collision) {

		if (!identity.isServer)
			return;

		_collider.enabled = false;

		OnChangeUse (false);
	}

	public void OnChangeUse (bool value) {

		inUse = value; 

		if (inUse == false) {
			
			transform.position = noUsePosition;
			_rigidbory.velocity = Vector3.zero;

		} else {
			Debug.Log ("fragment on change use: "+value);
			transform.position = startPosition;
			transform.rotation = Quaternion.identity;
			//_rigidbory.velocity = new Vector3 (0.05f, 0, 0);
			_collider.enabled = true;
		}
	}

}

/*



/*


	//[SyncVar(hook = "OnChangePosition")]
	//public Vector3 position;

	public GameObject fragment1;
	public GameObject fragment2;

	public void OnChangeUse (bool value) {

		Debug.Log ("isLocalPlayer:" + isLocalPlayer);
		Debug.Log ("isClient:" + isClient);
		Debug.Log ("isServer:" + isServer);

		if (isClient) {
			// move back to zero location
			//transform.position = Vector3.zero;
			fragment1.GetComponent<Collider> ().enabled = true;
			fragment2.GetComponent<Collider> ().enabled = true;

		}

	}

	//public void OnChangePosition(Vector3 position) {

	//	if (isClient) {


	//		transform
	//	}
	//}


	void Start () {
		fragment1.GetComponent<Collider> ().enabled = false;
		fragment2.GetComponent<Collider> ().enabled = false;


	}


	void Update () {

		//if (!canUse) {
		//	fragment1.transform.position = new Vector3 (-1, 0, 0);
		//	fragment2.transform.position = new Vector3 (1, 0, 0);
		//	fragment1.transform.rotation = Quaternion.identity;
		//	fragment2.transform.rotation = Quaternion.identity;



		//}

	}

	[ClientRpc]
	public void RpcTest (Vector3 position) {

		gameObject.transform.position = position;
		gameObject.SetActive (true);
	}
}*/
