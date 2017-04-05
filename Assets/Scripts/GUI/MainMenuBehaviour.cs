using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public abstract class ObserverBehaviour : MonoBehaviour {
	public abstract void OnNotify ();
}

public class GuiBehaviour : ObserverBehaviour {

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

	public override void OnNotify () { }


}

public class MainMenuBehaviour : GuiBehaviour {

	private bool isServerFound = false;
	public Text tapToPlayText;
	public Text tapToJoinText;

	private bool isLoggedIn = false;
	public GameObject fbLoginButton;
	public GameObject fbLoggedContainer;
	public Image fbPicture;
	public Text fbName;
	public Text salutation;

	public RectTransform menuContainer;
	public RectTransform menuButton;


	void Awake () {

		FB.Init (SetInit, OnHideUnity);
	}

	void SetInit()
	{

		if (FB.IsLoggedIn) {
			fbLoginButton.SetActive (false);
			fbLoggedContainer.SetActive (true);

			FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
		} else {
			Debug.Log ("FB is not logged in");
		}

		//DealWithFBMenus (FB.IsLoggedIn);

	}

	void OnHideUnity(bool isGameShown) {

		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

	}



	void Start () {

		if (isServerFound) {
			ShowTapToJoinText ();
		} else {
			ShowTapToPlayText ();
		}

		/**if (FB.IsLoggedIn) {

			fbLoginButton.SetActive (false);
			fbLoggedContainer.SetActive (true);

			FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);

		} else {

			fbLoginButton.SetActive (true);
			fbLoggedContainer.SetActive (false);
		}*/
	}

	Vector3 rotationEuler;
	void Update () {


		if (this.isMenuActive) {


			this.menuButton.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
			this.menuContainer.anchoredPosition = Vector3.Lerp (this.menuContainer.anchoredPosition, new Vector3 (0, -64f, 0), Time.deltaTime * 10f);
		} else { 

			this.menuButton.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
			this.menuContainer.anchoredPosition = Vector3.Lerp (this.menuContainer.anchoredPosition, new Vector3 (0, 64f, 0), Time.deltaTime * 10f);
		}
	}

	public override void OnNotify () {
		if (!isServerFound) {
			isServerFound = true;

	
		}
	}


	private void ShowTapToJoinText () {

		StopAllCoroutines ();
		StartCoroutine (FadeTextToZeroAlpha (0f, tapToPlayText));
		StartCoroutine (FadeTextToFullAlpha (1f, tapToJoinText));
	}

	private void ShowTapToPlayText () {
		
		StopAllCoroutines ();
		StartCoroutine(FadeTextToFullAlpha(1f, tapToPlayText));
		StartCoroutine(FadeTextToZeroAlpha(0f, tapToJoinText));
	}


	public void OnTapAction () {
		Debug.Log ("On tap action");

		GameObject obj = GameObject.FindGameObjectWithTag ("NetworkManager");
		if (obj != null) {

			NetworkManagerBehaviour nmb = obj.GetComponent<NetworkManagerBehaviour> ();
			if (nmb != null) {

				if (isServerFound)
					nmb.OnJoinAction ("JOIN");
				else
					nmb.OnPlayAction ("PLAY");

			} else {
				Debug.Log ("Main menu Behaviour - nmb is null");
			}
		} else {
			Debug.Log ("Main menu Behaviour - obj is null");
		}
	}

	public void LogIn () {

		List<string> permissions = new List<string> ();
		permissions.Add ("public_profile");

		FB.LogInWithReadPermissions (permissions, AuthCallBack);
	}

	void AuthCallBack(IResult result) {

		if (result.Error != null) {
			Debug.Log (result.Error);
		} else {
			if (FB.IsLoggedIn) {
				Debug.Log ("FB is logged in");

				fbLoginButton.SetActive (false);
				fbLoggedContainer.SetActive (true);

				FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
				FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);

			} else {
				Debug.Log ("FB is not logged in");
			}
		}
	}

	void DisplayUsername (IResult result) {

		if (result.Error == null) {

			fbName.text = result.ResultDictionary ["first_name"].ToString ();

		} else {
			Debug.Log (result.Error);
		}

	}

	void DisplayProfilePic(IGraphResult result) {

		if (result.Texture != null) {

			fbPicture.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
		}

	}

	bool isMenuActive = false;
	public void MenuAction () {

		rotationEuler += Vector3.forward*30*Time.deltaTime; 
		this.menuButton.rotation = Quaternion.Euler(rotationEuler);
		this.isMenuActive = !this.isMenuActive;

	}
}
