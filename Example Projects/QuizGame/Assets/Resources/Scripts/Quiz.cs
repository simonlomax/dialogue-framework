using UnityEngine;
using System.Collections;

public class Quiz : MonoBehaviour {
	public TextAsset xml;
	//Graph to hold the Quiz
	Graph quizGraph;
	//XmlParser parseXML;
	//Nodes to hold the questions
	
	//Pointer to the current node
	Node currentNode;
	
	GUIStyle questionStyle;
	GUIStyle answerStyle;

	void Awake()
	{
		quizGraph = new Graph();
	}
	
	// Use this for initialization
	void Start () 
	{
		questionStyle = new GUIStyle();
		
		questionStyle.fontSize = 40;
		questionStyle.wordWrap = true;
		questionStyle.alignment = TextAnchor.MiddleCenter;
		questionStyle.normal.textColor = Color.white;
		
//		parseXML = new XmlParser (xml);
		//Initialise the Graph
//		quizGraph = parseXML.GetGraph();
		

		
		//Start at question 1
		currentNode = quizGraph.GetNodeList()[0];
		
		quizGraph.DisplayGraph();
	}
	
	void OnGUI()
	{
		answerStyle = new GUIStyle("button");
		answerStyle.fontSize = 25;
		
		int i = 0;
		GUI.Label (new Rect(10,100, Screen.width - 10, 200), currentNode.GetDialogue(),questionStyle);
		int x = 20;
		
		foreach(Edge edge in currentNode.GetNeighbors())
		{
			int y = Screen.height / 2;
			
			if(GUI.Button(new Rect(x,y,330,120), edge.GetTo().GetDialogue(),answerStyle))
			{
				if(edge.GetTo().GetNeighbors().Count <= 0)
				{
					Debug.Log ("Wrong");
				}
				else if(edge.GetTo().GetNeighbors().Count >= 1)
				{
					Debug.Log ("Right");
					ArrayList lol = edge.GetTo().GetNeighbors();
					Edge lolz = (Edge)lol[0];
					
		

					currentNode = (Node)lolz.GetTo();
				}
			}
			x+= 340;
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
