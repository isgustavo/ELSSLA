using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour {

	public GameObject _asteroid;
	public GameObject _asteroid12;
	public GameObject _asteroid22;
	public GameObject _asteroid13;
	public GameObject _asteroid23;
	public GameObject _asteroid33;

	private const int POOL_SIZE = 10;
	private GameObject[] _asteroids; 

	private const int POOL_2_PART_SIZE = 6;
	//private AsteroidBehaviour[] asteroidsPart1of2;
	//private AsteroidBehaviour[] asteroidsPart2of2;

	private const int POOL_3_PART_SIZE = 4;
	//private AsteroidBehaviour[] asteroidsPart1of3;
	//private AsteroidBehaviour[] asteroidsPart2of3;
	//private AsteroidBehaviour[] asteroidsPart3of3;

	public AsteroidPool (GameObject asteroid) {

		for (int x = 0; x < POOL_SIZE; x++) {
			_asteroids [x] = (GameObject)Instantiate (asteroid);

		}
	}


	void Start () {

		for (int x = 0; x < POOL_SIZE; x++) {
			_asteroids [x] = (GameObject)Instantiate (_asteroid);

			/*if (x < POOL_2_PART_SIZE) {
				asteroidsPart1of2 [x] = Instantiate (_asteroid12);
				asteroidsPart2of2 [x] = Instantiate (_asteroid22);
			}

			if (x < POOL_3_PART_SIZE) {

				asteroidsPart1of3 [x] = Instantiate (_asteroid13);
				asteroidsPart2of3 [x] = Instantiate (_asteroid23);
				asteroidsPart3of3 [x] = Instantiate (_asteroid33);
			}*/

		}

	}


	public void GetAsteroid () {

		for (int x = 0; x < POOL_SIZE; x++) {

			if (!_asteroids [x].activeInHierarchy) {
				//just an asteroid
				_asteroids [x].transform.position = transform.position;
				_asteroids [x].transform.rotation = transform.rotation;
				_asteroids [x].SetActive (true);	

				break;
			}

		}

	}
}
