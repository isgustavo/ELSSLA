using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShootBehaviour : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public float timeBetweenFires = .3f;
	private float timeTilNextFire = 0.0f;


	void Update () {

		if (!isLocalPlayer)
			return;

		if (ShootButtonBehaviour.isShooting) {

			if (timeTilNextFire < 0) {
				timeTilNextFire = timeBetweenFires;
				CmdShoot (bulletSpawn.position, bulletSpawn.rotation);
			}
		}
		timeTilNextFire -= Time.deltaTime;
		
	}


	[Command]
	void CmdShoot (Vector3 position, Quaternion rotation) {

		var bullet = (GameObject)Instantiate(bulletPrefab, position, rotation);

		NetworkServer.Spawn(bullet);
	}
		
}
