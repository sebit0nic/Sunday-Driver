using UnityEngine;
using System.Collections;

public class RoadSpawner : MonoBehaviour {

	public GameObject road;
	public float spawnInterval = 1;
	private float timer;
	private ObjectPool objectPool;

	private void Start() {
		objectPool = new ObjectPool (road, 10, false);

		for (int i = 0; i < 4; i++) {
			GameObject obj = objectPool.GetPooledObject ();
			obj.transform.position = new Vector3 (0, 0, -10 * i);
			obj.SetActive (true);
		}
	}

	private void Update() {
		if (timer >= spawnInterval) {
			GameObject obj = objectPool.GetPooledObject ();
			obj.transform.position = transform.position;
			obj.SetActive (true);
			timer = 0;
		}
		timer += Time.deltaTime;
	}
}
