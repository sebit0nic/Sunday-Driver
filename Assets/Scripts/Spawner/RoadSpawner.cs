using UnityEngine;
using System.Collections;

public class RoadSpawner : MonoBehaviour {

	private GameObject parent;
	public GameObject road3L, road3T4, road4L, road4T5, road5L, road5T4, road4T3;
	private ObjectPool pool3L, pool4L, pool5L;
	private GameObject transition3T4, transition4T5, transition5T4, transition4T3;
	private GameObject lastSpawnedObject;
	private int lanes = 3;
	private bool transitioningPlus = false, transitioningMinus = false;
	private float transitionTimer;
	public TrafficSpawnManager tsm;
	public NatureSpawner natureSpawner;

	private bool playing = false;

	private void Awake() {
		parent = GameObject.Find ("Instantiated Objects");
	}

	private void Start() {
		pool3L = ObjectPool.CreateInstance<ObjectPool> ();
		pool3L.Init (road3L, 5, true);
		for (int i = 0; i < 4; i++) {
			GameObject pooledObject = pool3L.GetPooledObject();
			pooledObject.transform.position = new Vector3 (0, 0, -30 * i);
			pooledObject.SetActive (true);
			lastSpawnedObject = pooledObject;
		}
		pool4L = ObjectPool.CreateInstance<ObjectPool> ();
		pool4L.Init (road4L, 5, true);
		pool5L = ObjectPool.CreateInstance<ObjectPool> ();
		pool5L.Init (road5L, 5, true);

		transition3T4 = (GameObject)Instantiate (road3T4);
		transition3T4.transform.SetParent (parent.transform, false);
		transition3T4.SetActive (false);
		transition4T5 = (GameObject)Instantiate (road4T5);
		transition4T5.transform.SetParent (parent.transform, false);
		transition4T5.SetActive (false);
		transition5T4 = (GameObject)Instantiate (road5T4);
		transition5T4.transform.SetParent (parent.transform, false);
		transition5T4.SetActive (false);
		transition4T3 = (GameObject)Instantiate (road4T3);
		transition4T3.transform.SetParent (parent.transform, false);
		transition4T3.SetActive (false);

		transitionTimer = Time.time + Random.Range (5, 10);
	}

	public void Spawn() {
		GameObject pooledObject = null;
		switch (lanes) {
		case 3:
			if (transitioningPlus) {
				lanes++;
				pooledObject = transition3T4;
				transitioningPlus = false;
			} else if (transitioningMinus) {
				pooledObject = pool3L.GetPooledObject ();
				transitioningMinus = false;
			} else {
				pooledObject = pool3L.GetPooledObject ();
			}
			break;
		case 4:
			if (transitioningPlus) {
				lanes++;
				pooledObject = transition4T5;
				transitioningPlus = false;
			} else if (transitioningMinus) {
				lanes--;
				pooledObject = transition4T3;
				natureSpawner.DecreaseLeftOffset ();
				transitioningMinus = false;
			} else {
				pooledObject = pool4L.GetPooledObject ();
			}
			break;
		case 5:
			if (transitioningPlus) {
				pooledObject = pool5L.GetPooledObject ();
				transitioningPlus = false;
			} else if (transitioningMinus) {
				lanes--;
				pooledObject = transition5T4;
				natureSpawner.DecreaseLeftOffset ();
				transitioningMinus = false;
			} else {
				pooledObject = pool5L.GetPooledObject ();
			}
			break;
		}
		pooledObject.transform.position = new Vector3 (transform.position.x, transform.position.y, lastSpawnedObject.transform.position.z - 30);
		pooledObject.SetActive (true);
		lastSpawnedObject = pooledObject;

		if (playing) {
			if (transitionTimer < Time.time) {
				int random = Random.Range (0, 1);
				if (random == 0) {
					transitioningPlus = true;
					if (lanes < 5) {
						natureSpawner.IncreaseLeftOffset ();
					}
				} else {
					transitioningMinus = true;
					if (lanes > 3) {
						tsm.DecreaseAllowedPositions ();
					}
				}
				transitionTimer = Time.time + Random.Range (5, 15);
			}
		}
	}

	public void StartSpawning() {
		transitionTimer = Time.time + 5;
		playing = true;
	}

	public void Reset() {
		transitionTimer = Time.time + 5;
		lanes = 3;
		transitioningPlus = false;
		transitioningMinus = false;
		playing = false;
		for (int i = 0; i < 4; i++) {
			if (pool3L != null) {
				GameObject pooledObject = pool3L.GetPooledObject();
				pooledObject.transform.position = new Vector3 (0, 0, -30 * i);
				pooledObject.SetActive (true);
				lastSpawnedObject = pooledObject;
			}
		}
	}

	public void IncreaseLanes() {
		transitioningPlus = true;
	}

	public void DecreaseLanes() {
		transitioningMinus = true;
	}
}
