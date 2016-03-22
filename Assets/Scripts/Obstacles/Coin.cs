using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public ParticleSystem particles;
	private ParticleSystem.EmissionModule psemit;
	private bool playedOnce;
	private BoxCollider thisBoxCollider;
	private float timeout;
	public Animator thisAnimator;

	private void Awake() {
		thisBoxCollider = GetComponent<BoxCollider> ();
		psemit = particles.emission;
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
			timeout = Time.time + 0.5f;
			thisBoxCollider.enabled = false;
			thisAnimator.SetTrigger ("OnDisappear");
		}
	}

	private void OnEnable() {
		if (particles.isPlaying) {
			psemit.enabled = false;
			particles.Stop ();
		}
		thisBoxCollider.enabled = true;
		thisAnimator.SetTrigger ("OnIdle");
	}
}
