using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour {

	public GameObject traffic;
	public float spawnInterval = 1;
	private float timer;

	private void Update() {
		int randomLocation = Random.Range (-1, 2);
		if (timer >= spawnInterval) {
			Instantiate (traffic, new Vector3 (1.5f * randomLocation, transform.position.y, transform.position.z), Quaternion.identity);
			timer = 0;
		}
		timer += Time.deltaTime;
	}

	public void Reset() {
		timer = 0;
	}
}
