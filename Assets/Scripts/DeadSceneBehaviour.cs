using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSceneBehaviour : MonoBehaviour {

	[SerializeField]
	private TextMesh score;
	private int _score;
	[SerializeField]
	private TextMesh highscore;
	private int _highscore;
	[SerializeField]
	private TextMesh friendScore;
	private int _friendScore;
	[SerializeField]
	private TextMesh programmerScore;
	private int _programmerScore;


	void Update () {

		if (9504 > _score) {

			_score += Mathf.CeilToInt((9504-_score)*.1f);
			score.text = _score.ToString("000000000");

		}

		if (2346109 > _highscore) {

			_highscore += Mathf.CeilToInt((2346109-_highscore)*.1f);
			highscore.text = _highscore.ToString("000000000");

		}

		if (5 > _friendScore) {

			_friendScore += Mathf.CeilToInt((5-_friendScore)*.1f);
			friendScore.text = _friendScore.ToString("00000000") + "x";

		}

		if (9 > _programmerScore) {

			_programmerScore += Mathf.CeilToInt((9-_programmerScore)*.1f);
			programmerScore.text = _programmerScore.ToString("00000000") + "x";

		}

	}
}
