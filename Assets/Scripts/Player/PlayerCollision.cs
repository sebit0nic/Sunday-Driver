using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerCollision : MonoBehaviour {

	public RoadSpawner roadSpawner;
	public Score score;
	public GameObject gameoverCanvas, startCanvas;
	public ParticleSystem particleSys;
	private PlayerController playerController;
	private TrafficSpawnManager tsm;
	private CameraController cameraController;
	private Animator animator;

	private float timeout;
	private bool crashed, screenShakedOnce;
	private Rigidbody thisRigidbody;

	private void Start() {
		playerController = GetComponent<PlayerController> ();
		tsm = GameObject.Find ("Traffic Spawn Manager").GetComponent<TrafficSpawnManager> ();
		cameraController = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
		animator = GetComponent<Animator> ();
		thisRigidbody = GetComponent<Rigidbody> ();
	}

	private void Update() {
		if (crashed && timeout < Time.time) {
			cameraController.gameObject.GetComponent<Blur> ().enabled = true;
			Time.timeScale = 0.025f;
			gameoverCanvas.SetActive (true);
		}
		if (crashed) {
			float random = Random.Range (-5f, 5f);
			transform.position = Vector3.Lerp (transform.position, new Vector3 (random, 0, 20), Time.deltaTime * 1.5f);
			if (random >= 0) {
				transform.Rotate (0, 5, 0);
			} else {
				transform.Rotate (0, -5, 0);
			}
			particleSys.Simulate (Time.unscaledDeltaTime * 3, true, false);
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals ("Traffic")) {
			crashed = true;
			timeout = Time.time + 0.25f;
			tsm.gameObject.SetActive (false);
			playerController.enabled = false;
			animator.enabled = false;
			if (!screenShakedOnce) {
				cameraController.gameObject.GetComponent<Screenshake> ().Shake ();
				screenShakedOnce = true;
			}
			Time.timeScale = 0.1f;
			score.gameObject.SetActive (false);
			thisRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
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

	public void OnResetForGame() {
		crashed = false;
		Time.timeScale = 1;
		cameraController.gameObject.GetComponent<Blur> ().enabled = false;
		gameoverCanvas.SetActive (false);
		GameObject[] destroyableObjects = GameObject.FindGameObjectsWithTag ("Traffic");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		destroyableObjects = GameObject.FindGameObjectsWithTag ("Road");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		cameraController.MoveToOrigin (false);
		cameraController.MoveToPosition (3);
		roadSpawner.Reset ();
		roadSpawner.StartSpawning ();
		playerController.enabled = true;
		score.gameObject.SetActive (true);
		score.Reset ();
		tsm.gameObject.SetActive (true);
		animator.enabled = true;
		screenShakedOnce = false;
		thisRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		particleSys.Pause ();
		particleSys.Clear ();
	}

	public void OnResetForHome() {
		crashed = false;
		Time.timeScale = 1;

		GameObject[] destroyableObjects = GameObject.FindGameObjectsWithTag ("Traffic");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		destroyableObjects = GameObject.FindGameObjectsWithTag ("Road");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		cameraController.MoveToOrigin (true);
		roadSpawner.Reset ();
		playerController.enabled = true;
		playerController.gameObject.SetActive (false);
		score.Reset ();
		screenShakedOnce = false;
		animator.enabled = true;
		startCanvas.SetActive (true);
		gameoverCanvas.SetActive (false);
		thisRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		particleSys.Pause ();
		particleSys.Clear ();
		this.gameObject.SetActive (false);
	}
}
