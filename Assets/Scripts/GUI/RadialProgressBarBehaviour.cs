using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBarBehaviour : GuiBehaviour {

	[SerializeField]
	private Text timerText;
	[SerializeField]
	private Image loadingBar;
	[SerializeField]
	private Image centerBar;

	private float timeToHold = 0f;
	private float timeSpent;
	private bool isHold;

	void Start () {

		ResetProgressBar ();
	}
		
	void Update () {

		if (this.isHold) {
			this.timeSpent += Time.deltaTime;

			this.loadingBar.fillAmount = this.timeSpent / this.timeToHold;
			timerText.text = ((int)(this.timeToHold - this.timeSpent) + 1).ToString ();
		}

	}

	public void SetTimeToHold (float value) {

		this.timeToHold = value;
	}

	public void ResetProgressBar () {

		FadeToZeroAlpha();

		this.loadingBar.fillAmount = 0;
		this.timerText.text = timeToHold.ToString ();
		this.timeSpent = 0f;
	}

	public void IsHold (bool value) {

		this.isHold = value;

		if (value) {

			ResetProgressBar ();
			StopAllCoroutines ();

			FadeToFullAlphaByTime (1.6f, 0.1f, 1f);

		} else {
			
			StopAllCoroutines ();
			FadeToZeroAlphaByTime (.3f, 3f, .5f);
		}
	}


	void FadeToZeroAlpha() {

		FadeToZeroAlphaByTime (0f, 0f, 0f);

	}

	void FadeToZeroAlphaByTime(float loadingBarTime, float centerBarTime, float timerTextTime) {

		StartCoroutine (FadeImageToZeroAlpha (loadingBarTime, loadingBar));
		StartCoroutine (FadeImageToZeroAlpha (centerBarTime, centerBar));
		StartCoroutine (FadeTextToZeroAlpha (timerTextTime, timerText));
	}

	void FadeToFullAlphaByTime(float loadingBarTime, float centerBarTime, float timerTextTime) {

		StartCoroutine (FadeImageToFullAlpha (loadingBarTime, loadingBar));
		StartCoroutine (FadeImageToFullAlpha (centerBarTime, centerBar));
		StartCoroutine (FadeTextToFullAlpha (timerTextTime, timerText));
	}
}
