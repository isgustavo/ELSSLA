using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FragmentSpawnManager : NetworkBehaviour {

	private int twoPartsfragmentPoolSize = 6;
	private int threePartsfragmentPoolSize = 0;
	private Vector3 noUsePosition = new Vector3(0, 0, -20);

	public GameObject twoPartsFragmentPrefab;
	public GameObject threePartsFragmentPrefab;
	public List<GameObject> fragmentsPool = new List<GameObject> ();

	void Start () {

		for (int i = 0; i < twoPartsfragmentPoolSize; i++) {
			
			GameObject twoParts = (GameObject)Instantiate(twoPartsFragmentPrefab, noUsePosition, Quaternion.identity);
			twoParts.transform.name = "twoPartsfragment" + i;
			twoParts.GetComponent<FragmentManagerBehaviour> ().pushDelegate += new PushDelegate (this.Push);
			fragmentsPool.Add(twoParts);

			NetworkServer.Spawn (twoParts);

			if (i < threePartsfragmentPoolSize) {

				GameObject threeParts = (GameObject)Instantiate(threePartsFragmentPrefab, noUsePosition, Quaternion.identity);
				threeParts.transform.name = "threePartsfragment" + i;
				threeParts.GetComponent<FragmentManagerBehaviour> ().pushDelegate += new PushDelegate (this.Push);
				fragmentsPool.Add(threeParts);

				NetworkServer.Spawn (threeParts);

			}
		}
	}

	public bool ExistFragmentAvailable(){

		return fragmentsPool.Count == (twoPartsfragmentPoolSize + threePartsfragmentPoolSize) ? true : false;

	}

	void Push(GameObject fragment) {
		//Debug.Log ("push");
		if (fragmentsPool != null) {

			fragmentsPool.Add (fragment);
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

	public void SetFragmentToUse (Vector3 position) {

		GameObject fragment = Pop ();
		if (fragment != null) {
			fragment.transform.position = position;

			FragmentManagerBehaviour fragmentManager = fragment.GetComponent<FragmentManagerBehaviour> ();
			fragmentManager.OnChangeUse (true);
		}
	}
		
}