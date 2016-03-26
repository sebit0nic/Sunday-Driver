﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerCollision : MonoBehaviour {

	public RoadSpawner roadSpawner;
	public Score score;
	public CoinCounter coinCounter;
	public GameObject gameoverCanvas, startCanvas, gameCanvas;
	private PlayerController playerController;
	private TrafficSpawnManager tsm;
	private CameraController cameraController;
	private Animator animator;
	public Animator crashAnimator;
	public GameObject crashText;
	public GameObject marker;
	public NatureSpawner natureSpawner;

	private float timeout;
	private bool crashed, screenShakedOnce, canvasActivatedOnce;

	private Vector3 crashTextPos1, crashTextPos2, crashTextPos3;
	private Vector3 crashTextScale1, crashTextScale2, crashTextScale3;

	private void Start() {
		playerController = GetComponent<PlayerController> ();
		tsm = GameObject.Find ("Traffic Spawn Manager").GetComponent<TrafficSpawnManager> ();
		cameraController = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
		animator = GetComponent<Animator> ();

		crashTextPos1 = new Vector3 (2f, 3f, -5f);
		crashTextPos2 = new Vector3 (3.75f, 8f, 7.5f);
		crashTextPos3 = new Vector3 (4.75f, 10f, 7.5f);
		crashTextScale1 = new Vector3 (1f, 1f, 1f);
		crashTextScale2 = new Vector3 (1.2f, 1.2f, 1.2f);
		crashTextScale3 = new Vector3 (1.5f, 1.5f, 1.5f);
	}

	private void Update() {
		if (crashed && timeout < Time.time) {
			cameraController.gameObject.GetComponent<Blur> ().enabled = true;
			Time.timeScale = 0.025f;
			if (!canvasActivatedOnce) {
				gameoverCanvas.SetActive (true);
				canvasActivatedOnce = true;
			}
			score.OnGameOver ();
			gameCanvas.SetActive (false);
		}
		if (crashed) {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (Random.Range (-5f, 5f), transform.position.y, 30), Time.deltaTime * 2.5f);
			transform.Rotate (0, 10, 0);
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals ("Traffic") || other.gameObject.tag.Equals("Rock")) {
			crashed = true;
			tsm.gameObject.SetActive (false);
			playerController.enabled = false;
			animator.enabled = false;
			if (!screenShakedOnce) {
				timeout = Time.time + 0.3f;
				cameraController.gameObject.GetComponent<Screenshake> ().Shake (0.1f, 0.0025f);
				screenShakedOnce = true;
				crashText.SetActive (true);
				crashAnimator.SetTrigger ("OnStart");
				score.SetStopped ();
			}
			Time.timeScale = 0.1f;
			marker.SetActive (false);
		}
		if (other.gameObject.tag.Equals ("Road")) {
			if (other.gameObject.name.Equals ("Road 3T4(Clone)")) {
				playerController.IncreaseMaxPosition();
				tsm.IncreaseAllowedPositions ();
				tsm.AdjustSpawnerPosition ();
				cameraController.MoveToPosition (4);
				crashText.transform.position = crashTextPos2;
				crashText.transform.localScale = crashTextScale2;
			}
			if (other.gameObject.name.Equals("Road 4T5(Clone)")) {
				playerController.IncreaseMaxPosition();
				tsm.IncreaseAllowedPositions ();
				tsm.AdjustSpawnerPosition ();
				cameraController.MoveToPosition (5);
				crashText.transform.position = crashTextPos3;
				crashText.transform.localScale = crashTextScale3;
			}
			if (other.gameObject.name.Equals("Road 5T4(Clone)")) {
				playerController.DecreaseMaxPosition();
				cameraController.MoveToPosition (4);
				tsm.AdjustSpawnerPosition ();
				crashText.transform.position = crashTextPos2;
				crashText.transform.localScale = crashTextScale2;
			}
			if (other.gameObject.name.Equals("Road 4T3(Clone)")) {
				playerController.DecreaseMaxPosition();
				cameraController.MoveToPosition (3);
				tsm.AdjustSpawnerPosition ();
				crashText.transform.position = crashTextPos1;
				crashText.transform.localScale = crashTextScale1;
			}
		}

		if (other.gameObject.tag.Equals ("Puddle") && !screenShakedOnce) {
			animator.SetTrigger ("OnSpin");
		}

		if (other.gameObject.tag.Equals ("Coin")) {
			coinCounter.IncreaseCoinCounter(1);
		}
	}

	public void OnResetForGame() {
		ResetObjects ();
		cameraController.gameObject.GetComponent<Blur> ().enabled = false;
		cameraController.MoveToOrigin (false);
		cameraController.MoveToPosition (3);
		roadSpawner.StartSpawning ();
		gameCanvas.SetActive (true);
		score.Reset ();
		tsm.gameObject.SetActive (true);
		animator.SetTrigger ("OnIdle");
		screenShakedOnce = false;
	}

	public void OnResetForHome() {
		ResetObjects ();
		cameraController.MoveToOrigin (true);
		playerController.gameObject.SetActive (false);
		screenShakedOnce = false;
		startCanvas.SetActive (true);
		this.gameObject.SetActive (false);
	}

	private void ResetObjects() {
		crashed = false;
		Time.timeScale = 1;
		gameoverCanvas.SetActive (false);
		GameObject[] destroyableObjects = GameObject.FindGameObjectsWithTag ("Traffic");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		destroyableObjects = GameObject.FindGameObjectsWithTag ("Road");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		destroyableObjects = GameObject.FindGameObjectsWithTag ("Puddle");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		destroyableObjects = GameObject.FindGameObjectsWithTag ("Rock");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		destroyableObjects = GameObject.FindGameObjectsWithTag ("Coin");
		for (int i = 0; i < destroyableObjects.Length; i++) {
			destroyableObjects[i].SetActive(false);
		}
		roadSpawner.Reset ();
		playerController.enabled = true;
		playerController.Reset ();
		animator.enabled = true;
		crashText.transform.position = crashTextPos1;
		crashText.transform.localScale = crashTextScale1;
		marker.SetActive (true);
		natureSpawner.Reset ();
		canvasActivatedOnce = false;
	}
}
