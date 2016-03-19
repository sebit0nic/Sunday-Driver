using UnityEngine;
using System.Collections;

public class TrafficSpawnManager : MonoBehaviour {

	private TrafficSpawner[] trafficSpawner;
	public int maxAllowedTraffic = 10, currentTrafficCount = 0, allowedPositions = 3, blockedPosition = 0;
	public int currentSpeed = 10;
	public float minSpawnTime = 0.75f, maxSpawnTime = 3;

	[Header("Timer")]
	public float[] timer;
	public float blockTimer = 0, blockInterval = 3;
	public float difficultyTimer = 0, difficultyInterval = 10;

	private void Start () {
		trafficSpawner = GetComponentsInChildren<TrafficSpawner> ();
		for (int i = 0; i < timer.Length; i++) {
			timer [i] = Time.time + Random.Range (0f, 1f);
		}

		blockedPosition = Random.Range (0, allowedPositions);
		blockTimer = Time.time + blockInterval;
		difficultyTimer = Time.time + difficultyInterval;
		difficultyInterval = 5;
	}

	private void Update () {
		for (int i = 0; i < allowedPositions; i++) {
			if (timer [i] < Time.time) {
				timer [i] = Time.time + Random.Range (minSpawnTime, maxSpawnTime);
				if (currentTrafficCount < maxAllowedTraffic && i != blockedPosition) {
					trafficSpawner [i].Spawn (currentSpeed);
					currentTrafficCount++;
				}
			}
		}
		if (blockTimer < Time.time) {
			timer [blockedPosition] = Time.time + Random.Range (1f, 2f);
			if (Random.Range (0, 3) == 0 && allowedPositions > 3) {
				trafficSpawner [blockedPosition].SpawnRock (blockedPosition);
			}

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
			if (currentSpeed > 20 && minSpawnTime > 0.25f) {
				minSpawnTime -= 0.25f;
			}
			difficultyTimer = Time.time + difficultyInterval;
		}
	}

	public void DecreaseCurrentTrafficCount() {
		currentTrafficCount--;
		if (Random.Range (0, 5) == 0) {
			//Spawn random obstacle
			int random = Random.Range(0, allowedPositions);
			trafficSpawner[random].SpawnPuddle(random);
		}
	}

	public void Reset() {
		currentSpeed = 10;
		maxAllowedTraffic = 10;
		maxSpawnTime = 3;
		blockInterval = 3;
		blockTimer = Time.time + blockInterval;
		currentTrafficCount = 0;
		allowedPositions = 3;
		transform.position = new Vector3 (transform.position.x, transform.position.y, -40);
		for (int i = 0; i < timer.Length; i++) {
			timer [i] = Time.time + Random.Range (0f, 1f);
		}
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
