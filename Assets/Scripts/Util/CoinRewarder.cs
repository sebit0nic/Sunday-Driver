using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinRewarder : MonoBehaviour {

	public int rewardedCoins;
	public GameObject banner;
	public Text bannerText, coinText;
	public GameObject freeButton, shareButton, adButton;
	public long nextFreeTimestamp, nextAdTimestamp;

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
			rewardedCoins = Random.Range (3, 9) * 10;
			coinText.text = "+" + rewardedCoins;
		} else if (nextAdTimestamp < System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond) {
			banner.SetActive (true);
			bannerText.text = "Watch Ad. Get Coins!";
			adButton.SetActive (true);
			rewardedCoins = Random.Range (5, 9) * 10;
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

	public void Reset() {
		banner.SetActive (false);
		bannerText.gameObject.SetActive (true);
		rewardedCoins = 0;
		freeButton.SetActive (false);
		shareButton.SetActive (false);
		adButton.SetActive (false);
		bannerText.text = "";
	}
}
