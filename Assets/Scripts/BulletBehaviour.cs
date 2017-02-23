using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletBehaviour : NetworkBehaviour {

	[SyncVar]
	public string playerId;






/*	private Rigidbody _rigidbody;
	//private ParticleSystem _rocket;
	public int damage = 3;

	public float spreadDelay = 2f;
	public float _spreadAmount = .5f;
	public bool spread = false;
	private Quaternion rotTarget;

	public float bulletSpeed = 10f;

	public float lifetime = 3.0f;
	public float speed = 5.0f;

	void Start () {
		Debug.Log ("bullet Start");
		_rigidbody = GetComponent<Rigidbody> ();
		rotTarget = _rigidbody.rotation;

		StartCoroutine("Spread");
		StartCoroutine("DestroyRocket");

		Destroy(this.gameObject, lifetime + .75f);
				
	
	}


	/*void Update () {


		if (isServer) {
			Debug.Log ("isServer");
		} else {
			Debug.Log ("client");
		}
		transform.Translate (Vector3.up * Time.deltaTime * speed);
		//transform.position += transform.forward * Time.deltaTime * speed;
	}

	void FixedUpdate () {
		_rigidbody.velocity = transform.up * bulletSpeed;

		if (spread) {
			_rigidbody.rotation = Quaternion.Lerp (_rigidbody.rotation, rotTarget, Time.deltaTime * _spreadAmount);	
		}							
	}


	public IEnumerator Spread () {
		yield return new WaitForSeconds(spreadDelay);

		rotTarget = Random.rotation;					
		spread = true;							
	}

	public IEnumerator DestroyRocket () {
		yield return new WaitForSeconds(lifetime);
		//_rocket.enableEmission = false;
		GetComponent<Collider> ().enabled = false;
	}
		

	void OnCollisionEnter(Collision collision) { 
	

		GameObject hit = collision.gameObject;
		AsteroidBehaviour asteroid = hit.GetComponent<AsteroidBehaviour>();
		if (asteroid != null)
		{
			//asteroid.TakeDamage(damage);
		}

		Destroy (gameObject);

	}*/
}
