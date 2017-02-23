using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionBehaviour : MonoBehaviour {




	void OnCollisionEnter(Collision collision) { 


		GameObject hit = collision.gameObject;

		if (hit.GetComponent<BulletBehaviour> () != null) {
			Destroy (hit);
			return;
		}


		if (hit.GetComponent<PlayerManagerBehaviour> () != null) {

			hit.GetComponent<PlayerManagerBehaviour> ().isDead = true;
		}

		BulletBehaviour bullet = gameObject.GetComponent<BulletBehaviour> ();

		PlayerManagerBehaviour player = GameObject.Find (bullet.playerId).GetComponent<PlayerManagerBehaviour> ();
		player.score = 100;

		Destroy (gameObject);

	}

}
