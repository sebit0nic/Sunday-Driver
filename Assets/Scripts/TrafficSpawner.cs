using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour {

	public GameObject traffic;
	public float spawnInterval = 1;
	private float timer;
	private ObjectPool objectPool;

	private void Start() {
		//objectPool = ObjectPool.CreateInstance ("ObjectPool") as ObjectPool;
		//objectPool.Init (traffic, 20, true);
	}

	private void Update() {
		int randomLocation = Random.Range (-1, 2);
		if (timer >= spawnInterval) {
			/*GameObject obj = objectPool.GetPooledObject ();
			obj.transform.position = new Vector3 (1.5f * randomLocation, transform.position.y, transform.position.z); 
			obj.SetActive (true);*/
			Instantiate (traffic, new Vector3 (1.5f * randomLocation, transform.position.y, transform.position.z), Quaternion.identity);
			timer = 0;
		}
		timer += Time.deltaTime;
	}
}
