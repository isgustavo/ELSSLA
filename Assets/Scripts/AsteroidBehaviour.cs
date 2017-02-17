using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AsteroidBehaviour : NetworkBehaviour {

	public const int maxHealth = 3;
	[SyncVar]
	public int currentHealth = maxHealth;

	public int _point = 100;

	private Renderer _renderer;
	private Rigidbody _rigidbody;
	public GameObject[] _asteroidPieces;
	public ParticleSystem _asteroidFragment;

	void Start () {

		_renderer = GetComponentInChildren<Renderer> ();
		_rigidbody = GetComponent<Rigidbody> ();
		//_rigidbody.velocity = transform.up * 0.5f;

	}

	public void TakeDamage(int amount) {

		if (!isServer) {
			return;
		}

		currentHealth -= amount;
		if (currentHealth <= 0) {
			currentHealth = 0;

			//gameObject.SetActive (false);
			RpcDestroy ();

			//((ParticleSystem) Instantiate (_asteroidFragment, transform.position, transform.rotation)).Play();
			//gameObject.SetActive (false);

		}
	}


	[ClientRpc]
	void RpcDestroy () {
		Debug.Log (gameObject);
		((ParticleSystem) Instantiate (_asteroidFragment, transform.position, transform.rotation)).Play();
		gameObject.SetActive (false);
		if (isLocalPlayer) {
			//transform.position = Vector3.zero;

			Debug.Log ("..."+gameObject.activeInHierarchy);

		}

	}

	void Update () {

		/*if (_hitPoints <= 0) {
			float i = 5;
			    _asteroidPieces [0].transform.position = new Vector3(transform.position.x+.2f, transform.position.y, transform.position.z);
				_asteroidPieces [0].transform.rotation = transform.rotation;
				_asteroidPieces [0].SetActive (true);

			_asteroidPieces [1].transform.position = new Vector3(transform.position.x-.2f, transform.position.y, transform.position.z);
				_asteroidPieces [1].transform.rotation = transform.rotation;
				_asteroidPieces [1].SetActive (true);

			if (_asteroidPieces.Length > 2) {
				_asteroidPieces [2].transform.position = transform.position;
				_asteroidPieces [2].transform.rotation = transform.rotation;
				_asteroidPieces [2].SetActive (true);
			} 


			CmdStartExplosion();

			gameObject.SetActive (false);

		} else {*/
			Vector3 newPosition = transform.position;

			if (transform.position.z > 0) {
				newPosition.z = Mathf.Lerp (transform.position.z, 0, Time.deltaTime * 4f);
			} else {
				newPosition.z = -0f;
			}
			transform.position = newPosition;
		//}

	}

	/*public void SetDamage(int damage) {
		Debug.Log ("set damage");
		//_hitPoints -= damage;

		CmdSetAsteroidDamange (damage);
	}


	[Command] 
	void CmdSetAsteroidDamange(int damage) {
		RpcSetAsteroidDamange (damage);
	}

	void RpcSetAsteroidDamange(int damange) {

		if (isLocalPlayer)
			return;

		_hitPoints -= damange;

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

	}*/
}
