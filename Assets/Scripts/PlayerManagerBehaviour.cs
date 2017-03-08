using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class PlayerManagerBehaviour : NetworkBehaviour {

	[SyncVar]
	public int score = 0; 
	public int highscore = 0;
	public int friend = 0;
	public int programmer = 0;
	[SyncVar (hook="OnUpdateStatus")]
	public bool isDead = false;

	public ParticleSystem explosion;

	private GameGUIBehaviour gameGUI;
	private DiedGUIBehaviour diedGUI;

	void Start () {

		score = 0;
		highscore = 345098;
		friend = 6;
		programmer = 10;

	}
		

	public void Score() {

		score += 100;
	}

	void OnUpdateStatus(bool value) {
		//Debug.Log (gameObject.name);
		isDead = value;
		if (isDead) {

			//call respaw scene
			//save status
			//call ads


			gameObject.SetActive (false);
			Instantiate (explosion, transform.position, transform.rotation).Play();

			if (isLocalPlayer) {
				gameGUI.SetDeactive ();
				diedGUI.SetActiveGUI (true);

			}
		} else {
			score = 0;
		}
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();

		CameraFollowBehaviour camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollowBehaviour>();
		camera.target = gameObject;

		gameGUI = GameObject.FindGameObjectWithTag ("GameGUI").GetComponent<GameGUIBehaviour>();
		gameGUI.player = this;
		gameGUI.SetActiveGUI (true);

		diedGUI = GameObject.FindGameObjectWithTag ("DiedGUI").GetComponent<DiedGUIBehaviour>();
		diedGUI.player = this;
		diedGUI.SetActiveGUI (false);

	}

}
