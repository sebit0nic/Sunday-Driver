using UnityEngine;
using System.Collections;

public class Traffic : MonoBehaviour {

	public GameObject[] carModels;
	public Material[] allPaintJobs;
	public ParticleSystem crashParticles;
	private MeshRenderer meshRenderer;
	private int moveSpeed;
	private Quaternion initialRotation;
	private Vector3 initialPosition;
	private bool crashed;
	private CapsuleCollider thisCollider;
	private AudioSource crashSound;

	private void Awake() {
		thisCollider = GetComponent<CapsuleCollider> ();
		crashSound = GetComponent<AudioSource> ();
	}

	private void Start() {
		initialRotation = transform.rotation;
	}

	public void Init(int moveSpeed, Vector3 position) {
		this.moveSpeed = moveSpeed;
		thisCollider.enabled = false;
		crashed = false;
		crashParticles.gameObject.SetActive (false);
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

		if (transform.position.z >= -31.5f && !thisCollider.enabled) {
			thisCollider.enabled = true;
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
		if (coll.gameObject.tag.Equals ("Obstacle") || coll.gameObject.tag.Equals("Traffic")) {
			crashParticles.gameObject.SetActive (true);
			moveSpeed = 25;
			if (transform.position.z < 2) {
				crashSound.pitch = Random.Range (0.8f, 1.2f);
				crashSound.Play ();
			}
		}
	}
}
