using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour {

	public Rain rain;
	private Light mainLight;
	private float timer;
	private string state;
	private bool transitioning;
	private Color tempColor;

	private void Awake() {
		mainLight = GameObject.Find ("Directional Light").GetComponent<Light> ();
	}

	private void Start() {
		timer = Time.time + Random.Range (20f, 30f);
		tempColor = Color.white;
		state = "Normal";
	}

	private void Update() {
		if (timer < Time.time) {
			transitioning = true;
			timer = Time.time + Random.Range (30f, 40f);
		}
		TransitionWeather ();
	}

	private void TransitionWeather() {
		if (transitioning) {
			switch (state) {
			case "Normal":
				rain.gameObject.SetActive (true);
				tempColor.r -= Time.deltaTime * 0.4f;
				tempColor.g -= Time.deltaTime * 0.4f;
				tempColor.b -= Time.deltaTime * 0.4f;
				mainLight.color = tempColor;
				rain.gameObject.transform.position = Vector3.Lerp (rain.gameObject.transform.position, new Vector3 (-4, 20, -8f), Time.deltaTime * 0.5f);
				rain.SetCanLightning (true);
				if (tempColor.r <= 0.6f) {
					tempColor.r = 0.6f;
					tempColor.g = 0.6f;
					tempColor.b = 0.6f;
				}
				if (rain.gameObject.transform.position.x >= -5) {
					transitioning = false;
					state = "Rain";
				}
				break;
			case "Rain":
				tempColor.r += Time.deltaTime * 0.4f;
				tempColor.g += Time.deltaTime * 0.4f;
				tempColor.b += Time.deltaTime * 0.4f;
				mainLight.color = tempColor;
				rain.gameObject.transform.position = Vector3.Lerp (rain.gameObject.transform.position, new Vector3 (-35, 20, -8f), Time.deltaTime * 0.5f);
				rain.SetCanLightning (false);
				if (tempColor.r >= 1f) {
					tempColor.r = 1f;
					tempColor.g = 1f;
					tempColor.b = 1f;
				}
				if (rain.gameObject.transform.position.x <= -34) {
					rain.gameObject.SetActive (false);
					transitioning = false;
					state = "Normal";
				}
				break;
			}
		}
	}
}
