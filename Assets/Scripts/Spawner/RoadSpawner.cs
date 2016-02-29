using UnityEngine;
using System.Collections;

public class RoadSpawner : MonoBehaviour {

	public GameObject road;
	private ObjectPool pool;
	private GameObject lastSpawnedObject;

	private void Start() {
		pool = ObjectPool.CreateInstance<ObjectPool> ();
		pool.Init (road, 10, true);
		for (int i = 0; i < 4; i++) {
			GameObject pooledObject = pool.GetPooledObject();
			pooledObject.transform.position = new Vector3 (0, 0, -30 * i);
			pooledObject.SetActive (true);
			lastSpawnedObject = pooledObject;
		}
	}

	public void Spawn() {
		GameObject pooledObject = pool.GetPooledObject();
		pooledObject.transform.position = new Vector3 (transform.position.x, transform.position.y, lastSpawnedObject.transform.position.z - 30);
		pooledObject.SetActive (true);
		lastSpawnedObject = pooledObject;
	}

	public void Reset() {
		for (int i = 0; i < 4; i++) {
			GameObject pooledObject = pool.GetPooledObject();
			pooledObject.transform.position = new Vector3 (0, 0, -30 * i);
			pooledObject.SetActive (true);
			lastSpawnedObject = pooledObject;
		}
	}
}
