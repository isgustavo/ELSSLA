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

	private const int POOL_SIZE = 10;
	private const int POOL_2_SIZE = 6;
	private const int POOL_3_SIZE = 4;
		
	private GameObject[] _asteroids;
	private GameObject[,] _fragments2;
	private GameObject[,] _fragments3;

	public override void OnStartServer () {

		_asteroids = new GameObject[POOL_SIZE];
		_fragments2 = new GameObject[POOL_2_SIZE , 2];
		_fragments3 = new GameObject[POOL_3_SIZE , 3];

		for (int x = 0; x < POOL_SIZE; x++) {

			Vector3 position = new Vector3 (Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(6.0f, 10.0f));

			_asteroids [x] = (GameObject)Instantiate (_asteroid, position, Random.rotation);
			NetworkServer.Spawn (_asteroids [x]);

			if (x < POOL_2_SIZE) {
				_fragments2 [x, 0] = Instantiate (_asteroid12);
				_fragments2 [x, 0].SetActive (false);
				NetworkServer.Spawn (_fragments2 [x, 0]);
				_fragments2 [x, 1] = Instantiate (_asteroid22);
				_fragments2 [x, 1].SetActive (false);
				NetworkServer.Spawn (_fragments2 [x, 1]);
			}

			if (x < POOL_3_SIZE) {
				_fragments3 [x, 0] = Instantiate (_asteroid13);
				_fragments3 [x, 0].SetActive (false);
				NetworkServer.Spawn (_fragments3 [x, 0]);
				_fragments3 [x, 1] = Instantiate (_asteroid23);
				_fragments3 [x, 1].SetActive (false);
				NetworkServer.Spawn (_fragments3 [x, 1]);
				_fragments3 [x, 2] = Instantiate (_asteroid33);
				_fragments3 [x, 2].SetActive (false);
				NetworkServer.Spawn (_fragments3 [x, 2]);
			}

		}
	}


	 

}
