﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int score = 0, lastScore = 0;
	private float floatScore = 0;
	private Text scoreText;

	private void Start() {
		scoreText = GetComponent<Text> ();
	}

	private void Update() {
		floatScore += Time.deltaTime;
		score = Mathf.RoundToInt (floatScore);
		if (score > lastScore) {
			scoreText.text = "Score: " + score;
			lastScore = score;
		}
	}

	public void Reset() {
		score = 0;
		floatScore = 0;
		scoreText.text = "Score: 0";
	}
}
