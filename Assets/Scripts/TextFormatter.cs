using System;

public class TextFormatter
{
	public static string FormatName(string name){
		if (name.Contains(" ")){
			name = name.Replace(" ","\n");
		}
		return name;
	}

	public static string FormatDefinition(string definition){
		//remove new line characters from text3
		if (definition.Contains("\\n")){
			definition = definition.Replace(@"\n"," ");
		}
		//insert new line after every fourth here
		int spaceCounter = 0;
		int symbCounter = 0;
		for (int i = 0; i < definition.Length; i++) {
			symbCounter++;
			if (definition[i] == ' '){
				spaceCounter++;
				if (spaceCounter == 4 || symbCounter>21) {
					definition = definition.Insert (i, "\n");
					symbCounter = 0;
					spaceCounter = 0;
				}
			}
		}
		return definition;
	}
}

