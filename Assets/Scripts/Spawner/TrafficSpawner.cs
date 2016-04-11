using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour {

	public GameObject traffic, puddle, rock, coin, tree;
	public bool canSpawn = true, canSpawnTree = false;
	private ObjectPool trafficPool, puddlePool, rockPool, coinPool, treePool;
	private float puddleTimer, coinTimer, treeTimer;

	private void Start() {
		trafficPool = ObjectPool.CreateInstance<ObjectPool> ();
		trafficPool.Init (traffic, 5, true);
		puddlePool = ObjectPool.CreateInstance<ObjectPool> ();
		puddlePool.Init (puddle, 2, true);
		rockPool = ObjectPool.CreateInstance<ObjectPool> ();
		rockPool.Init (rock, 2, true);
		coinPool = ObjectPool.CreateInstance<ObjectPool> ();
		coinPool.Init (coin, 2, true);
		if (canSpawnTree) {
			treePool = ObjectPool.CreateInstance<ObjectPool> ();
			treePool.Init (tree, 4, true);
		}
		puddleTimer = Time.time;
		coinTimer = Time.time;
		treeTimer = Time.time;
	}

	public void Spawn (int moveSpeed) {
		if (canSpawn) {
			Traffic trafficInstance = trafficPool.GetPooledObject ().GetComponent<Traffic> ();
			trafficInstance.transform.position = transform.position;
			trafficInstance.Init (moveSpeed, transform.position);
			trafficInstance.gameObject.SetActive (true);
		}
	}

	public void SpawnPuddle(int position) {
		if (canSpawn && puddleTimer < Time.time) {
			GameObject puddleInstance = puddlePool.GetPooledObject ();
			puddleInstance.transform.position = new Vector3 (transform.position.x - position * 0.05f, 0.026f, transform.position.z);
			puddleInstance.SetActive (true);
			puddleTimer = Time.time + 0.5f;
		}
	}

	public void SpawnRock(int position) {
		if (canSpawn) {
			GameObject rockInstance = rockPool.GetPooledObject ();
			rockInstance.transform.position = new Vector3 (transform.position.x, 0.255f, transform.position.z);
			rockInstance.SetActive (true);
		}
	}

	public void SpawnCoin(int position) {
		if (canSpawn && coinTimer < Time.time) {
			GameObject coinInstance = coinPool.GetPooledObject ();
			coinInstance.transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
			coinInstance.SetActive (true);
			coinTimer += 0.5f;
		}
	}

	public void SpawnTree(float positionX) {
		if (canSpawnTree && treeTimer < Time.time) {
			GameObject treeInstance = treePool.GetPooledObject ();
			treeInstance.transform.position = new Vector3 (positionX, 0.07f, transform.position.z);
			treeInstance.transform.Rotate (-90, 0, 0);
			treeInstance.SetActive (true);
			treeTimer += 1f;
		}
	}

	public void SetCanSpawn(bool canSpawn) {
		this.canSpawn = canSpawn;
	}
}
