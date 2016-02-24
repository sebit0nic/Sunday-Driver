using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour {

	public GameObject traffic;
	public bool canSpawn = true;
	public float minSpawnInterval = 1, maxSpawnInterval = 1;
	public int minTrafficSpeed = 1, maxTrafficSpeed = 1;
	private float spawnInterval;
	private float timer;
	private ObjectPool pool;

	private void Start() {
		spawnInterval = Random.Range (minSpawnInterval, maxSpawnInterval);
		pool = ObjectPool.CreateInstance<ObjectPool> ();
		pool.Init (traffic, 30, true);
	}

	private void Update() {
		if (canSpawn) {
			if (timer >= spawnInterval) {
				/*Traffic trafficInstance = Instantiate (traffic, transform.position, traffic.transform.rotation) as Traffic;
				trafficInstance.Init (minTrafficSpeed, maxTrafficSpeed);*/
				Traffic trafficInstance = pool.GetPooledObject ().GetComponent<Traffic> ();
				trafficInstance.transform.position = transform.position;
				trafficInstance.Init (minTrafficSpeed, maxTrafficSpeed);
				trafficInstance.gameObject.SetActive (true);

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
