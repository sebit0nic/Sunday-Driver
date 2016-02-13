using UnityEngine;
using System.Collections;

public class Road : MonoBehaviour {

	public float moveSpeed = 1;

	private void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * moveSpeed);
	}
}
