using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour {

	private Light globalLight;
	private float lightningTimer;
	private bool falloff;

	private void Start() {
		globalLight = GameObject.Find ("Directional Light").GetComponent<Light> ();
		lightningTimer = Time.time + Random.Range (8f, 20f);
	}

	private void Update() {
		if (lightningTimer < Time.time) {
			globalLight.intensity = 4;
			lightningTimer = Time.time + Random.Range (8f, 20f);
			falloff = true;
		}
		if (falloff) {
			globalLight.intensity -= 0.075f;
			if (globalLight.intensity <= 1) {
				globalLight.intensity = 1;
				falloff = false;
			}
		}
	}
}
