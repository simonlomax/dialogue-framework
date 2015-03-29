using UnityEngine;
using System.Collections;
using System.Xml;

public class DownloadQuizes : MonoBehaviour {
	
	//Screen Resolution
	int width = Screen.width;
	int height = Screen.height;
	//Scale for Resolution
	float yScale;
	
	//GUISkin
	public GUISkin skin;
	
	//Strings to hold URLs leading to server
	private string url, url2;
	//WWW objects to hold retrieved information from server
	private WWW list, dlevel;
	//Array to hold all the quizes
	private string[] levels;
	//control booleans
	bool isVisible;
	bool quizTime;
	bool gameWon;
	//game variables
	int score;
	int questionNum;
	
	
	//-------------------QUIZ VARIABLES------------------------//
	
	Graph quizGraph;
	XmlParser parseXML;
	Node currentNode;
	GUIStyle questionStyle;
	GUIStyle answerStyle;
	
	//----------------------------------------------------------//

	// Use this for initialization
	void Start () 
	{
		url = "http://192.168.1.100/QuizAppWebsite/quizList.php";
		url2 = "http://192.168.1.100/QuizAppWebsite/quizes/";
		list = new WWW(url);
		StartCoroutine (quizList());
		isVisible = true;
		quizTime = false;
		gameWon = false;
		score = 0;
		questionNum = 0;
		yScale = width / 533f;
	}
	
	public IEnumerator quizList()
	{
		yield return list;
		string data = list.text;
		levels = data.Split(',');
	}
	
	public IEnumerator downloadLevel(string level)
	{
		string url = url2 + level;
		
		Debug.Log (url);
		dlevel = new WWW(url);
		yield return dlevel;
		if(dlevel.error !=null)
		{
			Debug.Log ("There was an error downloading " + level);
		}
		else
		{
			Debug.Log (level + " downloaded successfully.");
			isVisible = false;
			Debug.Log (dlevel.text);
			createQuiz(dlevel);
		}
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		GUI.matrix *= Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3(yScale, yScale, 1f));
		int y1 = 105;
		if(isVisible)
		{
			if(GUI.Button (new Rect(0,0,(Screen.width / yScale) / 6 , (Screen.width / yScale) / 6), "↺"))
			{
				Application.LoadLevel("QuizScene");
			}
			foreach (string level in levels)
			{
				if(level == "." || level == "..")
				{
					continue;
				}
				else
				{
					if(GUI.Button(new Rect(0,y1,Screen.width / yScale ,75), level))
					{
						StartCoroutine(downloadLevel(level));
					}
				}
				y1 += 76;
			}
		}
		
		if(quizTime)
		{
			int i = 0;
			if(GUI.Button (new Rect(0,0,(Screen.width / yScale) / 6 , (Screen.width / yScale) / 6), "Menu"))
			{
				Application.LoadLevel("QuizScene");
			}
			
			GUI.Label (new Rect(0,150, Screen.width / yScale, 200), currentNode.GetDialogue());
			int x = 0;
			int y = 360;
			foreach(Edge edge in currentNode.GetNeighbors())
			{
				if(GUI.Button(new Rect(x,y,Screen.width / yScale,120), edge.GetTo().GetDialogue()))
				{
					questionNum+= 1;
					if(edge.GetTo ().GetCondition() == "true")
					{
						Debug.Log ("Correct");
						score+=1;
					}
					else if(edge.GetTo().GetCondition() == "false")
					{
						Debug.Log ("Wrong");
					}
					
					if(edge.GetTo().GetCondition() == "end")
					{
						Debug.Log ("Correct");
						score+=1;
						quizTime = false;
						gameWon = true;
					}
					else if(edge.GetTo().GetCondition() == "endfalse")
					{
						Debug.Log ("Wrong");
						quizTime = false;
						gameWon = true;
					}
					else
					{
						ArrayList list = edge.GetTo ().GetNeighbors ();
						Edge next = (Edge)list[0];
						currentNode = (Node)next.GetTo();
					}
					/*else
					{
						if(edge.GetTo().GetNeighbors().Count <= 0)
						{
							Debug.Log ("Wrong");
							currentNode.DecrementLives();
							currentNode.DecrementScore();
							if(currentNode.IsDead())
							{
								Application.LoadLevel("GameOver");
							}
						}
						else if(edge.GetTo().GetNeighbors().Count >= 1)
						{
							Debug.Log ("Right");
							Debug.Log (edge.GetTo().GetCondition ());
							ArrayList lol = edge.GetTo().GetNeighbors();
							Edge lolz = (Edge)lol[0];
						
							currentNode.IncrementScore();
						
								currentNode = (Node)lolz.GetTo();
						
						//if last question
						}
					}*/
				}
				y+= 130;
				i++;
			}
		}
		if(gameWon)
		{
			GUI.Label (new Rect(0,200, Screen.width / yScale, 250), "You got "+score+ "/"+questionNum+" correct!");
			
			if(GUI.Button (new Rect(0,(Screen.height / 2) / yScale, (Screen.width / yScale) / 2 , 250 ), "Menu"))
			{
				Application.LoadLevel("QuizScene");
			}
			if(GUI.Button (new Rect((Screen.width / yScale) / 2 ,(Screen.height / 2) / yScale,  (Screen.width / yScale) / 2 , 250 ), "Retry"))
			{
				//Reset Level
				score = 0;
				questionNum = 0;
				currentNode = quizGraph.GetNodeList()[0];
				gameWon = false;
				quizTime = true;
			}
			
			
		}
	}
	
	void createQuiz(WWW l)
	{
		XmlDocument quiz = new XmlDocument();
		quiz.LoadXml(l.text);
		//----------------QUIZ-----------------//
		
		parseXML = new XmlParser (quiz);
		quizGraph = parseXML.GetGraph();
		//Start at question 1
		currentNode = quizGraph.GetNodeList()[0];
		quizGraph.DisplayGraph();
		quizTime = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
