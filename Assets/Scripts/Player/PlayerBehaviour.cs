using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour, Destructible {

	private const string OBJECT_NAME_PREFIX = "PLAYER";
	private const int POINTS = 300;
	private const int INITIAL_SCORE = 0;
	private const float Z_FINAL_POSITION = -6f;
	private const float Z_DEAD_POSITION = -10f;
	private const float POSITION_SPEED = 10f;
	private const float TILT_MIN = .05f;
	private const float TILT_MAX = .2f;
	private const float ROTATE_AMOUNT = 2f;
	private const float TIME_BETWEEN_SHOT = .3f;

	[SyncVar (hook="OnScoreChange")]
	public int score = INITIAL_SCORE;
	public int highscore;
	[SyncVar (hook="OnStatusChange")]
	public bool isDead = false;
	public bool isMoving { get; set; }
	public bool isShooting { get; set; }

	private Rigidbody rb;
	private CapsuleCollider cc;
	private float timeTilNextShot = .0f;

	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private Transform bulletSpawn;
	[SerializeField]
	private ParticleSystem explosion;



	//MARK::
	void Start () {

		rb = GetComponent<Rigidbody> ();
		cc = GetComponent<CapsuleCollider> ();
		highscore = LocalPlayerBehaviour.instance.GetHighscore ();
		explosion = Instantiate (explosion);

	}

	void Update () {

		if (!isLocalPlayer || isDead)
			return;
		
		if (rb.position.z > Z_FINAL_POSITION) {
			Vector3 newPosition = rb.position;
			newPosition.z = Mathf.Lerp (rb.position.z, Z_FINAL_POSITION, Time.deltaTime * POSITION_SPEED);
			transform.position = newPosition;
		} 

		//Rotation
		float tiltValue = GetTiltValue();
		Vector3 oldAngles = this.transform.eulerAngles;

		this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (2 * ROTATE_AMOUNT));
		//this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (tiltValue * ROTATE_AMOUNT));


		if (isShooting) {

			if (timeTilNextShot < 0) {
				timeTilNextShot = TIME_BETWEEN_SHOT;
				CmdShoot (bulletSpawn.position, bulletSpawn.rotation, gameObject.transform.name);
			}
		}
		timeTilNextShot -= Time.deltaTime;

	}

	void FixedUpdate () {

		if (!isLocalPlayer || isDead)
			return;

		if (isMoving) {

			rb.AddForce (transform.up * POSITION_SPEED, ForceMode.Acceleration);
		}
	}

	//MARK:: Network Behaviour methods
	public override void OnStartLocalPlayer () {
		base.OnStartLocalPlayer ();

		CameraFollowBehaviour camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollowBehaviour> ();
		camera.target = gameObject;

		LocalGUIGameBehaviour game = GameObject.FindGameObjectWithTag ("GameGUI").GetComponent<LocalGUIGameBehaviour> ();
		game.player = this;

	}

	public override void OnStartClient () {
		base.OnStartClient ();

		gameObject.transform.name = "PLAYER" + gameObject.GetComponent<NetworkIdentity> ().netId.ToString ();
		GameManagerBehaviour.instance.AddPlayer (gameObject.transform.name, this);
	}

	//MARK:: Hook methods
	public void OnStatusChange (bool value) {

		isDead = value;
		if (isDead) {
			if (isLocalPlayer) {
				LocalPlayerBehaviour.instance.SaveLocalPlayerIndo (score);
			}

			explosion.transform.position = rb.position;
			explosion.Play ();

			rb.position = new Vector3 (rb.position.x, rb.position.y, Z_DEAD_POSITION);
			rb.velocity = Vector3.zero;
			transform.rotation = Quaternion.identity;

		} else {
			
			score = INITIAL_SCORE;
			rb.position = new Vector3 (0, 0, 0);
			cc.enabled = true;
		}
	}

	public void OnScoreChange (int value) {
		
		if (value > highscore) {
			highscore = value;
		}
	}

	//MARK:: Collision methods
	void OnCollisionEnter (Collision collision) {

		if (!isLocalPlayer)
			return;

		cc.enabled = false;

		GameObject hit = collision.gameObject;
		BulletBehaviour obj = hit.GetComponent<BulletBehaviour> ();

		if (obj != null) {
			Debug.Log (obj.playerId + "Kill you");
		}


		CmdDestroy ();
	}
		
	//MARK:: Command methods
	[Command]
	void CmdDestroy () {

		isDead = true;
	}

	[Command]
	public void CmdRespawn () {

		isDead = false;
	}

	[Command]
	void CmdShoot (Vector3 position, Quaternion rotation, string playerId) {

		var bullet = (GameObject)Instantiate(bulletPrefab, position, rotation);
		bullet.GetComponent<BulletBehaviour> ().playerId = playerId;

		NetworkServer.Spawn(bullet);
	}

	//MARK:: Destructible interface method
	public int GetPoints () {
		
		return POINTS;
	}


	//MARK:: Others methods
		
	float GetTiltValue () {

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
}


