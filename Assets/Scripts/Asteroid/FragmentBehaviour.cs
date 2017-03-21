using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FragmentBehaviour : NetworkBehaviour, Destructible {

	private const int POINTS = 50;

	public bool inUse = false;
	public int velocityDirection;

	public Vector3 startPosition;
	public Vector3 startRotation;

	public event PushFragmentDelegate pushDelegate;

	private Rigidbody rb;
	private BoxCollider cc;

	[SerializeField]
	private ParticleSystem explosion;

	void Start () {

		if (!isServer)
			return;

		rb = GetComponent<Rigidbody> ();
		rb.position = GameUtil.NO_USE_POSITION;
		cc = GetComponent<BoxCollider> ();
		cc.enabled = false;
	}

	void Update () {
		
		if (!isServer || !inUse)
			return;
		
		GameUtil.VerifyZPosition (rb);
	}

	void OnCollisionEnter(Collision collision) {

		if (!isServer)
			return;

		GameObject hit = collision.gameObject;
		if (hit.GetComponent<BulletBehaviour> () != null) {
			cc.enabled = false;

			RpcFragmentExplosion (rb.position, rb.rotation);

			OnChangeUse (false, GameUtil.NO_USE_POSITION, Quaternion.identity);
		}

	}


	public void OnChangeUse (bool value, Vector3 position, Quaternion rotation) {

		if (!isServer)
			return;
		
		inUse = value; 

		if (inUse) {

			rb.position = startPosition + position;
			rb.rotation.SetLookRotation(startRotation);

			if (velocityDirection == 1) {
				rb.velocity = Vector3.right;
			} else if (velocityDirection == 0) {
				rb.velocity = Vector3.up;
			} else {
				rb.velocity = Vector3.left;
			}

			cc.enabled = true;

		} else { 
			
			rb.position = position;
			rb.velocity = Vector3.zero;
		}
	}

	public int GetPoints () {

		return POINTS;
	}

	[ClientRpc]
	void RpcFragmentExplosion (Vector3 position, Quaternion rotation) {

		Instantiate (explosion, position, rotation).Play();
	}
}