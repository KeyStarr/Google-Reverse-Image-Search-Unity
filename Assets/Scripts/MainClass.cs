using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class MainClass : MonoBehaviour 
{
	public GameObject scanButtonObject;
	private GameObject scanStripObject;
	private GameObject titleTextObject;
	private GameObject definitionTextObject;
	private GameObject modeDropdown;

	private bool useGoogleReverseSearch = true;

	void Start(){
		findObjects ();
		SetStartUIState ();
		SetModeDropdownListener ();
	}

	private void findObjects(){
		scanButtonObject = GameObject.Find ("ScanButton");
		scanStripObject = GameObject.Find ("ScanStrip");
		titleTextObject = GameObject.Find ("TitleText");
		definitionTextObject = GameObject.Find ("DefinitionText");
		modeDropdown = GameObject.Find ("ModeDropdown");
	}

	private void SetStartUIState(){
		scanStripObject.SetActive (false);
		titleTextObject.SetActive (false);
		titleTextObject.transform.parent.gameObject.SetActive (false);
		definitionTextObject.SetActive (false);
		definitionTextObject.transform.parent.gameObject.SetActive (false);
	}

	private void SetModeDropdownListener(){
		Dropdown drop = modeDropdown.GetComponent<Dropdown> ();
		drop.onValueChanged.AddListener (delegate {
			OnModeSwitched(drop);
		});
	}

	private void OnModeSwitched(Dropdown mode){
		switch (mode.value) {
		case 0:
			useGoogleReverseSearch = true;
			break;
		case 1:
			useGoogleReverseSearch = false;
			break;
		}
	}

	public void ScanPressed(){
		StartCoroutine ("MainLogic");
	}

	private IEnumerator MainLogic(){
		SetScreenTakeUIState ();
		CameraHandler cameraHandler = new CameraHandler ();
		yield return cameraHandler.TakePhoto ();
		SetInProgressUIState ();
		string wordsToSearch;
		if (useGoogleReverseSearch) {
			CloudinaryAPI cloudinary = new CloudinaryAPI ();
			yield return cloudinary.UploadImage (cameraHandler.imageByteArray);
			GoogleReverseImageSearch reverseSearch = new GoogleReverseImageSearch ();
			yield return reverseSearch.RecognizeImage (cloudinary.imgURL);
			wordsToSearch = reverseSearch.wordsToSearch;
			yield return cloudinary.DeleteImage ();
		} else {
			MicrosoftVisionAPI microsoftAPI = new MicrosoftVisionAPI ();
			yield return microsoftAPI.RecognizeImage (cameraHandler.imageByteArray);
			wordsToSearch = microsoftAPI.wordsToSearch;
		}
		DefinitionManagement def = new DefinitionManagement ();
		yield return def.FindDefinition(wordsToSearch);
		CreateVisibleText (wordsToSearch, def.definition);
	}

	private void SetScreenTakeUIState(){
		titleTextObject.SetActive (false);
		definitionTextObject.SetActive (false);
		modeDropdown.SetActive (false);
		titleTextObject.transform.parent.gameObject.SetActive (false);
		definitionTextObject.transform.parent.gameObject.SetActive (false);
	}

	private void SetInProgressUIState(){
		scanButtonObject.SetActive (false);
		scanStripObject.SetActive (true);
	}

	private void CreateVisibleText(string name, string definition){
		SetResultUIState ();
		if (name == null || name == "") {
			name = "Oops";
			definition = "No call. \nTry again with a different angle!";
		} else if (definition == null || definition == "") {
			definition = "I don't know what is this...";
		} else {
			name = TextFormatter.FormatName (name);
			definition = TextFormatter.FormatDefinition (definition);
		}
		titleTextObject.GetComponent<Text> ().text = name;
		definitionTextObject.GetComponent<Text> ().text = definition;
	}
		
	private void SetResultUIState(){
		scanStripObject.SetActive (false);
		scanButtonObject.SetActive (true);
		titleTextObject.SetActive (true);
		definitionTextObject.SetActive (true);
		titleTextObject.transform.parent.gameObject.SetActive (true);
		definitionTextObject.transform.parent.gameObject.SetActive (true);
		modeDropdown.SetActive (true);
	}
}