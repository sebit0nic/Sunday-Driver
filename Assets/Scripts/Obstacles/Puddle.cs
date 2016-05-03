using UnityEngine;
using System.Collections;

public class Puddle : MonoBehaviour {

	public ParticleSystem particles;
	private ParticleSystem.EmissionModule psemit;
	private bool playedOnce;
	private BoxCollider thisBoxCollider;
	private float timeout;
	private AudioSource skidSound;

	private void Awake() {
		thisBoxCollider = GetComponent<BoxCollider> ();
		psemit = particles.emission;
		skidSound = GetComponent<AudioSource> ();
	}
		
	private void Update() {
		if (playedOnce && timeout < Time.time) {
			thisBoxCollider.enabled = true;
			playedOnce = false;
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals ("Player") && !playedOnce) {
			if (!particles.isPlaying) {
				particles.Simulate (0.0f, true, true);
				psemit.enabled = true;
				particles.Play ();
			}
			playedOnce = true;
			skidSound.Play ();
			timeout = Time.time + 0.5f;
			thisBoxCollider.enabled = false;
		}
	}

	private void OnEnable() {
		if (particles.isPlaying) {
			psemit.enabled = false;
			particles.Stop ();
		}
		thisBoxCollider.enabled = true;
	}
}
