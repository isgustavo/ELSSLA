using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FragmentSpawnManager : NetworkBehaviour {

	private int twoPartsfragmentPoolSize = 8;
	private int threePartsfragmentPoolSize = 4;

	public GameObject twoPartsFragmentPrefab;
	public GameObject threePartsFragmentPrefab;

	private List<GameObject> fragmentsPool = new List<GameObject> ();

	private AsteroidSpawnManager asteroidSpawnManager;

	void Start () {

		asteroidSpawnManager = GetComponent<AsteroidSpawnManager> ();

		for (int i = 0; i < twoPartsfragmentPoolSize; i++) {
			
			GameObject twoParts = (GameObject)Instantiate(twoPartsFragmentPrefab);
			twoParts.transform.name = "twoPartsfragment" + i;
			twoParts.GetComponent<FragmentManagerBehaviour> ().pushDelegate += new PushDelegate (this.Push);

			fragmentsPool.Add(twoParts);

			NetworkServer.Spawn (twoParts);

			if (i < threePartsfragmentPoolSize) {

				GameObject threeParts = (GameObject)Instantiate(threePartsFragmentPrefab);
				threeParts.transform.name = "threePartsfragment" + i;
				threeParts.GetComponent<FragmentManagerBehaviour> ().pushDelegate += new PushDelegate (this.Push);

				fragmentsPool.Add(threeParts);

				NetworkServer.Spawn (threeParts);

			}
		}
	}

	void Push(GameObject fragment) {

		if (fragmentsPool.Count > 0) {
			fragmentsPool.Add (fragment);
			asteroidSpawnManager.SetAsteroidToUse ();
		}

	}

	GameObject Pop () {

		if (fragmentsPool.Count > 0) {

			GameObject obj = fragmentsPool [0];
			fragmentsPool.RemoveAt (0);
			return obj;
		}

		return null;
	}

	public bool ExistFragmentAvailable(){

		return fragmentsPool.Count == (twoPartsfragmentPoolSize + threePartsfragmentPoolSize) ? true : false;

	}

	public void SetFragmentToUse (Vector3 position, Quaternion rotation) {

		GameObject fragment = Pop ();
		if (fragment != null) {

			FragmentManagerBehaviour fragmentManager = fragment.GetComponent<FragmentManagerBehaviour> ();
			fragmentManager.OnChangeUse (true, position, rotation);
		}
	}
		
}