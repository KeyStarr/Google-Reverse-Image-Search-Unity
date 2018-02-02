using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class GoogleReverseImageSearch 
{
	private const string BASE_URL = "http://www.google.com/searchbyimage?hl=ru&image_url=";

	public string wordsToSearch {get; set;}

	public IEnumerator RecognizeImage(string imageURL){
		string url = BASE_URL+imageURL;
		WWWForm form = new WWWForm ();
		var headers = form.headers;
		headers ["User-Agent"] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
		WWW www = new WWW (url, null, headers);
		yield return www;
		string response = www.text;
		Match m = Regex.Match (response, "style=\"font-style:italic\">(.*?(?=<))");
		wordsToSearch = m.Groups [1].Value;
	}
}