using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour {

	public MoveableScript traffic;
	public bool canSpawn = true;
	public float minSpawnInterval = 1, maxSpawnInterval = 1;
	public int minTrafficSpeed = 1, maxTrafficSpeed = 1;
	private float spawnInterval;
	private float timer;

	private void Start() {
		spawnInterval = Random.Range (minSpawnInterval, maxSpawnInterval);
	}

	private void Update() {
		if (canSpawn) {
			if (timer >= spawnInterval) {
				MoveableScript trafficInstance = Instantiate (traffic, transform.position, Quaternion.identity) as MoveableScript;
				trafficInstance.Init (minTrafficSpeed, maxTrafficSpeed);
				timer = 0;
				spawnInterval = Random.Range (minSpawnInterval, maxSpawnInterval);
			}
			timer += Time.deltaTime;
		}
	}

	public void Reset() {
		timer = 0;
	}
}
