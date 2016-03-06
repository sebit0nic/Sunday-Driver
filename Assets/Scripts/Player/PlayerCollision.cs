using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerCollision : MonoBehaviour {

	public RoadSpawner roadSpawner;
	public Score score;
	private PlayerController playerController;
	private TrafficSpawnManager tsm;
	private CameraController cameraController;
	private Animator animator;

	private void Start() {
		playerController = GetComponent<PlayerController> ();
		tsm = GameObject.Find ("Traffic Spawn Manager").GetComponent<TrafficSpawnManager> ();
		cameraController = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
		animator = GetComponent<Animator> ();
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals ("Traffic")) {
			//OnGameOver ();
			roadSpawner.gameObject.SetActive(false);
			tsm.gameObject.SetActive (false);
			playerController.enabled = false;
			animator.enabled = false;
			cameraController.gameObject.GetComponent<Screenshake> ().Shake ();
			cameraController.gameObject.GetComponent<Blur> ().enabled = true;
			Time.timeScale = 0.1f;
		}
		if (other.gameObject.tag.Equals ("Road")) {
			if (other.gameObject.name.Equals ("Road 3T4(Clone)")) {
				playerController.IncreaseMaxPosition();
				tsm.IncreaseAllowedPositions ();
				cameraController.MoveToPosition (4);
			}
			if (other.gameObject.name.Equals("Road 4T5(Clone)")) {
				playerController.IncreaseMaxPosition();
				tsm.IncreaseAllowedPositions ();
				cameraController.MoveToPosition (5);
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
		cameraController.MoveToPosition (3);
		roadSpawner.Reset ();
		playerController.Reset ();
		score.Reset ();
		tsm.Reset ();
	}
}
