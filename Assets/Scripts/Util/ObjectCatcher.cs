using UnityEngine;
using System.Collections;

public class ObjectCatcher : MonoBehaviour {

	public TrafficSpawnManager tsm;
	private RoadSpawner roadSpawner;

	private void Start() {
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
