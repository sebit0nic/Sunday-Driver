using UnityEngine;
using System.Collections;

public class CollisionAvoidance : MonoBehaviour {

	public Traffic traffic;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Traffic")) {
			Traffic temp = other.gameObject.GetComponent<Traffic> ();
			traffic.SetMoveSpeed (temp.GetMoveSpeed ());
		}
	}
}
