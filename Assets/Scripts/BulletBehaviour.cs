using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletBehaviour : NetworkBehaviour {
	
	private const float LIFETIME = 0.9f;
	private const float SPREAD_DELAY = 0.1f;
	private const float VELOCITY = 7.0f;
	private const float SPREED_VELOCITY = .5f;

	private Rigidbody rb;
	public string playerId { get; set;}
	private Quaternion rotTarget;
	private bool isSpread = false;

	public ParticleSystem explosion;

	void Start () {

		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.up * VELOCITY;

		StartCoroutine(Spread());
		Destroy(this.gameObject, LIFETIME);

	}

	void FixedUpdate () {

		rb.velocity = transform.up * VELOCITY;	

		if (isSpread) {
			
			rb.rotation = Quaternion.Lerp(rb.rotation, rotTarget, Time.deltaTime * SPREED_VELOCITY);
		}
	}

	void OnCollisionEnter(Collision collision) { 

		if (!isServer)
			return;

		gameObject.GetComponent<BoxCollider> ().enabled = false;

		Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation).Play();

		GameObject hit = collision.gameObject;
		Destructible obj = hit.GetComponent<Destructible> ();
		if (obj != null) {
			Debug.Log ("Collision" + playerId);
			PlayerBehaviour player = GameManagerBehaviour.instance.GetPlayer (playerId);
			player.score += obj.GetPoints ();
		}
			
		Destroy (gameObject);
	}


	IEnumerator Spread () {
		
		yield return new WaitForSeconds(SPREAD_DELAY);
		rotTarget = Random.rotation;
		isSpread = true;
	}
		
}