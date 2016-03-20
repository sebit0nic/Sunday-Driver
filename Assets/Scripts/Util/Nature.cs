using UnityEngine;
using System.Collections;

public class Nature : MonoBehaviour {

	public GameObject[] natureObjects;
	private GameObject[] instantiatedNatureObjects;
	private bool firstRound = true;

	private void Start() {
		instantiatedNatureObjects = new GameObject[20];
		for (int i = 0; i < natureObjects.Length; i++) {
			instantiatedNatureObjects[i] = Instantiate (natureObjects [i], Vector3.zero, Quaternion.identity) as GameObject;
			instantiatedNatureObjects[i].transform.Rotate (-90, 0, 0);
			instantiatedNatureObjects[i].transform.parent = this.gameObject.transform;
			instantiatedNatureObjects[i].SetActive (false);
		}
		int random = Random.Range (0, natureObjects.Length);
		instantiatedNatureObjects [random].transform.position = transform.position;
		instantiatedNatureObjects [random].SetActive (true);
		firstRound = false;
	}

	private void OnEnable() {
		if (!firstRound) {
			for (int i = 0; i < natureObjects.Length; i++) {
				instantiatedNatureObjects [i].SetActive (false);
			}
			int random = Random.Range (0, natureObjects.Length);
			instantiatedNatureObjects [random].transform.position = transform.position;
			instantiatedNatureObjects [random].SetActive (true);
		}
	}
}
