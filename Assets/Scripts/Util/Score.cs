using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int score = 0, lastScore = 0;
	private float floatScore = 0;
	private Text scoreText;
	private bool stopped;

	private void Start() {
		scoreText = GetComponent<Text> ();
	}

	private void Update() {
		floatScore += Time.deltaTime;
		score = Mathf.RoundToInt (floatScore);
		if (score > lastScore) {
			scoreText.text = score.ToString ();
			lastScore = score;
		}
	}

	public void Reset() {
		score = 0;
		lastScore = 0;
		floatScore = 0;
		scoreText.text = "0";
	}
}
