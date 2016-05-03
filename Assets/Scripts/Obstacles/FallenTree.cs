﻿using UnityEngine;
using System.Collections;

public class FallenTree : MonoBehaviour {

	private Animator thisAnimator;
	private bool animatedOnce;
	private AudioSource treeSound;

	private void Awake() {
		thisAnimator = GetComponent<Animator> ();
		treeSound = GetComponent<AudioSource> ();
	}

	private void Update() {
		if (transform.position.z > -40 && !animatedOnce) {
			if (transform.position.x > 0) {
				thisAnimator.SetTrigger ("OnFallRight");
			} else {
				thisAnimator.SetTrigger ("OnFallLeft");
			}
			treeSound.Play ();
			animatedOnce = true;
		}
	}

	private void OnEnable() {
		thisAnimator.SetTrigger ("OnIdle");
		animatedOnce = false;
	}
}
