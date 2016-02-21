using UnityEngine;
using System.Collections;

public class Traffic : MonoBehaviour {

	private int moveSpeed;

	public void Init(int minMoveSpeed, int maxMoveSpeed) {
		moveSpeed = Random.Range (minMoveSpeed, maxMoveSpeed);
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
