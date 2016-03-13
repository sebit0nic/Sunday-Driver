using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public bool clearMemory;

	public Text endscoreText, endscoreShadow, highscoreText, highscoreShadow;
	private int score = 0, highscore = 0, lastScore = 0;
	private int tempScore = 0, tempHighscore = 0;
	private float floatScore = 0, tempFloatScore = 0, tempFloatHighscore;
	private Text scoreText;
	private bool gameover, stopped;

	private void Awake() {
		scoreText = GetComponent<Text> ();
	}

	private void Start() {
		if (clearMemory) {
			PlayerPrefs.DeleteAll ();
		}
		highscore = PlayerPrefs.GetInt ("Highscore");
	}

	private void Update() {
		if (!stopped) {
			floatScore += Time.deltaTime;
			score = Mathf.RoundToInt (floatScore);
			if (score > lastScore) {
				scoreText.text = score.ToString ();
				lastScore = score;
			}
		}
		if (gameover) {
			if (tempScore < score) {
				tempFloatScore += Time.unscaledDeltaTime * score;
				tempScore = Mathf.RoundToInt (tempFloatScore);
				endscoreText.text = tempScore.ToString ();
				endscoreShadow.text = tempScore.ToString ();
			}
			if (tempHighscore < highscore) {
				tempFloatHighscore += Time.unscaledDeltaTime * highscore * 0.75f;
				tempHighscore = Mathf.RoundToInt (tempFloatHighscore);
				highscoreText.text = "Top: " + tempHighscore.ToString ();
				highscoreShadow.text = "Top: " + tempHighscore.ToString ();
			}
		}
	}

	public void OnGameOver() {
		scoreText.enabled = false;
		gameover = true;
		if (highscore < score) {
			highscore = score;
			PlayerPrefs.SetInt ("Highscore", highscore);
			PlayerPrefs.Save ();
		}
	}

	public void SetStopped() {
		stopped = true;
	}

	public void Reset() {
		scoreText.enabled = true;
		score = 0;
		lastScore = 0;
		floatScore = 0;
		tempFloatScore = 0;
		tempScore = 0;
		tempFloatHighscore = 0;
		tempHighscore = 0;
		scoreText.text = "0";
		gameover = false;
		stopped = false;
	}
}
