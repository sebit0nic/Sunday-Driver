using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float smoothing;
	public Transform hotspot;
	private Vector3 newPosition;
	private float newSize;
	private Camera thisCamera;
	private float fastSmoothing = 1;

	private void Start() {
		newPosition = transform.position;
		thisCamera = GetComponent<Camera> ();
		newSize = thisCamera.orthographicSize;
	}

	public void MoveToOrigin(bool lerp) {
		fastSmoothing = 1;
		if (lerp) {
			newPosition = new Vector3 (3.2f, 8.25f, -1.45f);
			newSize = 6.75f;
		} else {
			transform.position = new Vector3(3.2f, 8.25f, -1.45f);
			thisCamera.orthographicSize = 6.75f;
		}
	}

	public void MoveToPosition(int lanes) {
		fastSmoothing = 1;
		switch (lanes) {
		case 3:
			newPosition = new Vector3 (3.2f, 8.25f, 5.4f);
			newSize = 6.75f;
			break;
		case 4:
			newPosition = new Vector3 (3.8f, 8.25f, 1.75f);
			newSize = 8;
			break;
		case 5:
			newPosition = new Vector3 (5f, 10.5f, 1.75f);
			newSize = 10;
			break;
		}
	}

	private void Update() {
		transform.position = Vector3.Lerp (transform.position, newPosition, Time.deltaTime * smoothing);
		thisCamera.orthographicSize = Mathf.Lerp (thisCamera.orthographicSize, newSize, Time.deltaTime * smoothing * fastSmoothing);
	}
}
