using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	public RoadSpawner roadSpawner;
	public TrafficSpawner trafficSpawner;
	public Score score;
	private PlayerController playerController;

	private void Start() {
		playerController = GetComponent<PlayerController> ();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Traffic")) {
			OnGameOver ();
		}
	}

	private void OnGameOver() {
		GameObject[] destroyableObjects = GameObject.FindGameObjectsWithTag ("Traffic");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			Destroy (destroyableObjects [i]);
		}
		destroyableObjects = GameObject.FindGameObjectsWithTag ("Road");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			Destroy (destroyableObjects [i]);
		}
		roadSpawner.Reset ();
		trafficSpawner.Reset ();
		playerController.Reset ();
		score.Reset ();
	}
}
