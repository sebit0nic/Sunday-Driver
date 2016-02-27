using UnityEngine;
using System.Collections;

public class TrafficSpawnManager : MonoBehaviour {

	private TrafficSpawner[] trafficSpawner;
	public float[] timer;
	public float[] nextSpawn;
	public int maxAllowedTraffic = 10, currentTrafficCount = 0, allowedPositions = 3, blockedPosition = 0;
	private int currentSpeed = 10;

	public float blockTimer = 0, blockInterval = 3;

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
	}

	private void Update () {
		for (int i = 0; i < allowedPositions; i++) {
			if (timer [i] < Time.time) {
				nextSpawn [i] = Random.Range (1f, 4.0f);
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
	}

	public void DecreaseCurrentTrafficCount() {
		currentTrafficCount--;
	}
}
