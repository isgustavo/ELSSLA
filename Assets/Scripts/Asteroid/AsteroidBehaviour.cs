using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AsteroidBehaviour : NetworkBehaviour {

	//[SyncVar(hook = "OnChangeUse")]
	public bool inUse = true;
	private Vector3 noUsePosition = new Vector3(0, 0, -20);

	public event PushDelegate pushDelegate;

	private Rigidbody _rigidbory;
	private SphereCollider _collider;


	void Start () {

		if (!isServer)
			return;
		
		_rigidbory = gameObject.GetComponent<Rigidbody> ();
		_collider = gameObject.GetComponent<SphereCollider> ();
	}


	void OnCollisionEnter(Collision collision) {

		if (!isServer)
			return;

		GameObject hit = collision.gameObject;
		if (hit.GetComponent<BulletBehaviour> () != null) {
			_collider.enabled = false;

			OnChangeUse (false);
		}
			
	}
		
	public void OnChangeUse (bool value) {

		if (!isServer)
			return;
		
		inUse = value; 

		if (inUse == false) {
			pushDelegate (gameObject);
			transform.position = noUsePosition;
			_rigidbory.velocity = Vector3.zero;


		} else if (value == true) {

			transform.position = new Vector3 (0, 0, -6);
			_collider.enabled = true;
		}
	}
		
}
