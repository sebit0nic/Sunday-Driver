using UnityEngine;
using System.Collections;

public class Puddle : MonoBehaviour {

	public ParticleSystem particles;
	private ParticleSystem.EmissionModule psemit;
	private bool playedOnce;
	private BoxCollider thisBoxCollider;

	private void Awake() {
		thisBoxCollider = GetComponent<BoxCollider> ();
		psemit = particles.emission;
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals ("Player") && !playedOnce) {
			if (!particles.isPlaying) {
				particles.Simulate (0.0f, true, true);
				psemit.enabled = true;
				particles.Play ();
			}
			playedOnce = true;
			thisBoxCollider.enabled = false;
		}
	}

	private void OnEnable() {
		if (particles.isPlaying) {
			psemit.enabled = false;
			particles.Stop ();
		}
		playedOnce = false;
		thisBoxCollider.enabled = true;
	}
}
