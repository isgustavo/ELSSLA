﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayBehaviour : MonoBehaviour {


	void OnMouseUp() {
		gameObject.SetActive (false);
		GameManagerBehaviour.Instance.StartAsAHost ();
	}
}