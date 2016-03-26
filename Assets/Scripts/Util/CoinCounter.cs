using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

	public Text coinCounterText, coinCounterShopText;
	public int coins;
	public bool overrideCoins;

	private void Start() {
		if (overrideCoins) {
			PlayerPrefs.SetInt ("Coins", 500);
			PlayerPrefs.Save ();
		}
		coins = PlayerPrefs.GetInt ("Coins");
		coinCounterText.text = coins.ToString ();
		coinCounterShopText.text = coins.ToString ();
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
}
