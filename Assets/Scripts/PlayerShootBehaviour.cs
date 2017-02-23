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
				this.timeTilNextFire = timeBetweenFires;
				CmdShoot ();
			}
		}
		timeTilNextFire -= Time.deltaTime;
		
	}


	[Command]
	void CmdShoot () {

		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		bullet.GetComponent<BulletBehaviour> ().playerId = gameObject.name;

		NetworkServer.Spawn(bullet);
	}
}
