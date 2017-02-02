using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class LocalPlayer : Camera2DFollow {

	// Use this for initialization
	void Start () {

		Vector3 oldAngles = this.transform.eulerAngles;
		this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + 148);
	}
	
	// Update is called once per frame
	void Update () {



		Debug.Log ("update");
		transform.position += transform.up * Time.deltaTime * 3;
	}
}
