using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AsteroidBehaviour : NetworkBehaviour {

	public int _hitPoints = 3;
	public int _point = 100;

	private Renderer _renderer;
	private Rigidbody _rigidbody;
	public GameObject[] _asteroidPieces;
	public ParticleSystem _asteroidFragment;

	void Start () {

		_renderer = GetComponentInChildren<Renderer> ();
		_rigidbody = GetComponent<Rigidbody> ();
		_rigidbody.velocity = transform.up * 0.5f;

	}

	void Update () {


		if (_hitPoints <= 0) {
			//int asteroidFragmentsCount = Random.Range (20, 25);

			//_asteroidFragment.transform.position = transform.position;
			//ParticleSystem.MainModule  mm = _asteroidFragment.main;
			//mm.startColor = _renderer.sharedMaterial.color;


			//CmdExplosion();
			//_asteroidFragment.Play();

			//set point
			CmdStartExplosion();

			gameObject.SetActive (false);

		} else {
			Vector3 newPosition = transform.position;

			if (transform.position.z > 0) {
				newPosition.z = Mathf.Lerp (transform.position.z, 0, Time.deltaTime * 4f);
			} else {
				newPosition.z = -0f;
			}
			transform.position = newPosition;
		}

	}

	public void SetDamage(int damage) {
		Debug.Log ("set damage");
		_hitPoints -= damage;
	}

	[Command]
	void CmdStartExplosion() {

		//ParticleSystem obj = Instantiate (_asteroidFragment, transform.position, transform.rotation);

		//NetworkServer.Spawn (_asteroidFragment);

		//_asteroidFragment.Play ();
		RpcStartExplosion ();

	}

	[ClientRpc]
	void RpcStartExplosion() { 

		if(isLocalPlayer)
			return;

		((ParticleSystem) Instantiate (_asteroidFragment, transform.position, transform.rotation)).Play();

	}
}
