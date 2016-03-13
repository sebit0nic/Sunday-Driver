using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerCollision : MonoBehaviour {

	public RoadSpawner roadSpawner;
	public Score score;
	public GameObject gameoverCanvas, startCanvas;
	private PlayerController playerController;
	private TrafficSpawnManager tsm;
	private CameraController cameraController;
	private Animator animator;
	public Animator crashAnimator;
	public GameObject crashText;

	private float timeout;
	private bool crashed, screenShakedOnce;

	private void Start() {
		playerController = GetComponent<PlayerController> ();
		tsm = GameObject.Find ("Traffic Spawn Manager").GetComponent<TrafficSpawnManager> ();
		cameraController = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
		animator = GetComponent<Animator> ();
	}

	private void Update() {
		if (crashed && timeout < Time.time) {
			cameraController.gameObject.GetComponent<Blur> ().enabled = true;
			Time.timeScale = 0.025f;
			gameoverCanvas.SetActive (true);
			score.OnGameOver ();
		}
		if (crashed) {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (Random.Range (-5f, 5f), transform.position.y, 20), Time.deltaTime * 2.5f);
			transform.Rotate (0, 10, 0);
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals ("Traffic")) {
			crashed = true;
			tsm.gameObject.SetActive (false);
			playerController.enabled = false;
			animator.enabled = false;
			if (!screenShakedOnce) {
				timeout = Time.time + 0.3f;
				cameraController.gameObject.GetComponent<Screenshake> ().Shake ();
				screenShakedOnce = true;
				crashText.SetActive (true);
				crashAnimator.SetTrigger ("OnStart");
				score.SetStopped ();
			}
			Time.timeScale = 0.1f;
		}
		if (other.gameObject.tag.Equals ("Road")) {
			if (other.gameObject.name.Equals ("Road 3T4(Clone)")) {
				playerController.IncreaseMaxPosition();
				tsm.IncreaseAllowedPositions ();
				tsm.AdjustSpawnerPosition ();
				cameraController.MoveToPosition (4);
				crashText.transform.Translate (1.75f, 2, 10);
				crashText.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			}
			if (other.gameObject.name.Equals("Road 4T5(Clone)")) {
				playerController.IncreaseMaxPosition();
				tsm.IncreaseAllowedPositions ();
				tsm.AdjustSpawnerPosition ();
				cameraController.MoveToPosition (5);
				crashText.transform.Translate (2, 2, 10);
				crashText.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
			}
			if (other.gameObject.name.Equals("Road 5T4(Clone)")) {
				playerController.DecreaseMaxPosition();
				cameraController.MoveToPosition (4);
				tsm.AdjustSpawnerPosition ();
				crashText.transform.Translate (-2, -2, -10);
				crashText.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			}
			if (other.gameObject.name.Equals("Road 4T3(Clone)")) {
				playerController.DecreaseMaxPosition();
				cameraController.MoveToPosition (3);
				tsm.AdjustSpawnerPosition ();
				crashText.transform.Translate (-1.75f, -2, -10);
				crashText.transform.localScale = new Vector3 (1, 1, 1);
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
		score.Reset ();
		tsm.gameObject.SetActive (true);
		animator.enabled = true;
		screenShakedOnce = false;
		crashText.transform.position = new Vector3 (1.85f, 3f, -5f);
		crashText.transform.localScale = new Vector3 (1, 1, 1);
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
		screenShakedOnce = false;
		animator.enabled = true;
		startCanvas.SetActive (true);
		gameoverCanvas.SetActive (false);
		crashText.transform.position = new Vector3 (1.85f, 3f, -5f);
		crashText.transform.localScale = new Vector3 (1, 1, 1);
		this.gameObject.SetActive (false);
	}
}
