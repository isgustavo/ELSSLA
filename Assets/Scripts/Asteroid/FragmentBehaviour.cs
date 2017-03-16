using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FragmentBehaviour : MonoBehaviour, Destructible {

	private const int POINTS = 50;

	public bool inUse = false;
	public int velocityDirection;

	private Vector3 noUsePosition = new Vector3(0, 0, -20);
	public Vector3 startPosition;
	public Vector3 startRotation;

	public NetworkIdentity identity;

	private Rigidbody _rigidbory;
	private BoxCollider _collider;

	void Start () {

		if (!identity.isServer)
			return;
		
		_rigidbory = GetComponent<Rigidbody> ();
		_collider = GetComponent<BoxCollider> ();
	}

	void OnCollisionEnter(Collision collision) {

		if (!identity.isServer)
			return;

		GameObject hit = collision.gameObject;
		if (hit.GetComponent<BulletBehaviour> () != null) {
			_collider.enabled = false;

			OnChangeUse (false);
		}

	}


	public void OnChangeUse (bool value) {

		if (!identity.isServer)
			return;
		
		inUse = value; 

		if (inUse == false) {
			
			transform.position = noUsePosition;
			_rigidbory.velocity = Vector3.zero;

		} else { 
			
			transform.localPosition = startPosition;
			transform.localRotation.SetLookRotation(startRotation);
			if (velocityDirection == 1) {
				_rigidbory.velocity = Vector3.right;
			} else if (velocityDirection == 0) {
				_rigidbory.velocity = Vector3.up;
			} else {
				_rigidbory.velocity = Vector3.left;
			}
				_collider.enabled = true;
		}
	}

	public int GetPoints () {

		return POINTS;
	}

}