using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBehaviour : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

}
