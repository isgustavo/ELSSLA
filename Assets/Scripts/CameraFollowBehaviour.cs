using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBehaviour : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;   


	void Start () {

		offset.x = transform.position.x - player.transform.position.x;
		offset.y = transform.position.y - player.transform.position.y;

	}

	
	void LateUpdate () 
	{

		Vector3 position = new Vector3 (player.transform.position.x + offset.x,
			player.transform.position.y+ offset.y,
			-10f);
		//transform.position.x = player.transform.position.x + offset.x;
		transform.position = position;
	}
}
