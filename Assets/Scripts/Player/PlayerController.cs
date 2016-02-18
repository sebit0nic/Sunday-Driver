using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private int position = 0;
	private float inputDelay = 0.2f, timer;

	public float speed = 1;
	private float startTime;
	private float journeyLength;

	private Vector2 fp, lp;

	private void Update() {
		timer += Time.deltaTime;

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
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				fp = touch.position;
				lp = touch.position;
			}
			if (touch.phase == TouchPhase.Moved) {
				lp = touch.position;
			}
			if (touch.phase == TouchPhase.Ended) {
				if ((fp.x - lp.x) > 50 && position < 1) {
					position++;
					SetLerpValues ();
				} else if ((fp.x - lp.x) < -50 && position > -1) {
					position--;
					SetLerpValues ();
				}
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

	public void Reset() {
		position = 0;
		transform.position = Vector3.zero;
	}
}
