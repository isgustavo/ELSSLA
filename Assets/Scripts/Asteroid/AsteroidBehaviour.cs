using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public interface Destructible {

	int GetPoints ();
}

public class AsteroidBehaviour : NetworkBehaviour, Destructible {

	private const int POINTS = 100;

	public bool inUse = true;
	private Vector3 noUsePosition = new Vector3(0, 0, -20);

	public event PushDelegate pushDelegate;

	private Rigidbody _rigidbory;
	private SphereCollider _collider;

	void Awake () {

		transform.position = AsteroidUtils.GetRandomPosition ();
		transform.rotation = Random.rotation;

	}

	void Start () {

		if (!isServer)
			return;
		
		_rigidbory = gameObject.GetComponent<Rigidbody> ();
		_rigidbory.velocity = AsteroidUtils.GetRandomVelocity ();
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


		} else {

			transform.position = AsteroidUtils.GetRandomPosition ();
			_rigidbory.velocity = AsteroidUtils.GetRandomVelocity ();
			_collider.enabled = true;
		}
	}

	public int GetPoints () {

		return POINTS;
	}

		
}
