using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour {

	private Light globalLight;
	private float lightningTimer;
	private bool falloff, canLightning = true;
	private AudioSource thunderSound;

	private void Awake() {
		thunderSound = GetComponent<AudioSource> ();
	}

	private void Start() {
		globalLight = GameObject.Find ("Directional Light").GetComponent<Light> ();
		lightningTimer = Time.time + Random.Range (8f, 20f);
	}

	private void Update() {
		if (lightningTimer < Time.time && canLightning) {
			globalLight.intensity = 4;
			lightningTimer = Time.time + Random.Range (8f, 20f);
			falloff = true;
			thunderSound.Play ();
		}
		if (falloff) {
			globalLight.intensity -= Time.deltaTime * 3;
			if (globalLight.intensity <= 1) {
				globalLight.intensity = 1;
				falloff = false;
			}
		}
	}

	public void SetCanLightning(bool canLightning) {
		this.canLightning = canLightning;
	}
}
