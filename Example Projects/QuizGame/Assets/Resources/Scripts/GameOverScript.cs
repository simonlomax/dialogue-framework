using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	GUIStyle textStyle;
	GUIStyle textStyle2;
	GUIStyle textStyle3;
	GUIStyle buttonStyle;

	void OnGUI()
	{
		textStyle = new GUIStyle();
		textStyle.fontSize = 100;
		textStyle.normal.textColor = Color.white;
		textStyle.alignment = TextAnchor.MiddleCenter;
		
		textStyle2 = new GUIStyle();
		textStyle2.fontSize = 70;
		textStyle2.normal.textColor = Color.white;
		textStyle2.alignment = TextAnchor.MiddleCenter;
		
		textStyle3 = new GUIStyle();
		textStyle3.fontSize = 200;
		textStyle3.normal.textColor = Color.green;
		textStyle3.alignment = TextAnchor.MiddleCenter;
		
		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 70;


		GUI.Label(new Rect(10, 100, Screen.width, 100), "GAME OVER",textStyle);
		GUI.Label(new Rect(10, 170, Screen.width, 100), "You ran out of Lives!", textStyle2);
		if(GUI.Button(new Rect(Screen.width / 2 - 400 / 2, 300, 400,200),"Restart", buttonStyle))
		{
			Application.LoadLevel("QuizScene");
		}

		if(GUI.Button (new Rect(Screen.width /2 - 200,510,400,50), "Exit"))
		{
			Application.Quit();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
