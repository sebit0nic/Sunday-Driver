using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class CoinRewarder : MonoBehaviour {

	public int rewardedCoins;
	public GameObject banner;
	public Text bannerText, coinText;
	public GameObject freeButton, shareButton, adButton;
	public long nextFreeTimestamp, nextAdTimestamp;
	public Animator coins, coinsIcon;
	public CoinCounter coinCounter;
	private AudioSource rewardSound;

	private void Start() {
		string temp = PlayerPrefs.GetString ("NextFreeTimestamp");
		if (temp.Length == 0) {
			nextFreeTimestamp = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond + 120000;
			PlayerPrefs.SetString ("NextFreeTimestamp", nextFreeTimestamp.ToString ());
		} else {
			nextFreeTimestamp = long.Parse (temp);
		}

		temp = PlayerPrefs.GetString ("NextAdTimestamp");
		if (temp.Length == 0) {
			nextAdTimestamp = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond + 240000;
			PlayerPrefs.SetString ("NextAdTimestamp", nextAdTimestamp.ToString ());
		} else {
			nextAdTimestamp = long.Parse (temp);
		}
		rewardSound = GameObject.Find ("Main Camera").GetComponent<AudioSource> ();

		PlayerPrefs.Save ();
	}

	public void CheckBanner(bool newHighscore) {
		if (newHighscore) {
			banner.SetActive (true);
			bannerText.text = "Share Highscore!";
			shareButton.SetActive (true);
		} else if (nextFreeTimestamp < System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond) {
			banner.SetActive (true);
			bannerText.text = "Free Coins!";
			freeButton.SetActive (true);
			rewardedCoins = Random.Range (5, 11) * 10;
			coinText.text = "+" + rewardedCoins;
		} else if (nextAdTimestamp < System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond) {
			banner.SetActive (true);
			bannerText.text = "Watch Ad. Get Coins!";
			adButton.SetActive (true);
			rewardedCoins = Random.Range (7, 11) * 10;
			coinText.text = "+" + rewardedCoins;
		}
	}

	public void RewardCoins() {
		PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") + rewardedCoins);
		PlayerPrefs.Save ();
	}

	public void SetFreeTimestamp() {
		nextFreeTimestamp = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond + 1200000;
		PlayerPrefs.SetString ("NextFreeTimestamp", nextFreeTimestamp.ToString());
		PlayerPrefs.Save ();
	}

	public void SetAdTimestamp() {
		nextAdTimestamp = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond + 600000;
		PlayerPrefs.SetString ("NextAdTimestamp", nextAdTimestamp.ToString());
		PlayerPrefs.Save ();
	}

	public void OnSuccessfulTransaction(int mode) {
		coins.SetTrigger ("OnPop");
		coinsIcon.SetTrigger ("OnPop");
		bannerText.text = "";
		if (mode == 0) {
			freeButton.SetActive (false);
			SetFreeTimestamp ();
		} else {
			adButton.SetActive (false);
			SetAdTimestamp ();
		}
		RewardCoins ();
		rewardSound.Play ();
		coinCounter.Refresh ();
	}

	public void Reset() {
		banner.SetActive (false);
		bannerText.gameObject.SetActive (true);
		rewardedCoins = 0;
		freeButton.SetActive (false);
		shareButton.SetActive (false);
		adButton.SetActive (false);
		bannerText.text = "";
	}

	public void ShowRewardedAd() {
		if (Advertisement.IsReady("rewardedVideo")) {
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	private void HandleShowResult(ShowResult result) {
		switch (result) {
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			OnSuccessfulTransaction (1);
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}
