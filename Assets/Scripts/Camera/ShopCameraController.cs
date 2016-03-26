using UnityEngine;
using System.Collections;

public class ShopCameraController : MonoBehaviour {

	private Vector3 tempVector;
	private float focusX;
	private bool isMobile = false;
	public int selectedCar = 0;
	public SelectableCar[] cars;
	private bool[] bought;
	public GameObject selectButton, buyButton;
	public CoinCounter coinCounter;
	public GameObject marker;
	public float[] markerPositions;

	private void Start() {
		tempVector = transform.position;
		bought = new bool[cars.Length];
		if (Application.platform == RuntimePlatform.Android) {
			isMobile = true;
		}
		for (int i = 0; i < cars.Length; i++) {
			if (i == 0) {
				PlayerPrefs.SetInt ("Bought0", 1);
				PlayerPrefs.Save ();
			}
			if (PlayerPrefs.GetInt ("Bought" + i) == 1) {
				bought [i] = true;
			}
		}
	}

	private void Update() {
		//Mobile
		if (isMobile) {
			if (Input.touchCount < 1) {
				transform.position = Vector3.Lerp (transform.position, new Vector3 (focusX, transform.position.y, transform.position.z), Time.unscaledDeltaTime * 3);
			} else {
				Touch touch = Input.GetTouch(0);
				tempVector.x = transform.position.x + touch.deltaPosition.x * Time.unscaledDeltaTime;
				transform.position = tempVector;
			}
		}

		//PC
		if (!isMobile) {
			if (Input.GetAxis ("Horizontal") != 0) {
				tempVector.x = transform.position.x + Input.GetAxis ("Horizontal") * -1 * Time.unscaledDeltaTime * 3;
				transform.position = tempVector;
			} else {
				transform.position = Vector3.Lerp (transform.position, new Vector3 (focusX, transform.position.y, transform.position.z), Time.unscaledDeltaTime * 3);
			}
		}
	}

	public void SetFocusX(float focusX, int index) {
		this.focusX = focusX;
		selectedCar = index;
		if (bought [index]) {
			selectButton.SetActive (true);
			buyButton.SetActive (false);
		} else {
			selectButton.SetActive (false);
			buyButton.SetActive (true);
		}
	}

	public void SelectCar() {
		PlayerPrefs.SetInt ("SelectedCar", selectedCar);
		PlayerPrefs.Save ();
		marker.transform.position = new Vector3 (markerPositions [selectedCar], marker.transform.position.y, marker.transform.position.z);
	}

	public void BuyCar() {
		if (PlayerPrefs.GetInt ("Coins") >= 100) {
			PlayerPrefs.SetInt ("Bought" + selectedCar, 1);
			int coins = PlayerPrefs.GetInt ("Coins");
			PlayerPrefs.SetInt ("Coins", coins - 100);
			PlayerPrefs.Save ();
			bought [selectedCar] = true;
			selectButton.SetActive (true);
			buyButton.SetActive (false);
			coinCounter.Refresh ();
		}
	}

	private void OnEnable() {
		marker.transform.position = new Vector3 (markerPositions [PlayerPrefs.GetInt ("SelectedCar")], marker.transform.position.y, marker.transform.position.z);
	}
}
