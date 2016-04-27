using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Selector : MonoBehaviour {

	public Text carNameText, carNameShadow;
	private AudioSource thisAudioSource;

	private void Awake() {
		thisAudioSource = GetComponent<AudioSource> ();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			thisAudioSource.Play ();
			carNameText.text = other.gameObject.name;
			carNameShadow.text = other.gameObject.name;
		}
	}
}
