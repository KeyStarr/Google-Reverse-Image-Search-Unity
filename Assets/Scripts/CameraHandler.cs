using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class CameraHandler
{
	public byte[] imageByteArray { get; set; }

	public IEnumerator TakePhoto()
	{
		string imagePath;
		if (Application.isMobilePlatform) {
			imagePath = Application.persistentDataPath + "/image.png";
			Application.CaptureScreenshot ("/image.png");
		} else {
			imagePath = Application.dataPath + "/StreamingAssets/" + "image.png";
			Application.CaptureScreenshot (imagePath);
		}
		//Time For Picture To Save
		yield return new WaitForSeconds(0.5f);
		imageByteArray = File.ReadAllBytes (imagePath);
	}
}

