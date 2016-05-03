using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour {

	public Rain rain;
	public GameObject snow;
	private Light mainLight;
	private float timer;
	private int state, newState; //0 = normal, 1 = rain, 2 = snow
	private bool transitioning;
	private Color tempColor;
	private float tempIntensity;
	private AudioSource rainSound, snowSound;

	private void Awake() {
		mainLight = GameObject.Find ("Directional Light").GetComponent<Light> ();
	}

	private void Start() {
		timer = Time.time + Random.Range (20f, 30f);
		tempColor = Color.white;
		tempIntensity = 1;
		state = 0;

		rainSound = GetComponents<AudioSource> () [0];
		snowSound = GetComponents<AudioSource> () [1];
	}

	private void Update() {
		if (timer < Time.time) {
			transitioning = true;
			timer = Time.time + Random.Range (20f, 30f);
			if (state == 0) {
				newState = Random.Range (1, 3);
			} else {
				newState = Random.Range (0, 3);
			}
		}
		TransitionWeather ();
	}

	private void TransitionWeather() {
		if (transitioning) {
			switch (newState) {
			case 0:
				if (state == 1) {
					tempColor.r += Time.deltaTime * 0.4f;
					tempColor.g += Time.deltaTime * 0.4f;
					tempColor.b += Time.deltaTime * 0.4f;
					mainLight.color = tempColor;
					rain.gameObject.transform.position = Vector3.Lerp (rain.gameObject.transform.position, new Vector3 (-35, 20, -8f), Time.deltaTime * 0.5f);
					rain.SetCanLightning (false);
					if (rainSound.volume > 0) {
						rainSound.volume -= Time.deltaTime / 2;
					}
					if (tempColor.r >= 1f) {
						tempColor.r = 1f;
						tempColor.g = 1f;
						tempColor.b = 1f;
					}
					if (rain.gameObject.transform.position.x <= -34) {
						rain.gameObject.SetActive (false);
						transitioning = false;
						state = 0;
					}
				}
				if (state == 2) {
					tempIntensity += Time.deltaTime * 0.4f;
					mainLight.intensity = tempIntensity;
					snow.transform.position = Vector3.Lerp (snow.transform.position, new Vector3 (-60, 21.3f, -2.9f), Time.deltaTime * 0.5f);
					if (snowSound.volume > 0) {
						snowSound.volume -= Time.deltaTime / 2;
					}
					if (tempIntensity >= 1f) {
						tempIntensity = 1f;
					}
					if (snow.transform.position.x <= -59) {
						snow.SetActive (false);
						transitioning = false;
						state = 0;
					}
				}
				break;
			case 1:
				if (state == 0) {
					rain.gameObject.SetActive (true);
					tempColor.r -= Time.deltaTime * 0.4f;
					tempColor.g -= Time.deltaTime * 0.4f;
					tempColor.b -= Time.deltaTime * 0.4f;
					mainLight.color = tempColor;
					rain.gameObject.transform.position = Vector3.Lerp (rain.gameObject.transform.position, new Vector3 (-4, 20, -8f), Time.deltaTime * 0.5f);
					rain.SetCanLightning (true);
					if (rainSound.volume < 0.8f) {
						rainSound.volume += Time.deltaTime/2;
					}
					if (tempColor.r <= 0.6f) {
						tempColor.r = 0.6f;
						tempColor.g = 0.6f;
						tempColor.b = 0.6f;
					}
					if (rain.gameObject.transform.position.x >= -5) {
						transitioning = false;
						state = 1;
					}
				}
				if (state == 2) {
					tempIntensity += Time.deltaTime * 0.4f;
					mainLight.intensity = tempIntensity;
					snow.transform.position = Vector3.Lerp (snow.transform.position, new Vector3 (-60, 21.3f, -2.9f), Time.deltaTime * 0.5f);
					if (snowSound.volume > 0) {
						snowSound.volume -= Time.deltaTime / 2;
					}
					if (tempIntensity >= 1f) {
						tempIntensity = 1f;
					}
					if (snow.transform.position.x <= -59) {
						snow.SetActive (false);
						transitioning = false;
						state = 0;
					}
				}
				break;
			case 2:
				if (state == 0) {
					snow.SetActive (true);
					tempIntensity -= Time.deltaTime * 0.4f;
					mainLight.intensity = tempIntensity;
					snow.transform.position = Vector3.Lerp (snow.transform.position, new Vector3 (-8f, 3.8f, -7.4f), Time.deltaTime * 0.5f);
					if (snowSound.volume < 0.8f) {
						snowSound.volume += Time.deltaTime / 2;
					}
					if (tempIntensity <= 0.8f) {
						tempIntensity = 0.8f;
					}
					if (snow.transform.position.x >= -9) {
						transitioning = false;
						state = 2;
					}
				}
				if (state == 1) {
					tempColor.r += Time.deltaTime * 0.4f;
					tempColor.g += Time.deltaTime * 0.4f;
					tempColor.b += Time.deltaTime * 0.4f;
					mainLight.color = tempColor;
					rain.gameObject.transform.position = Vector3.Lerp (rain.gameObject.transform.position, new Vector3 (-35, 20, -8f), Time.deltaTime * 0.5f);
					rain.SetCanLightning (false);
					if (rainSound.volume > 0) {
						rainSound.volume -= Time.deltaTime/2;
					}
					if (tempColor.r >= 1f) {
						tempColor.r = 1f;
						tempColor.g = 1f;
						tempColor.b = 1f;
					}
					if (rain.gameObject.transform.position.x <= -34) {
						rain.gameObject.SetActive (false);
						transitioning = false;
						state = 0;
					}
				}
				break;
			}
		}
	}

	public int GetCurrentState() {
		return state;
	}
}
