using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShootBehaviour : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public float timeBetweenFires = .3f;
	private float timeTilNextFire = 0.0f;


	void Start () {

		if (!isLocalPlayer) {
			return;
		}
	}

	void Update () {

		if (!isLocalPlayer)
			return;

		if (ShootButtonBehaviour.isShooting) {

			if (timeTilNextFire < 0) {
				timeTilNextFire = timeBetweenFires;
				CmdShoot (bulletSpawn.position, bulletSpawn.rotation, gameObject.transform.name);
			}
		}
		timeTilNextFire -= Time.deltaTime;
		
	}


	[Command]
	void CmdShoot (Vector3 position, Quaternion rotation, string playerId) {

		//Debug.Log ("shoot:" + playerId);
		var bullet = (GameObject)Instantiate(bulletPrefab, position, rotation);
		bullet.GetComponent<BulletBehaviour> ().playerId = playerId;

		NetworkServer.Spawn(bullet);
	}
		
}
