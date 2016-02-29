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
		if (other.gameObject.tag.Equals ("Road")) {
			if (other.gameObject.name.Equals ("Road 3T4(Clone)")) {
				playerController.IncreaseMaxPosition();
				tsm.IncreaseAllowedPositions ();
			}
		}
	}

	private void OnGameOver() {
		GameObject[] destroyableObjects = GameObject.FindGameObjectsWithTag ("Traffic");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		destroyableObjects = GameObject.FindGameObjectsWithTag ("Road");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		roadSpawner.Reset ();
		playerController.Reset ();
		score.Reset ();
		tsm.Reset ();
	}
}
