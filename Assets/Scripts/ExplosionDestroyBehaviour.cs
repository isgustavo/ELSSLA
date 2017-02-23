using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyBehaviour : MonoBehaviour {


	void Start () {

		Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().duration); 
	}
	

}
