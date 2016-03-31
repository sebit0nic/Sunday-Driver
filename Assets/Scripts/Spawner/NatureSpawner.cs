using UnityEngine;
using System.Collections;

public class NatureSpawner : MonoBehaviour {

	public float rightMaxX, rightMinX, leftMaxX, leftMinX;
	public GameObject naturePrefab;
	private float timer;
	private ObjectPool naturePool;

	private void Start() {
		timer = Time.time + 0.2f;
		naturePool = ObjectPool.CreateInstance<ObjectPool> ();
		naturePool.Init (naturePrefab, 60, true);
		for (int i = 0; i < 40; i++) {
			GameObject pooledObject = naturePool.GetPooledObject();
			if (i % 2 == 0) {
				pooledObject.transform.position = new Vector3 (Random.Range(rightMinX, rightMaxX), 0, Random.Range(-85, -10));
			} else {
				pooledObject.transform.position = new Vector3 (Random.Range(leftMinX, leftMaxX), 0, Random.Range(-85, -10));
			}
			pooledObject.SetActive (true);
		}
	}

	private void Update() {
		if (timer < Time.time) {
			GameObject pooledObject = naturePool.GetPooledObject ();
			pooledObject.transform.position = new Vector3 (Random.Range (rightMinX, rightMaxX), 0, Random.Range (-93, -90));
			pooledObject.SetActive (true);

			pooledObject = naturePool.GetPooledObject ();
			pooledObject.transform.position = new Vector3 (Random.Range (leftMinX, leftMaxX), 0, Random.Range (-93, -90));
			pooledObject.SetActive (true);
			timer = Time.time + 0.3f;
		}
	}

	public void IncreaseLeftOffset() {
		leftMinX += 2f;
		leftMaxX += 2f;
	}

	public void DecreaseLeftOffset() {
		leftMinX -= 2f;
		leftMaxX -= 2f;
	}

	public void Reset() {
		leftMinX = 5f;
		leftMaxX = 9f;
	}
}
