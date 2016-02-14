﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private int position = 0;
	private float inputDelay = 0.2f, timer;

	public float speed = 1;
	private float startTime;
	private float journeyLength;

	private void Update() {
		timer += Time.deltaTime;
	}

	private void FixedUpdate() {
		//PC
		if (Input.GetAxis ("Horizontal") < 0 && position < 1 && timer >= inputDelay) {
			position++;
			timer = 0;
			SetLerpValues ();
		} else if (Input.GetAxis ("Horizontal") > 0 && position > -1 && timer >= inputDelay) {
			position--;
			timer = 0;
			SetLerpValues ();
		}

		//Mobile
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved && timer >= inputDelay) {
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			if (touchDeltaPosition.x < -2 && position < 1) {
				position++;
				timer = 0;
				SetLerpValues ();
			} else if (touchDeltaPosition.x > 2 && position > -1) {
				position--;
				timer = 0;
				SetLerpValues ();
			}
		}

		if (journeyLength != 0) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(transform.position, new Vector3(1.5f * position, 0, 0), fracJourney);
		}
	}

	private void SetLerpValues() {
		startTime = Time.time;
		journeyLength = Vector3.Distance(transform.position, new Vector3(1.5f * position, 0, 0));
	}
}
