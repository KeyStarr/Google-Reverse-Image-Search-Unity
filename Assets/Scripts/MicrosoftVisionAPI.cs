using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class MicrosoftVisionAPI
{
	private const string MICROSOFT_VISION_KEY = "";
	private const string MICROSOFT_VISION_URL = "https://westus.api.cognitive.microsoft.com/vision/v1.0/describe?maxCandidates=1";

	public string wordsToSearch {get; set;}

	public IEnumerator RecognizeImage(byte[] imageByteArray){
		WWWForm form = new WWWForm ();
		form.AddBinaryData ("file", imageByteArray);
		var headers = form.headers;
		headers["Ocp-Apim-Subscription-Key"] = MICROSOFT_VISION_KEY;
		WWW www = new WWW (MICROSOFT_VISION_URL, form.data, headers);
		yield return www;

		string response = www.text;
		Match m = Regex.Match(response, "tags\":\\[\"(.*?(?=\"))");
		wordsToSearch = m.Groups[1].Value;
	}
}

