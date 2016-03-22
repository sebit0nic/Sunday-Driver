using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

	public Text coinCounterText;
	public Image coinCounterIcon;
	private int coins;

	private void Start() {
		coins = PlayerPrefs.GetInt ("Coins");
		coinCounterText.text = coins.ToString ();
	}

	public void IncreaseCoinCounter(int value) {
		coins += value;
		PlayerPrefs.SetInt ("Coins", coins);
		PlayerPrefs.Save ();
		coinCounterText.text = coins.ToString ();
	}
}
