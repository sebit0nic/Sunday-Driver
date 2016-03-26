using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Selector : MonoBehaviour {

	public Text carNameText, carNameShadow;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			carNameText.text = other.gameObject.name;
			carNameShadow.text = other.gameObject.name;
		}
	}
}
