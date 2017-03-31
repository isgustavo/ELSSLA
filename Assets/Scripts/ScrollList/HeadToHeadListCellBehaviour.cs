using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadToHeadListCellBehaviour : MonoBehaviour {


	[SerializeField]
	private Text rightName;

	[SerializeField]
	private Text leftName;
	[SerializeField]
	private Text headToHeadValue;


	public void SetRightPlayer (string name, int value) {

		rightName.text = name;

	}


	public void SetLeftPlayer (string name, int value) {

		leftName.text = name;
	}

	public void SetValues (int rightValue, int leftValue) {

		headToHeadValue.text = rightValue + " vs " + leftValue;

	}






}
