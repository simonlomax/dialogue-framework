using UnityEngine;
using System.Collections;

public class GameWonScript : MonoBehaviour {

	GUIStyle textStyle;
	GUIStyle textStyle2;
	GUIStyle textStyle3;
	GUIStyle buttonStyle;

	// Use this for initialization
	void Start () {
		
	}
	
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


		GUI.Label(new Rect(10, 10, Screen.width, 100), "Congratulations", textStyle);
		GUI.Label(new Rect(10, 150, Screen.width, 100), "Your Score", textStyle2);


		if(GUI.Button(new Rect(Screen.width / 2 - 180, 500, 400,200),"Play Again",buttonStyle))
		{
			Application.LoadLevel("QuizScene");
		}

		if(GUI.Button (new Rect(Screen.width /2 - 180,710,400,50), "Exit"))
		{
			Application.Quit();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
