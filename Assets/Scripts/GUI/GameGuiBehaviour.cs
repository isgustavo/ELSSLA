using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiBehaviour : MonoBehaviour {

	public IEnumerator FadeTextToFullAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeTextToZeroAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeImageToFullAlpha(float t, Image image) {
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
		while (image.color.a < 1.0f) {
			image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeImageToZeroAlpha(float t, Image image) {

		image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
		while (image.color.a > 0.0f) {
			image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}




}

public class GameGuiBehaviour : GuiBehaviour {

	public Text tapToPlayText;

	// Use this for initialization
	void Start () {

		StartCoroutine(FadeTextToFullAlpha(1f, tapToPlayText));
	}


}
