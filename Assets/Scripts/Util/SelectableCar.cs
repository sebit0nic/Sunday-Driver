using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectableCar : MonoBehaviour {

	private bool selected;
	public ShopCameraController scc;
	public int index;

	private void Update() {
		if (selected) {
			transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), Time.deltaTime * 3);
			transform.Rotate (0, 2, 0);
		} else {
			transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 3);
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.identity, Time.deltaTime * 3);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Selector")) {
			selected = true;
			scc.SetFocusX (transform.position.x, index);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag.Equals ("Selector")) {
			selected = false;
		}
	}
}
