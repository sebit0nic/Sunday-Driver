using UnityEngine;
using System.Collections;

public class MoveableScript : MonoBehaviour {

	public int minMoveSpeed = 1, maxMoveSpeed = 1;
	private int moveSpeed;

	private void Start() {
		moveSpeed = Random.Range (minMoveSpeed, maxMoveSpeed);
	}

	public void Init(int minMoveSpeed, int maxMoveSpeed) {
		this.minMoveSpeed = minMoveSpeed;
		this.maxMoveSpeed = maxMoveSpeed;
		moveSpeed = Random.Range (minMoveSpeed, maxMoveSpeed);
	}

	private void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * moveSpeed);
	}
}
