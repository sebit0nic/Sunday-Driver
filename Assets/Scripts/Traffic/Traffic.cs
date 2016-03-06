using UnityEngine;
using System.Collections;

public class Traffic : MonoBehaviour {

	private int moveSpeed;
	private Quaternion initialRotation;
	private bool crashed;

	private void Start() {
		initialRotation = transform.rotation;
	}

	public void Init(int moveSpeed) {
		this.moveSpeed = moveSpeed;
	}

	private void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * moveSpeed);
		if (!crashed) {
			transform.rotation = initialRotation;
		}
	}

	public void SetMoveSpeed(int newMoveSpeed) {
		moveSpeed = newMoveSpeed;
	}

	public int GetMoveSpeed() {
		return moveSpeed;
	}

	private void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag.Equals ("Player")) {
			moveSpeed = 0;
			crashed = true;
		}
	}
}
