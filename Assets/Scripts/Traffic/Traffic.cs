using UnityEngine;
using System.Collections;

public class Traffic : MonoBehaviour {

	public GameObject[] carModels;
	public Material[] paintJobs;
	private MeshRenderer meshRenderer;
	private GameObject[] instantiatedCarModels;
	private int moveSpeed;
	private Quaternion initialRotation;
	private bool crashed;

	private void Awake() {
		instantiatedCarModels = new GameObject[20];
		for (int i = 0; i < carModels.Length; i++) {
			instantiatedCarModels [i] = Instantiate (carModels [i], Vector3.zero, Quaternion.identity) as GameObject;
			instantiatedCarModels [i].transform.Rotate (0, 180, 0);
			instantiatedCarModels [i].transform.parent = this.gameObject.transform;
			instantiatedCarModels [i].SetActive (false);
		}
	}

	private void Start() {
		initialRotation = transform.rotation;
	}

	public void Init(int moveSpeed) {
		this.moveSpeed = moveSpeed;
		for (int i = 0; i < carModels.Length; i++) {
			instantiatedCarModels [i].SetActive (false);
		}
		int random = Random.Range (0, carModels.Length);
		instantiatedCarModels [random].SetActive (true);
		meshRenderer = instantiatedCarModels [random].GetComponentInChildren<MeshRenderer> ();
		meshRenderer.material = paintJobs [Random.Range (0, paintJobs.Length)];
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
