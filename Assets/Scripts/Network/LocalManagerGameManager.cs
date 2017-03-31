using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalManagerGameManager : NetworkBehaviour {

	[SerializeField]
	private GameObject localCameraPrefab;
	[SerializeField]
	private GameObject localStarsPrefab;
	[SerializeField]
	private GameObject localGuiPrefab;

	private GameObject localPlayer;

	void Update () {


		if (localPlayer == null) {

		    localPlayer = GameObject.FindGameObjectWithTag (PlayerBehaviour.LOCAL_PLAYER_TAG);
			if (localPlayer != null) {

				LocalCameraBehaviour localCamera = (Instantiate (localCameraPrefab)).GetComponent<LocalCameraBehaviour> ();
				localCamera.SetPlayer (localPlayer.transform);

				LocalStarsBehaviour localStars = (Instantiate (localStarsPrefab)).GetComponent<LocalStarsBehaviour> ();
				localStars.SetPlayer (localPlayer.transform);

				LocalGUIGameBehaviour localGui = (Instantiate (localGuiPrefab)).GetComponent<LocalGUIGameBehaviour> ();
				localGui.player = localPlayer.GetComponent<PlayerBehaviour> ();

			}
		}
	}
		
}
