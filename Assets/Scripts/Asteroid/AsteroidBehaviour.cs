using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public interface Destructible {

	int GetPoints ();
}

public class AsteroidBehaviour : NetworkBehaviour, Destructible {

	private const int POINTS = 100;

	public bool inUse = true;
	private Rigidbody rb;
	private SphereCollider cc;

	public event PushDelegate pushDelegate;

	[SerializeField]
	private ParticleSystem explosion;


	void Start () {

		if (!isServer)
			return;
		
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.position = GameUtil.GetRandomPosition ();
		rb.rotation = Random.rotation;
		rb.velocity = GameUtil.GetRandomVelocity ();

		cc = gameObject.GetComponent<SphereCollider> ();
	}

	void Update () {
		
		if (!isServer || !inUse)
			return;

		GameUtil.AjustZPosition (rb);

		if (!GameUtil.VerifyInsideWorld (rb.position)) {
			OnChangeUse (false);
		}
	}


	void OnCollisionEnter(Collision collision) {

		if (!isServer)
			return;

		GameObject hit = collision.gameObject;
		if (hit.GetComponent<BulletBehaviour> () != null) {
			cc.enabled = false;

			RpcAsteroidExplosion (rb.position, rb.rotation);

			OnChangeUse (false);
		}
			
	}
		
	public void OnChangeUse (bool value) {

		if (!isServer)
			return;
		
		inUse = value; 

		if (inUse) {

			rb.position = GameUtil.GetRandomPosition ();
			rb.velocity = GameUtil.GetRandomVelocity ();
			cc.enabled = true;
		} else {

			pushDelegate (this);
			rb.position = GameUtil.NO_USE_POSITION;
			rb.velocity = Vector3.zero;
		}
	}

	public int GetPoints () {

		return POINTS;
	}

	[ClientRpc]
	void RpcAsteroidExplosion (Vector3 position, Quaternion rotation) {

		Instantiate (explosion, position, rotation).Play();
	}

		
}
