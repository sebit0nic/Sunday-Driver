using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private int position = 0;
	private float inputDelay = 0.2f, timer;

	private void Update() {
		timer += Time.deltaTime;
	}

	private void FixedUpdate() {
		//PC
		if (Input.GetAxis ("Horizontal") < 0 && position < 1 && timer >= inputDelay) {
			position++;
			timer = 0;
		} else if (Input.GetAxis ("Horizontal") > 0 && position > -1 && timer >= inputDelay) {
			position--;
			timer = 0;
		}

		//Mobile
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved && timer >= inputDelay) {
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			if (touchDeltaPosition.x < -2 && position < 1) {
				position++;
				timer = 0;
			} else if (touchDeltaPosition.x > 2 && position > -1) {
				position--;
				timer = 0;
			}
		}

		transform.position = new Vector3 (1.5f * position, 0, 0);
	}
}
