using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AsteroidSpawnerBehaviour : NetworkBehaviour {

	public GameObject _asteroid;
	public GameObject _asteroid12;
	public GameObject _asteroid22;
	public GameObject _asteroid13;
	public GameObject _asteroid23;
	public GameObject _asteroid33;

	private const int POOL_SIZE = 1;
	private const int POOL_2_SIZE = 1;
	private const int POOL_3_SIZE = 4;
		
	private GameObject[] _asteroids = new GameObject[POOL_SIZE];

	public override void OnStartServer () {

		for (int x = 0; x < POOL_SIZE; x++) {

			//Vector3 position = new Vector3 (Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(6.0f, 10.0f));

			Vector3 position = new Vector3 (0, 2, Random.Range(6.0f, 10.0f));

			_asteroids [x] = (GameObject)Instantiate (_asteroid, position, Random.rotation);

			NetworkServer.Spawn (_asteroids [x]);

			/*if (x < POOL_2_SIZE) {
				GameObject part1 = Instantiate (_asteroid12);
				part1.SetActive (false);
				NetworkServer.Spawn (part1);
				GameObject part2 = Instantiate (_asteroid22);
				part2.SetActive (false);
				NetworkServer.Spawn (part2);

				_asteroids [x].GetComponent<AsteroidBehaviour>()._asteroidPieces = new GameObject[2] { part1, part2 };
			}

			/*if (x < POOL_3_SIZE) {
				GameObject part1 = Instantiate (_asteroid13);
				part1.SetActive (false);
				NetworkServer.Spawn (part1);
				GameObject part2 = Instantiate (_asteroid23);
				part2.SetActive (false);
				NetworkServer.Spawn (part2);
				GameObject part3 = Instantiate (_asteroid33);
				part3.SetActive (false);
				NetworkServer.Spawn (part3);

				_asteroids [x].GetComponent<AsteroidBehaviour> ()._asteroidPieces = new GameObject[3] { part1, part2, part3 };

			//}*/

		}
	}

}
