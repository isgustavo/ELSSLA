using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour, Destructible {
	
	public static string LOCAL_PLAYER_TAG = "LocalPlayer";

	private const string OBJECT_NAME_PREFIX = "PLAYER";
	private const int POINTS = 300;
	private const int INITIAL_SCORE = 0;
	private const float TILT_MIN = .05f;
	private const float TILT_MAX = .2f;
	private const float ROTATE_AMOUNT = 2f;
	private const float TIME_BETWEEN_SHOT = .3f;
	private const float TIME_INSIDE_OUT_TOLERANCE = 5f;

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
	private float timeInsideOut = TIME_INSIDE_OUT_TOLERANCE;

	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private Transform bulletSpawn;
	[SerializeField]
	private Transform explosionSpawn;
	[SerializeField]
	private ParticleSystem explosion;
	[SerializeField]
	private ParticleSystem force;
	private bool forceExplosionUsed = false;


	//MARK::
	void Start () {

		highscore = LocalPlayerBehaviour.instance.GetHighscore ();

		rb = GetComponent<Rigidbody> ();
		rb.position = new Vector3 (0, 0, 10);
		cc = GetComponent<CapsuleCollider> ();

		explosion = Instantiate (explosion);
		force = Instantiate (force);
	}

	void Update () {

		if (!isLocalPlayer || isDead)
			return;
		
		GameUtil.AjustZPosition (rb);

		Vector3 oldAngles = this.transform.eulerAngles;
		#if UNITY_EDITOR
			if (Input.GetKey(KeyCode.LeftArrow)) {
				this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (2 * ROTATE_AMOUNT));
			} else if (Input.GetKey(KeyCode.RightArrow)) {
				this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (-2 * ROTATE_AMOUNT));
			}
		#else 
			float tiltValue = GetTiltValue();
			this.transform.eulerAngles = new Vector3(oldAngles.x, oldAngles.y, oldAngles.z + (tiltValue * ROTATE_AMOUNT));
		#endif


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

			if (!forceExplosionUsed) {
				force.transform.position = explosionSpawn.position;
				force.Play ();
				forceExplosionUsed = true;
			}

			rb.AddForce (transform.up * GameUtil.POSITION_SPEED, ForceMode.Acceleration);
		} else {
			forceExplosionUsed = false;
		}

		if (!GameUtil.VerifyInsideWorld (rb.position)) {

			if (timeInsideOut < 0) {
				isDead = true;
			}
			timeInsideOut -= Time.deltaTime;
			//TODO: display time 
		} else {
			
			timeInsideOut = TIME_INSIDE_OUT_TOLERANCE;
		}

	}
		
	//MARK:: Network Behaviour methods
	public override void OnStartLocalPlayer () {
		base.OnStartLocalPlayer ();

		gameObject.tag = LOCAL_PLAYER_TAG;

	}

	public override void OnStartClient () {
		base.OnStartClient ();

		gameObject.transform.name = "PLAYER" + gameObject.GetComponent<NetworkIdentity> ().netId.ToString ();

		if (!isServer)
			return;

		//ServerManagerBehaviour.instance.AddPlayer (gameObject.transform.name, this);
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

			rb.position = new Vector3 (rb.position.x, rb.position.y, GameUtil.Z_DEAD_POSITION);
			rb.velocity = Vector3.zero;
			transform.rotation = Quaternion.identity;
			isMoving = false;
			isShooting = false;

		} else {
			
			score = INITIAL_SCORE;
			rb.position = new Vector3 (0, 0, 0);
			cc.enabled = true;
		}
	}

	public void OnScoreChange (int value) {
		
		score = value;
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
			//TODO: obj.playerId KILL YOU
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
		
	public void Unspawn () {


		NetworkManagerBehaviour net = GameObject.FindGameObjectWithTag ("NetworkManager").GetComponent<NetworkManagerBehaviour> ();
		net.StopClient ();
		//Network.RemoveRPCs(Network.player);
		//Network.DestroyPlayerObjects(Network.player);
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


