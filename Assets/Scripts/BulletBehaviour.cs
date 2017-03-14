using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletBehaviour : NetworkBehaviour {

	private const float LIFETIME = 1.5f;
	private const float VELOCITY = 5.0f;

	private Rigidbody rb;
	public string playerId { get; set;}

	void Start () {
		
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.up * VELOCITY;

		Destroy(this.gameObject, LIFETIME);

	}

	void OnCollisionEnter(Collision collision) { 

		if (!isServer)
			return;

		gameObject.GetComponent<BoxCollider> ().enabled = false;


		GameObject hit = collision.gameObject;
		AsteroidBehaviour asteroid = hit.GetComponent<AsteroidBehaviour> ();
		if (asteroid != null) {

			PlayerBehaviour player = GameManagerBehaviour.instance.GetPlayer (playerId);
			player.score += 100;
		}

		Destroy (gameObject);
	}
		
}