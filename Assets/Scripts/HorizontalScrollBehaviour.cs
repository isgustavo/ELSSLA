using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalScrollBehaviour : MonoBehaviour {

	[SerializeField]
	private RectTransform container;
	public RectTransform[] cards;
	public Image[] dots;

	private bool isDragging = false;
	private bool isLerp = false;
	private float lerpTo;
	private int currentCard;
	public int CurrentCard {
		get { return currentCard; } 
		set { 
			currentCard = value;
			for (int i = 0; i < dots.Length; i++) {
				if (currentCard == i) {
					dots [i].color = Color.white;
				} else {
					dots [i].color = Color.gray;
				}
			}
		}
	} 

	void Update () {
		if (isLerp) {

			float newx = Mathf.Lerp (container.anchoredPosition.x, lerpTo , Time.deltaTime * 10f);
			Vector2 newPosition = new Vector2 (newx, container.anchoredPosition.y);
			container.anchoredPosition = newPosition;
		}

		container.anchoredPosition = new Vector2 (container.anchoredPosition.x, 0f);
	}


	void LerpToCard (int card) {

		CurrentCard = card;
		lerpTo = -cards [card].anchoredPosition.x;
		isLerp = true;

	}

	int GetNearestCard () {
		float currentPosition = container.anchoredPosition.x;

		// if first card, just put back for initial position
		if (currentPosition > 0) {
			return 0;
		}

		float distance = float.MaxValue;
		int nearestCard = 0;


		for (int i = 0; i < cards.Length; i++) {
			float testDistance = Mathf.Abs(cards [i].anchoredPosition.x - Mathf.Abs(currentPosition));
			if (testDistance < distance) {

				distance = testDistance;
				nearestCard = i;
			}
		}

		return nearestCard;
	}

	public void OnDrag () {
		Debug.Log ("Drag");
		isLerp = false;
		isDragging = true;

	}

	public void OnEndDrag () {

		isDragging = false;

		LerpToCard (GetNearestCard());

	}
}
