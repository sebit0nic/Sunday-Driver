﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlScheme : MonoBehaviour {

	private int controlScheme = 0; //0 = swipe, 1 = tap
	public Image buttonSwipe, buttonTap;
	public GameObject leftButton, rightButton;
	public GameObject leftSwipe, rightSwipe;

	private void Start() {
		controlScheme = PlayerPrefs.GetInt ("Controls");
		if (controlScheme == 0) {
			buttonSwipe.color = Color.white;
			buttonTap.color = Color.gray;
			leftButton.SetActive (false);
			rightButton.SetActive (false);
			leftSwipe.SetActive (true);
			rightSwipe.SetActive (true);
		} else {
			buttonSwipe.color = Color.gray;
			buttonTap.color = Color.white;
			leftButton.SetActive (true);
			rightButton.SetActive (true);
			leftSwipe.SetActive (false);
			rightSwipe.SetActive (false);
		}
	}

	public void Change(int controlScheme) {
		this.controlScheme = controlScheme;
		PlayerPrefs.SetInt ("Controls", controlScheme);
		PlayerPrefs.Save ();
		if (controlScheme == 0) {
			buttonSwipe.color = Color.white;
			buttonTap.color = Color.gray;
			leftButton.SetActive (false);
			rightButton.SetActive (false);
			leftSwipe.SetActive (true);
			rightSwipe.SetActive (true);
		} else {
			buttonSwipe.color = Color.gray;
			buttonTap.color = Color.white;
			leftButton.SetActive (true);
			rightButton.SetActive (true);
			leftSwipe.SetActive (false);
			rightSwipe.SetActive (false);
		}
	}

	public int GetControls() {
		return controlScheme;
	}
}
