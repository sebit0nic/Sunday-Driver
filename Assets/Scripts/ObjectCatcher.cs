using UnityEngine;
using System.Collections;

public class ObjectCatcher : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		//other.gameObject.SetActive (false);
		Destroy(other.gameObject);
	}
}
