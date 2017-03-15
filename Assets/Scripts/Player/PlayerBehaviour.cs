using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;



public class PlayerBehaviour : NetworkBehaviour {

	private const float POSITION_SPEED = 10f;
	private const float ROTATE_AMOUNT = 2f;

	[SyncVar]
	public int score;
	[SyncVar (hook="OnStatusChange")]
	public bool isDead;
	public bool isMoving { get; set; }
	public bool isShooting { get; set; }
	public PlayerData data { get; set; }

	private Rigidbody rb;
	private CapsuleCollider cc;
	[SerializeField]
	private ParticleSystem explosion;

	//MARK::
	void Start () {

		rb = GetComponent<Rigidbody> ();
		cc = GetComponent<CapsuleCollider> ();
		explosion = Instantiate (explosion);



		score = 0;
		isDead = false;

		if (!isLocalPlayer) 
			return;


		//Load ();

	}

	void Update () {

		if (!isLocalPlayer || isDead)
			return;

		float tiltValue = GetTiltValue();
		Vector3 oldAngles = this.transform.eulerAngles;

		//this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (tiltValue * ROTATE_AMOUNT));
		this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (1 * ROTATE_AMOUNT));

	}

	void FixedUpdate () {

		if (!isLocalPlayer || isDead)
			return;

		if (isMoving) {

			rb.AddForce (transform.up * POSITION_SPEED, ForceMode.Acceleration);
		}
	}

	//MARK::
	void OnCollisionEnter (Collision collision) {

		if (!isLocalPlayer)
			return;

		CmdDestroy ();
	}

	//MARK::
	public void OnStatusChange (bool value) {

		isDead = value;
		if (isDead) {
			if (isLocalPlayer) {
				Debug.Log ("Save");
			}
			explosion.transform.position = rb.position;
			explosion.Play ();
			rb.position = new Vector3 (rb.position.x, rb.position.y, -10);
			rb.velocity = new Vector3 (0, 0, 0);
			transform.rotation = Quaternion.identity;
		} else {
			score = 0;
			rb.position = new Vector3 (0, 0, -6);
			rb.velocity = new Vector3 (0, 0, 0);
			transform.rotation = Quaternion.identity;
		}
	}

	//MARK::
	public override void OnStartLocalPlayer () {
		base.OnStartLocalPlayer ();

		CameraFollowBehaviour camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollowBehaviour> ();
		camera.target = gameObject;

		LocalGameBehaviour manage = GameObject.FindGameObjectWithTag ("GameGUI").GetComponent<LocalGameBehaviour> ();
		manage.player = this;

	}

	public override void OnStartClient () {
		base.OnStartClient ();

		gameObject.transform.name = "Player" + gameObject.GetComponent<NetworkIdentity> ().netId.ToString ();
		GameManagerBehaviour.instance.AddPlayer (gameObject.transform.name, this);
	}
		


	[Command]
	void CmdDestroy () {

		isDead = true;
	}

	[Command]
	public void CmdRespawn () {

		isDead = false;
	}
		
	float GetTiltValue () {
		float TILT_MIN = 0.05f;
		float TILT_MAX = 0.2f;

		// Work out magnitude of tilt
		float tilt = Mathf.Abs(Input.acceleration.x);

		// If not really tilted don't change anything
		if (tilt < TILT_MIN) {
			return 0;
		}
		float tiltScale = (tilt - TILT_MIN) / (TILT_MAX - TILT_MIN);

		// Change scale to be negative if accel was negative
		if (Input.acceleration.x < 0) {
			return -tiltScale;
		} else {
			return tiltScale;
		}
	}
		

	// MARK::
	void Save (int points, bool programmerDeath) {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {
			
			file = new FileStream (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
			data = (PlayerData) bf.Deserialize (file);

			if (points > data.highscore) {
				data.highscore = points;
			}

			data.deaths += 1;

			if (programmerDeath) {
				data.programmerDeaths += 1;
			}
				
			bf.Serialize (file, data);
			file.Close (); 
		} 

	}

	void Load () {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {

			file = new FileStream (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
			data = (PlayerData) bf.Deserialize (file);
			file.Close ();
		} else {

			file = File.Create (Application.persistentDataPath + "/PlayerInfo.dat");
			data = new PlayerData ();
		}
	}

}


