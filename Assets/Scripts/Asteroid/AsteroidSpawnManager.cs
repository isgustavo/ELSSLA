using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public delegate void PushDelegate (GameObject asteroid);

public class AsteroidSpawnManager : NetworkBehaviour {

	private int asteroidPoolSize = 0;

	public GameObject asteroidPrefab;
	private List<GameObject> asteroidPool = new List<GameObject> ();

	private FragmentSpawnManager fragmentSpawnManager;

	void Start() {

		fragmentSpawnManager = GetComponent<FragmentSpawnManager> ();

		for (int i = 0; i < asteroidPoolSize; i++) {

			GameObject obj = (GameObject)Instantiate(asteroidPrefab);
			obj.transform.name = "Asteroid" + i;
			obj.GetComponent<AsteroidBehaviour> ().pushDelegate += new PushDelegate (this.Push);

			NetworkServer.Spawn (obj);
		}
	}

	void Push(GameObject asteroid) {

		if (asteroidPool != null) {
			asteroidPool.Add (asteroid);
			fragmentSpawnManager.SetFragmentToUse (asteroid.transform.position, asteroid.transform.rotation);
		}
	}

	GameObject Pop () {
		
		if (asteroidPool.Count > 0) {

			GameObject obj = asteroidPool [0];
			asteroidPool.RemoveAt (0);
			return obj;
		}

		return null;
	}

	public void SetAsteroidToUse() {

		GameObject asteroid = Pop ();
		if (asteroid != null) {

			AsteroidBehaviour asteroidBehaviour = asteroid.GetComponent<AsteroidBehaviour> ();
			asteroidBehaviour.OnChangeUse (true);
		}
	}

}
