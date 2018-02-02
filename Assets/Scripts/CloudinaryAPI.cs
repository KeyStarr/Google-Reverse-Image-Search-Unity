using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class CloudinaryAPI
{
	private const string CLOUD_NAME = "";
	private const string CLOUDINARY_API_KEY = "";
	private const string CLOUDINARY_API_SECRET = "";
	private const string CLOUDINARY_IMG_URL = "https://api.cloudinary.com/v1_1/" + CLOUD_NAME + "/auto/upload/";
	private const string CLOUDINARY_DELETE_URL = "https://api.cloudinary.com/v1_1/"+CLOUD_NAME+"/delete_by_token";
	private const string UPLOAD_PRESET_NAME = "";
	private const string IMAGE_PUBLIC_ID = "";
	private const string REGEX_CLOUDINARY_TEMPLATE = "\":\"(.*?(?=\"))";

	public string imgURL { get; set; }
	private string delete_token;

	public IEnumerator UploadImage(byte[] imgByte){
		WWWForm requestForm = new WWWForm ();
		requestForm.AddBinaryData("file", imgByte);
		requestForm.AddField ("upload_preset", UPLOAD_PRESET_NAME);
		requestForm.AddField ("public_id", IMAGE_PUBLIC_ID);

		WWW www = new WWW (CLOUDINARY_IMG_URL,requestForm);
		yield return(www);

		Match m = Regex.Match(www.text, "url" + REGEX_CLOUDINARY_TEMPLATE);
		imgURL = m.Groups[1].Value;
		m = Regex.Match (www.text, "delete_token" + REGEX_CLOUDINARY_TEMPLATE);
		delete_token = m.Groups [1].Value;
	}

	public IEnumerator DeleteImage(){
		WWWForm requestForm = new WWWForm ();
		requestForm.AddField ("token", delete_token);
		WWW www = new WWW (CLOUDINARY_DELETE_URL, requestForm);
		yield return (www);
	}
}