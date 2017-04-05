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

	private PlayerBehaviour localPlayer;

	void Update () {


		if (localPlayer == null) {

			localPlayer = GameObject.FindGameObjectWithTag (PlayerBehaviour.LOCAL_PLAYER_TAG).GetComponent<PlayerBehaviour> ();
			if (localPlayer != null) {


				LocalCameraBehaviour localCamera = (Instantiate (localCameraPrefab)).GetComponent<LocalCameraBehaviour> ();
				localCamera.SetPlayer (localPlayer);

				LocalStarsBehaviour localStars = (Instantiate (localStarsPrefab)).GetComponent<LocalStarsBehaviour> ();
				localStars.SetPlayer (localPlayer.transform);

				LocalGUIGameBehaviour localGui = (Instantiate (localGuiPrefab)).GetComponent<LocalGUIGameBehaviour> ();
				localGui.player = localPlayer;

			}
		}
	}
		
}
