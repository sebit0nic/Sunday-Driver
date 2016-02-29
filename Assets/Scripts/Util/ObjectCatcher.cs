using UnityEngine;
using System.Collections;

public class ObjectCatcher : MonoBehaviour {

	private TrafficSpawnManager tsm;
	private RoadSpawner roadSpawner;

	private void Start() {
		tsm = GameObject.Find ("Traffic Spawn Manager").GetComponent<TrafficSpawnManager> ();
		roadSpawner = GameObject.Find ("Road Spawner").GetComponent<RoadSpawner> ();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Traffic")) {
			tsm.DecreaseCurrentTrafficCount ();
		}
		if (other.gameObject.tag.Equals ("Road")) {
			roadSpawner.Spawn ();
		}
		other.gameObject.SetActive (false);
	}
}
