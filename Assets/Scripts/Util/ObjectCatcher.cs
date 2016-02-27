using UnityEngine;
using System.Collections;

public class ObjectCatcher : MonoBehaviour {

	private TrafficSpawnManager tsm;

	private void Start() {
		tsm = GameObject.Find ("Traffic Spawn Manager").GetComponent<TrafficSpawnManager> ();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Traffic")) {
			tsm.DecreaseCurrentTrafficCount ();
		}
		other.gameObject.SetActive (false);
	}
}
