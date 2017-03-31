using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public delegate void PushFragmentDelegate (FragmentBehaviour[] fragment);

public class FragmentSpawnManager: MonoBehaviour {

	private const int TWO_PARTS_FRAGMENT_POOL_SIZE = 35;
	private const int THREE_PARTS_FRAGMENT_POOL_SIZE = 15;

	[SerializeField]
	private GameObject twoPartsFragment1;
	[SerializeField]
	private GameObject twoPartsFragment2;

	[SerializeField]
	private GameObject threePartsFragment1;
	[SerializeField]
	private GameObject threePartsFragment2;
	[SerializeField]
	private GameObject threePartsFragment3;

	private List<FragmentBehaviour[]> fragmentsPool = new List<FragmentBehaviour[]> ();
	private List<FragmentBehaviour[]> fragmentsInUse = new List<FragmentBehaviour[]> ();

	private AsteroidSpawnManager asteroidSpawnManager;

	void Start () {

		asteroidSpawnManager = GetComponent<AsteroidSpawnManager> ();

		for (int i = 0; i < TWO_PARTS_FRAGMENT_POOL_SIZE; i++) {

			GameObject twoPart1 = (GameObject)Instantiate(twoPartsFragment1);
			GameObject twoPart2 = (GameObject)Instantiate(twoPartsFragment2);

			twoPart1.transform.name = "twoPartsfragment" + i + "1";
			twoPart2.transform.name = "twoPartsfragment" + i + "2";

			FragmentBehaviour part1 = twoPart1.GetComponent<FragmentBehaviour>();
			part1.pushDelegate += new PushFragmentDelegate (this.Push);

			FragmentBehaviour part2 = twoPart2.GetComponent<FragmentBehaviour>();
			part2.pushDelegate += new PushFragmentDelegate (this.Push);

			fragmentsPool.Add(new FragmentBehaviour[2]{part1, part2});

			NetworkServer.Spawn (twoPart1);
			NetworkServer.Spawn (twoPart2);

			if (i < THREE_PARTS_FRAGMENT_POOL_SIZE) {

				GameObject threePart1 = (GameObject)Instantiate(threePartsFragment1);
				GameObject threePart2 = (GameObject)Instantiate(threePartsFragment2);
				GameObject threePart3 = (GameObject)Instantiate(threePartsFragment3);

				threePart1.transform.name = "threePartsfragment" + i + "1";
				threePart2.transform.name = "threePartsfragment" + i + "2";
				threePart3.transform.name = "threePartsfragment" + i + "3";

				FragmentBehaviour part31 = threePart1.GetComponent<FragmentBehaviour>();
				part31.pushDelegate += new PushFragmentDelegate (this.Push);

				FragmentBehaviour part32 = threePart2.GetComponent<FragmentBehaviour>();
				part32.pushDelegate += new PushFragmentDelegate (this.Push);

				FragmentBehaviour part33 = threePart3.GetComponent<FragmentBehaviour>();
				part33.pushDelegate += new PushFragmentDelegate (this.Push);

				fragmentsPool.Add(new FragmentBehaviour[3]{part31, part32, part33});

				NetworkServer.Spawn (threePart1);
				NetworkServer.Spawn (threePart2);
				NetworkServer.Spawn (threePart3);

			}
		}


	}

	void Update () {


		for (int i = 0; i < fragmentsInUse.Count; i++) {
			FragmentBehaviour[] fragment = fragmentsInUse[i];
			if(!fragment[0].inUse && !fragment[1].inUse && !(fragment.Length > 2 && fragment[2].inUse)) {
				Push (fragment);
				fragmentsInUse.RemoveAt(i);
			}


		}

	}

	void Push(FragmentBehaviour[] fragment) {

		if (fragmentsPool.Count >= 0) {
			fragmentsPool.Add (fragment);
			asteroidSpawnManager.SetAsteroidToUse ();
		}

	}

	FragmentBehaviour[] Pop () {

		if (fragmentsPool.Count >= 0) {

			FragmentBehaviour[] obj = fragmentsPool [0];
			fragmentsPool.RemoveAt (0);
			fragmentsInUse.Add (obj);
			return obj;
		}

		return null;
	}



	public void SetFragmentToUse (Vector3 position, Quaternion rotation) {

		FragmentBehaviour[] fragments = Pop ();
		if (fragments != null) {

			foreach (FragmentBehaviour fragment in fragments) {
				fragment.OnChangeUse (true, position, rotation);
			}
		}
	}

}