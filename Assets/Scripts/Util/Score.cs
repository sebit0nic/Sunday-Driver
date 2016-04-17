using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

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
	public bool slippedOnce = false;

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

			Analytics.CustomEvent ("GameOver", new Dictionary<string, object> {
				{ "Score", score },
				{ "Highscore", highscore },
				{ "Coins", PlayerPrefs.GetInt ("Coins") },
				{ "Selected Car", PlayerPrefs.GetInt ("SelectedCar") },
				{ "Controlscheme", PlayerPrefs.GetInt ("Controls") }
			});
				
			if (score >= 50) {
				Social.ReportProgress("CgkInvGGzfYUEAIQAg", 100.0f, (bool success) => {
				});
			}
			if (score >= 200) {
				Social.ReportProgress("CgkInvGGzfYUEAIQAw", 100.0f, (bool success) => {
				});
			}
			if (score >= 500) {
				Social.ReportProgress("CgkInvGGzfYUEAIQBA", 100.0f, (bool success) => {
				});
			}
			if (score >= 100 && !slippedOnce) {
				Social.ReportProgress("CgkInvGGzfYUEAIQBg", 100.0f, (bool success) => {
				});
			}

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

	public void SetSlippedOnce() {
		slippedOnce = true;
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
		slippedOnce = false;
	}

	public void ShowGooglePlayHighscores() {
		PlayGamesPlatform.Activate ();
		Social.localUser.Authenticate((bool success) => {
			if (success) {
				Social.ReportScore(highscore, "CgkInvGGzfYUEAIQAQ", (bool success2) => {
					if (success2) {
						Social.ShowLeaderboardUI();
					}
				});
			}
		});
	}
}
