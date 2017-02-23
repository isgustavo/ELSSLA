using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletTrasnformBehaviour : NetworkBehaviour {

	private Rigidbody _rigidbody;
	private Quaternion rotTarget;

	private float lifetime = 3.0f;
	private float speed = 6f;

	private bool isSpread = false;
	private float spreadSpeed = 2f;
	private float spreadDelay = 2.0f;

	void Start () {
		Debug.Log ("bullet Start");
		_rigidbody = GetComponent<Rigidbody> ();
		rotTarget = _rigidbody.rotation;

		StartCoroutine("Spread");
		//StartCoroutine("DestroyRocket");

		Destroy(this.gameObject, lifetime);

	}

	void FixedUpdate () {
		_rigidbody.velocity = transform.up * speed;

		if (isSpread) {
			_rigidbody.rotation = Quaternion.Lerp (_rigidbody.rotation, rotTarget, Time.deltaTime * spreadSpeed);	
		}							
	}


	public IEnumerator Spread () {
		yield return new WaitForSeconds(spreadDelay);

		rotTarget = Random.rotation;					
		isSpread = true;							
	}

}
