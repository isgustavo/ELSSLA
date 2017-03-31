using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public delegate void PushDelegate (AsteroidBehaviour asteroid);

public class AsteroidSpawnManager : NetworkBehaviour {

	private const int ASTEROID_POOL_SIZE = 30;

	public GameObject asteroidPrefab;
	private List<AsteroidBehaviour> asteroidPool = new List<AsteroidBehaviour> ();

	private FragmentSpawnManager fragmentSpawnManager;

	void Start() {

		fragmentSpawnManager = GetComponent<FragmentSpawnManager> ();

		for (int i = 0; i < ASTEROID_POOL_SIZE; i++) {

			GameObject asteroid = (GameObject)Instantiate (asteroidPrefab);
			asteroid.transform.name = "Asteroid" + i;
			asteroid.GetComponent<AsteroidBehaviour> ().pushDelegate += new PushDelegate (this.Push);

			NetworkServer.Spawn (asteroid);
		}
	}

	public void Push (AsteroidBehaviour asteroid) {

		if (asteroidPool != null) {
			asteroidPool.Add (asteroid);
			fragmentSpawnManager.SetFragmentToUse (asteroid.transform.position, asteroid.transform.rotation);
		}
	}

	public AsteroidBehaviour Pop () {
		
		if (asteroidPool.Count > 0) {

			AsteroidBehaviour asteroid = asteroidPool [0];
			asteroidPool.RemoveAt (0);
			return asteroid;
		}

		return null;
	}

	public void SetAsteroidToUse () {
		
		AsteroidBehaviour asteroid = Pop ();
		if (asteroid != null) {
			
			asteroid.OnChangeUse (true);
		}
	}

}
