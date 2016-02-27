using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour {

	public GameObject traffic;
	public bool canSpawn = true;
	private ObjectPool pool;

	private void Start() {
		pool = ObjectPool.CreateInstance<ObjectPool> ();
		pool.Init (traffic, 30, true);
	}

	public void Spawn (int moveSpeed) {
		if (canSpawn) {
			Traffic trafficInstance = pool.GetPooledObject ().GetComponent<Traffic> ();
			trafficInstance.transform.position = transform.position;
			trafficInstance.Init (moveSpeed);
			trafficInstance.gameObject.SetActive (true);
		}
	}
}
