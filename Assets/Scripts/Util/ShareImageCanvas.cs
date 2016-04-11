using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ShareImageCanvas : MonoBehaviour {
	
	public string ScreenshotName = "screenshot.png";

	public void ShareScreenshotWithText() {
		string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
		Application.CaptureScreenshot(ScreenshotName);
		Share("My highscore on #sundaydriver is "+PlayerPrefs.GetInt("Highscore")+"!",screenShotPath,"");
	}

	public void Share(string shareText, string imagePath, string url, string subject = "") {
		#if UNITY_ANDROID
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + imagePath);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		intentObject.Call<AndroidJavaObject>("setType", "image/png");

		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText);

		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, subject);
		currentActivity.Call("startActivity", jChooser);
		#elif UNITY_IOS
		CallSocialShareAdvanced(shareText, subject, url, imagePath);
		#else
		Debug.Log("No sharing set up for this platform.");
		#endif
	}
}
