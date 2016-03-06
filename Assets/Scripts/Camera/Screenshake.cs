using UnityEngine;
using System.Collections;

public class Screenshake : MonoBehaviour {

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	private float shakeDecay;
	private float shakeIntensity;

	private void Start() {
		originalPosition = transform.position;
		originalRotation = transform.rotation;
	}

	private void Update() {
		if (shakeIntensity > 0) {
			transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;
			transform.rotation = new Quaternion (originalRotation.x + Random.Range (-shakeIntensity, shakeIntensity) * 0.2f,
				originalRotation.y + Random.Range (-shakeIntensity, shakeIntensity) * 0.2f,
				originalRotation.z + Random.Range (-shakeIntensity, shakeIntensity) * 0.2f,
				originalRotation.w + Random.Range (-shakeIntensity, shakeIntensity) * 0.2f);
			shakeIntensity -= shakeDecay;
		}
	}

	public void Shake() {
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		shakeIntensity = 0.1f;
		shakeDecay = 0.0025f;
	}
}
