using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	public RoadSpawner roadSpawner;
	public Score score;
	private PlayerController playerController;
	private TrafficSpawnManager tsm;

	private void Start() {
		playerController = GetComponent<PlayerController> ();
		tsm = GameObject.Find ("Traffic Spawn Manager").GetComponent<TrafficSpawnManager> ();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Traffic")) {
			OnGameOver ();
		}
	}

	private void OnGameOver() {
		GameObject[] destroyableObjects = GameObject.FindGameObjectsWithTag ("Traffic");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
			tsm.DecreaseCurrentTrafficCount ();
		}
		destroyableObjects = GameObject.FindGameObjectsWithTag ("Road");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		roadSpawner.Reset ();
		playerController.Reset ();
		score.Reset ();
	}
}
