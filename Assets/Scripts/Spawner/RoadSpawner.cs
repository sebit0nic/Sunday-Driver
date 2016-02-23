using UnityEngine;
using System.Collections;

public class RoadSpawner : MonoBehaviour {

	public GameObject road;
	public float spawnInterval = 1;
	private float timer;

	private void Start() {
		for (int i = 0; i < 5; i++) {
			Instantiate (road, new Vector3 (0, 0, -10 * i), Quaternion.identity);
		}
		timer = Time.time + spawnInterval;
	}

	private void Update() {
		if (timer < Time.time) {
			Instantiate (road, transform.position, Quaternion.identity);
			timer = Time.time + spawnInterval;
		}
	}

	public void Reset() {
		timer = 0;
		for (int i = 0; i < 5; i++) {
			Instantiate (road, new Vector3 (0, 0, -10 * i), Quaternion.identity);
		}
	}
}
