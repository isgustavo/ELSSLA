using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public delegate void PushDelegate (GameObject asteroid);

public class AsteroidSpawnManager : NetworkBehaviour {

	private int asteroidPoolSize = 12;
	private float timeBetweenAsteroids = 20f;
	private float timeTilNextAsteroid = 0.0f;

	public GameObject asteroidPrefab;
	public List<GameObject> asteroidPool = new List<GameObject> ();

	private FragmentSpawnManager fragmentSpawnManager;

	void Start() {

		fragmentSpawnManager = GetComponent<FragmentSpawnManager> ();

		for (int i = 0; i < asteroidPoolSize; i++) {

			GameObject obj = (GameObject)Instantiate(asteroidPrefab, new Vector3(0, i, -6), Quaternion.identity);
			obj.transform.name = "Asteroid" + i;
			obj.GetComponent<AsteroidBehaviour> ().pushDelegate += new PushDelegate (this.Push);

			NetworkServer.Spawn (obj);
		}
	}

	void Update () {

		if (!isServer) {
			return;
		}
			
		//TODO 
		if (timeTilNextAsteroid < 0f && asteroidPool.Count > 0 && fragmentSpawnManager.ExistFragmentAvailable ()) {
			GameObject asteroid = null;// Pop ();
				
			if (asteroid != null) {
				asteroid.GetComponent<AsteroidBehaviour> ().OnChangeUse (true);
				timeTilNextAsteroid = timeBetweenAsteroids;
			}
		} 
		timeTilNextAsteroid -= Time.deltaTime;

	}


	void Push(GameObject asteroid) {
		
		asteroidPool.Add (asteroid);
		//fragmentSpawnManager.SetFragmentToUse (asteroid.transform.position);

	}

	GameObject Pop () {
		
		//if (asteroidPool.Count > 0) {

			GameObject obj = asteroidPool [0];
			asteroidPool.RemoveAt (0);
			return obj;
		//}

		//return null;
	}

}
