using UnityEngine;
using System.Collections;

public class RoadPiece : MonoBehaviour {

	private WeatherController weatherController;
	public GameObject snow;

	private void Awake() {
		weatherController = GameObject.Find ("Weather").GetComponent<WeatherController> ();
	}

	private void OnEnable() {
		if (weatherController.GetCurrentState () == 2) {
			snow.SetActive (true);
		} else {
			snow.SetActive (false);
		}
	}
}
