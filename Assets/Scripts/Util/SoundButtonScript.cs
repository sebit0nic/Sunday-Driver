using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundButtonScript : MonoBehaviour {

	public Sprite soundSprite, noSoundSprite;
	private Image image;

	private void Awake() {
		image = GetComponent<Image> ();
	}

	private void Start() {
		if (PlayerPrefs.GetInt ("Sound", 0) == 0) {
			image.sprite = soundSprite;
		} else {
			image.sprite = noSoundSprite;
		}
	}

	public void ChangeGlobalVolume() {
		if (PlayerPrefs.GetInt ("Sound", 0) == 0) {
			PlayerPrefs.SetInt ("Sound", 1);
			PlayerPrefs.Save ();
			image.sprite = noSoundSprite;
			AudioListener.volume = 0;
		} else {
			PlayerPrefs.SetInt ("Sound", 0);
			PlayerPrefs.Save ();
			image.sprite = soundSprite;
			AudioListener.volume = 1;
		}
	}
}
