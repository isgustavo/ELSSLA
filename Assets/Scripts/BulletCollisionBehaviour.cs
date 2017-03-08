using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletCollisionBehaviour : NetworkBehaviour {

	void OnCollisionEnter(Collision collision) { 

		gameObject.GetComponent<BoxCollider> ().enabled = false;

		/*GameObject hit = collision.gameObject;

		if (hit.GetComponent<BulletBehaviour> () != null) {
			Destroy (hit);
			return;
		}

	
		Asteroid a = hit.GetComponent<Asteroid> ();
		if (a != null) {
			hit.SetActive (false);
		}


		AsteroidDestroyBehaviour asteroid2 = hit.GetComponentInParent<AsteroidDestroyBehaviour> ();
		if (asteroid2 != null) {

			asteroid2.Destroy ();
			//RpcDestroyAsteroid (hit);
		}

		AsteroidFragmentDestroyBehaviour fragment = hit.GetComponentInParent<AsteroidFragmentDestroyBehaviour> ();
		if (fragment != null) {

			hit.SetActive(false);
		}

		if (hit.GetComponent<PlayerManagerBehaviour> () != null) {

			hit.GetComponent<PlayerManagerBehaviour> ().isDead = true;
		}*/

		BulletBehaviour bullet = gameObject.GetComponent<BulletBehaviour> ();

//		PlayerManagerBehaviour player = GameObject.Find (bullet.playerId).GetComponent<PlayerManagerBehaviour> ();
		//player.Score ();

		Destroy (gameObject);

	}
		

}
