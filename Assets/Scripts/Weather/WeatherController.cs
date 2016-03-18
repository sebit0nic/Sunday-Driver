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
				tempColor.r -= 0.01f;
				tempColor.g -= 0.01f;
				tempColor.b -= 0.01f;
				mainLight.color = tempColor;
				rain.gameObject.transform.position = Vector3.Lerp (rain.gameObject.transform.position, new Vector3 (-4, 20, -15.1f), Time.deltaTime * 0.5f);
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
				tempColor.r += 0.01f;
				tempColor.g += 0.01f;
				tempColor.b += 0.01f;
				mainLight.color = tempColor;
				rain.gameObject.transform.position = Vector3.Lerp (rain.gameObject.transform.position, new Vector3 (-35, 20, -15.1f), Time.deltaTime * 0.5f);
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
