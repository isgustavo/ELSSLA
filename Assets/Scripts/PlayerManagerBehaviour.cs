using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class PlayerManagerBehaviour : NetworkBehaviour {

	[SyncVar (hook="OnUpdateScore")]
	public int score = 0; 
	[SyncVar (hook="OnUpdateStatus")]
	public bool isDead = false;

	public TextMesh scoreGUI;

	void Start () {

		scoreGUI = GameObject.Find ("Score").GetComponent<TextMesh> ();
		score = 0; 

	}


	void OnUpdateScore(int value) {
		
		score += value;

		Debug.Log ("hook");
		if (isLocalPlayer) {
			scoreGUI.text = score.ToString();
		}

	}

	void OnUpdateStatus(bool value) {

		isDead = value;
		if (isDead) {

			//call respaw scene
			//save status
			//call ads

			gameObject.SetActive (false);
		} else {


			
			score = 0;
		}
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();

		CameraFollowBehaviour camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollowBehaviour>();
		camera.target = gameObject;
	}
}
