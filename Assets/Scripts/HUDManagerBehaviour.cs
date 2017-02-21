using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObserverBehaviour : MonoBehaviour {
	public abstract void OnNotify ();
}

public class HUDManagerBehaviour : ObserverBehaviour {

	public GameObject tapToPlay;
	public GameObject tapToJoin;


	public override void OnNotify () {

		tapToPlay.gameObject.SetActive(false);
		tapToJoin.gameObject.SetActive(true);

	}

}
