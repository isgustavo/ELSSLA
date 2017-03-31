using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighscoreListBehaviour : MonoBehaviour {

	public GameObject highscoreCellPrefab;
	public GameObject highscoreContainerPrefab;

	void Start () {

		if (LocalPlayerBehaviour.instance.GetHighscoreValuesAvailable ()) {

			int position = 1;
			foreach (HighscorePlayer player in LocalPlayerBehaviour.instance.GetHighscorePlayersList ()) {

				HighscoreListCellBehaviour cell = (Instantiate (highscoreCellPrefab)).GetComponent<HighscoreListCellBehaviour> ();
				cell.gameObject.transform.parent = highscoreContainerPrefab.transform;

				cell.SetPosition (position);
				cell.SetName (player.GetName ());
				cell.SetHighscore (player.GetHighscore ());
				//cell.SetImage ();

				position += 1;
			}


		}

	}

}
