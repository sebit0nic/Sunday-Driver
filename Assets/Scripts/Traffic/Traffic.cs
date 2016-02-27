using UnityEngine;
using System.Collections;

public class Traffic : MonoBehaviour {

	private int moveSpeed;

	public void Init(int moveSpeed) {
		this.moveSpeed = moveSpeed;
	}

	private void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * moveSpeed);
	}

	public void SetMoveSpeed(int newMoveSpeed) {
		moveSpeed = newMoveSpeed;
	}

	public int GetMoveSpeed() {
		return moveSpeed;
	}
}
