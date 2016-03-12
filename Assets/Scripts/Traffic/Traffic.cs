using UnityEngine;
using System.Collections;

public class Traffic : MonoBehaviour {

	public GameObject[] carModels;
	public Material paintJobs;
	private Material instantiatedMaterial;
	private MeshRenderer meshRenderer;
	private GameObject[] instantiatedCarModels;
	private int moveSpeed;
	private Quaternion initialRotation;
	private Vector3 initialPosition;
	private bool crashed;

	private void Awake() {
		instantiatedCarModels = new GameObject[20];
		instantiatedMaterial = Instantiate (paintJobs) as Material;
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

	public void Init(int moveSpeed, Vector3 position) {
		this.moveSpeed = moveSpeed;
		crashed = false;
		transform.position = position;
		transform.rotation = initialRotation;
		for (int i = 0; i < carModels.Length; i++) {
			instantiatedCarModels [i].SetActive (false);
		}
		int random = Random.Range (0, carModels.Length);
		instantiatedCarModels [random].SetActive (true);
		meshRenderer = instantiatedCarModels [random].GetComponentInChildren<MeshRenderer> ();
		instantiatedMaterial.SetTextureOffset ("_MainTex", new Vector2 (Random.Range(0f, 1f), Random.Range(0f, 1f)));
		meshRenderer.material = instantiatedMaterial;
	}

	private void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * moveSpeed);
		if (!crashed) {
			transform.rotation = initialRotation;
		} else {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (Random.Range(-5f, 5f), 0, 20), Time.deltaTime * 2.5f);
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
