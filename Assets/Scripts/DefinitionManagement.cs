using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

public class DefinitionManagement
{
	private const string GOOGLE_API_KEY = "";
	private const string GOOGLE_CUSTOM_ENGINE_ID = "";
	private const string GOOGLE_SEARCH_URL = "https://www.googleapis.com/customsearch/v1?cx=" +
		GOOGLE_CUSTOM_ENGINE_ID+"&key="+GOOGLE_API_KEY+"&cref&q=";

	private const string OXFORD_API_KEY = "";
	private const string OXFORD_APP_ID = "";
	private const string OXFORD_SEACRH_URL = "https://od-api.oxforddictionaries.com/api/v1/entries/en/{0}/definitions";

	public string definition { get; set; }

	public IEnumerator FindDefinition(string wordsToSearch){
		yield return FindByGoogleSearchAPI(wordsToSearch);
		if ("".Equals (definition) || definition==null) {
			yield return FindByOxfordDictAPI (wordsToSearch);
		}
	}

	private IEnumerator FindByGoogleSearchAPI(string wordsToSearch){
		string searchURL = GOOGLE_SEARCH_URL + wordsToSearch;
		WWW www = new WWW(searchURL);
		yield return www;
		string result = www.text;
		Regex regex = new Regex("Wikipedia");
		Match match = regex.Match(result);
		if (match.Index != 0) {
			regex = new Regex ("snippet\": \"(.*?(?=\\.))", RegexOptions.Singleline);
			match = regex.Match (result, match.Index);
			definition = match.Groups [1].Value;
		}
	}

	private IEnumerator FindByOxfordDictAPI(string wordsToSearch){
		string words = wordsToSearch.Replace (" ", "_").ToLower();
		string url = String.Format(OXFORD_SEACRH_URL, words);
		var headers = new Dictionary<String, String>();
		headers ["app_id"] = OXFORD_APP_ID;
		headers ["app_key"] = OXFORD_API_KEY;
		headers ["Accept"] = "application/json";
		WWW www = new WWW (url, null, headers);
		yield return(www);
		string result = www.text;
		Match m = Regex.Match(result, "definitions\":.*?(?=\")\"(.*?(?=\"))", RegexOptions.Singleline);
		definition = m.Groups[1].Value;
	}
}