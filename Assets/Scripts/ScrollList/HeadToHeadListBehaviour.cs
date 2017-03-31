using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadToHeadListBehaviour : MonoBehaviour {

	public GameObject headToHeadCellPrefab;
	public GameObject headToHeadContainerPrefab;

	void Start () {

		if (LocalPlayerBehaviour.instance.GetHeadToHeadValuesAvailable ()) {

			foreach (HeadToHeadPlayer player in LocalPlayerBehaviour.instance.GetHeadToHeadListPlayers ()) {

				HeadToHeadListCellBehaviour cell = (Instantiate (headToHeadCellPrefab)).GetComponent<HeadToHeadListCellBehaviour> ();
				cell.gameObject.transform.parent = headToHeadContainerPrefab.transform;


				if (player.GetKills () > player.GetDeaths ()) {

					cell.SetRightPlayer (player.GetName (), player.GetKills ());
					cell.SetLeftPlayer (LocalPlayerBehaviour.instance.GetLocalPlayerName (), player.GetDeaths ());

					cell.SetValues (player.GetKills (), player.GetDeaths ());

				} else {
					
					cell.SetRightPlayer (LocalPlayerBehaviour.instance.GetLocalPlayerName (), player.GetDeaths ());
					cell.SetLeftPlayer (player.GetName (), player.GetKills ());

					cell.SetValues (player.GetDeaths (), player.GetKills ());
				}

			}
				
		}

	}

}
