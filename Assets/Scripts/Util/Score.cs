using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public bool clearMemory;

	public Text endscoreText, endscoreShadow, highscoreText, highscoreShadow;
	private int score = 0, highscore = 0, lastScore = 0;
	private int tempScore = 0, tempHighscore = 0;
	private float floatScore = 0, tempFloatScore = 0, tempFloatHighscore;
	public Text scoreText;
	private bool gameover, stopped;
	public Animator crownAnimator;
	private bool animationPlayedOnce, newHighscore, rewardedOnce;
	private CoinRewarder coinRewarder;
	public GameObject highscoreLine;

	private void Start() {
		coinRewarder = GameObject.Find ("Coin Rewarder").GetComponent<CoinRewarder> ();
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
			} else {
				endscoreText.text = score.ToString ();
				endscoreShadow.text = score.ToString ();
				if (!animationPlayedOnce && newHighscore) {
					crownAnimator.SetTrigger ("OnPop");
					animationPlayedOnce = true;
				}
			}
			if (tempHighscore < highscore) {
				tempFloatHighscore += Time.unscaledDeltaTime * highscore * 0.75f;
				tempHighscore = Mathf.RoundToInt (tempFloatHighscore);
				highscoreText.text = "Top: " + tempHighscore.ToString ();
				highscoreShadow.text = "Top: " + tempHighscore.ToString ();
			} else {
				highscoreText.text = "Top: " + highscore.ToString ();
				highscoreShadow.text = "Top: " + highscore.ToString ();
			}
		}
		if (score == highscore - 3 && highscore != 0) {
			highscoreLine.SetActive (true);
		}
	}

	public void OnGameOver() {
		gameover = true;
		if (!rewardedOnce) {
			coinRewarder.CheckBanner (highscore < score);
			rewardedOnce = true;
		}

		if (highscore < score) {
			highscore = score;
			PlayerPrefs.SetInt ("Highscore", highscore);
			PlayerPrefs.Save ();
			newHighscore = true;
		}
	}

	public void SetStopped() {
		stopped = true;
	}

	public void Reset() {
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
		animationPlayedOnce = false;
		newHighscore = false;
		rewardedOnce = false;
		highscoreLine.transform.position = new Vector3 (0, 0, -90);
		highscoreLine.SetActive (false);
	}
}
