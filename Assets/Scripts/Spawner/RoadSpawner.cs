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
	}

	private void Update() {
		if (timer >= spawnInterval) {
			Instantiate (road, transform.position, Quaternion.identity);
			timer = 0;
		}
		timer += Time.deltaTime;
	}

	public void Reset() {
		timer = 0;
		for (int i = 0; i < 5; i++) {
			Instantiate (road, new Vector3 (0, 0, -10 * i), Quaternion.identity);
		}
	}
}
