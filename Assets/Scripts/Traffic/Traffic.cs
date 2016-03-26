using UnityEngine;
using System.Collections;

public class Traffic : MonoBehaviour {

	public GameObject[] carModels;
	public Material[] allPaintJobs;
	private MeshRenderer meshRenderer;
	private int moveSpeed;
	private Quaternion initialRotation;
	private Vector3 initialPosition;
	private bool crashed;

	private void Start() {
		initialRotation = transform.rotation;
	}

	public void Init(int moveSpeed, Vector3 position) {
		this.moveSpeed = moveSpeed;
		crashed = false;
		transform.position = position;
		transform.rotation = initialRotation;
		for (int i = 0; i < carModels.Length; i++) {
			carModels[i].SetActive(false);
		}
		int random = Random.Range (0, carModels.Length);
		carModels[random].SetActive(true);
		meshRenderer = carModels[random].GetComponentInChildren<MeshRenderer>();
		meshRenderer.sharedMaterial = allPaintJobs[Random.Range(0, allPaintJobs.Length)];
	}

	private void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * moveSpeed);
		if (!crashed) {
			transform.rotation = initialRotation;
		} else {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (Random.Range(-5f, 5f), 0, 30), Time.deltaTime * 2.5f);
			transform.Rotate (0, 10, 0);
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
