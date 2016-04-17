using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

	public Text coinCounterText, coinCounterShopText;
	public int coins;
	public bool overrideCoins;
	public Animator shopButton1, shopButton2;

	private void Start() {
		if (overrideCoins) {
			PlayerPrefs.SetInt ("Coins", 500);
			PlayerPrefs.Save ();
		}
		coins = PlayerPrefs.GetInt ("Coins");
		coinCounterText.text = coins.ToString ();
		coinCounterShopText.text = coins.ToString ();
		if (coins > 100) {
			shopButton1.SetTrigger ("OnFlash");
		} else {
			shopButton1.SetTrigger ("OnIdle");
		}
	}

	public void IncreaseCoinCounter(int value) {
		coins += value;
		PlayerPrefs.SetInt ("Coins", coins);
		PlayerPrefs.Save ();
		coinCounterText.text = coins.ToString ();
		coinCounterShopText.text = coins.ToString ();

	}

	public void Refresh() {
		coins = PlayerPrefs.GetInt ("Coins");
		coinCounterText.text = coins.ToString ();
		coinCounterShopText.text = coins.ToString ();
	}

	public void OnGameOver() {
		if (coins > 100) {
			shopButton2.SetTrigger ("OnFlash");
		} else {
			shopButton2.SetTrigger ("OnIdle");
		}
	}
}
