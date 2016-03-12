using UnityEngine;
using System.Collections;

public class TrafficSpawnManager : MonoBehaviour {

	private TrafficSpawner[] trafficSpawner;
	public int maxAllowedTraffic = 10, currentTrafficCount = 0, allowedPositions = 3, blockedPosition = 0;
	public int currentSpeed = 10;
	public float minSpawnTime = 0.75f, maxSpawnTime = 3;

	[Header("Timer")]
	public float[] timer;
	public float[] nextSpawn;
	public float blockTimer = 0, blockInterval = 3;
	public float difficultyTimer = 0, difficultyInterval = 10;

	private void Start () {
		trafficSpawner = GetComponentsInChildren<TrafficSpawner> ();
		for (int i = 0; i < nextSpawn.Length; i++) {
			nextSpawn [i] = Random.Range (0f, 1f);
		}
		for (int i = 0; i < timer.Length; i++) {
			timer [i] = Time.time + nextSpawn [i];
		}

		blockedPosition = Random.Range (0, allowedPositions);
		blockTimer = Time.time + blockInterval;
		difficultyTimer = Time.time + difficultyInterval;
		difficultyInterval = 5;
	}

	private void Update () {
		for (int i = 0; i < allowedPositions; i++) {
			if (timer [i] < Time.time) {
				nextSpawn [i] = Random.Range (minSpawnTime, maxSpawnTime);
				timer [i] = Time.time + nextSpawn [i];
				if (currentTrafficCount < maxAllowedTraffic && i != blockedPosition) {
					trafficSpawner [i].Spawn (currentSpeed);
					currentTrafficCount++;
				}
			}
		}
		if (blockTimer < Time.time) {
			blockedPosition = Random.Range (0, allowedPositions);
			blockTimer = Time.time + blockInterval;
		}
		if (difficultyTimer < Time.time) {
			if (currentSpeed < 30) {
				currentSpeed++;
			}
			maxAllowedTraffic += 3;
			if (blockInterval > 1) {
				blockInterval -= 0.1f;
			}
			if (maxSpawnTime > 1.25f) {
				maxSpawnTime -= 0.25f;
			}
			difficultyTimer = Time.time + difficultyInterval;
		}
	}

	public void DecreaseCurrentTrafficCount() {
		currentTrafficCount--;
	}

	public void Reset() {
		currentSpeed = 10;
		maxAllowedTraffic = 10;
		maxSpawnTime = 3;
		blockInterval = 3;
		currentTrafficCount = 0;
		allowedPositions = 3;
		for (int i = 0; i < nextSpawn.Length; i++) {
			nextSpawn [i] = 0;
		}
		transform.position = new Vector3 (transform.position.x, transform.position.y, -40);
	}

	public void IncreaseAllowedPositions() {
		allowedPositions++;
		trafficSpawner [allowedPositions - 1].SetCanSpawn (true);
	}

	public void DecreaseAllowedPositions() {
		allowedPositions--;
		trafficSpawner [allowedPositions].SetCanSpawn (false);
	}

	public void AdjustSpawnerPosition() {
		switch (allowedPositions) {
		case 3:
			transform.position = new Vector3 (transform.position.x, transform.position.y, -40);
			break;
		case 4:
			transform.position = new Vector3 (transform.position.x, transform.position.y, -50);
			break;
		case 5:
			transform.position = new Vector3 (transform.position.x, transform.position.y, -60);
			break;
		}
	}

	private void OnEnable() {
		Reset ();
	}
}
