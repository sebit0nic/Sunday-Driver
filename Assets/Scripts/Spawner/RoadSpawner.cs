using UnityEngine;
using System.Collections;

public class RoadSpawner : MonoBehaviour {

	public GameObject road;
	public float spawnInterval = 1;
	private float timer;
	private ObjectPool pool;

	private void Start() {
		pool = ObjectPool.CreateInstance<ObjectPool> ();
		pool.Init (road, 10, true);
		for (int i = 0; i < 5; i++) {
			//Instantiate (road, new Vector3 (0, 0, -10 * i), Quaternion.identity);
			GameObject pooledObject = pool.GetPooledObject();
			pooledObject.transform.position = new Vector3 (0, 0, -10 * i);
			pooledObject.SetActive (true);
		}
		timer = Time.time + spawnInterval;
	}

	private void Update() {
		if (timer < Time.time) {
			//Instantiate (road, transform.position, Quaternion.identity);
			GameObject pooledObject = pool.GetPooledObject();
			pooledObject.transform.position = transform.position;
			pooledObject.SetActive (true);

			timer = Time.time + spawnInterval;
		}
	}

	public void Reset() {
		timer = 0;
		for (int i = 0; i < 5; i++) {
			//Instantiate (road, new Vector3 (0, 0, -10 * i), Quaternion.identity);
			GameObject pooledObject = pool.GetPooledObject();
			pooledObject.transform.position = new Vector3 (0, 0, -10 * i);
			pooledObject.SetActive (true);
		}
	}
}
