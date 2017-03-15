using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyBehaviour : MonoBehaviour {

	public List<ParticleSystem> particles = new List<ParticleSystem> ();

	public void EmitParticules () {

		foreach (ParticleSystem particle in particles) {

			particle.Emit (1);

		}

		//Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().duration); 
	}
	

}
