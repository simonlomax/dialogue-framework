using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour {

	public GameConditionManager m;
	
	//Size of the Dialogue Box
	public int boxWidth;
	public int boxHeight;
	
	public Texture boxTexture;
	
	//skin for the dialogue
	public GUISkin skin;
	
	//Position of the Dialogue Box
	public float xPos;
	public float yPos;
	
	//XML file
	public TextAsset xml;
	//Graph to hold dialogue
	private DialogueGraph dialogueGraph;
	//Parser to convert XML to Graph
	private XmlParser Parser;

	//the current node
	DialogueNode currentNode;
	
	//is the player talking
	bool isTalking;
	bool playerColliding;
	
	public int NPCstate;
	
	//Reset to defaults on awake (for reloading levels)
	void Awake()
	{
		dialogueGraph = null;
		NPCstate = 1;
		isTalking = false;
		playerColliding = false;
	}
	
	// Use this for initialization
	void Start () 
	{
		isTalking = false;
		playerColliding = false;
		NPCstate = 1;
		Parser = new XmlParser(xml);
		dialogueGraph = (DialogueGraph)Parser.GetGraph();
		currentNode = GetStartNode(NPCstate);
		m =  new GameConditionManager();
	}
	
	public DialogueNode GetStartNode(int state)
	{
		Debug.Log ("got to getstartnode");
		DialogueNode startNode = CheckState(state);
		return startNode;
	}
	
	public DialogueNode CheckState(int state)
	{
		Debug.Log ("CheckState: Checking: "+ state.ToString ());
		int count = 0;
		DialogueNode rootNode = null;
		Debug.Log (dialogueGraph.GetNodeList().Count.ToString ());
		foreach(DialogueNode node in dialogueGraph.GetNodeList())
		{
			count++;
			Debug.Log ("IF: "+ node.GetState().ToString ()+" = " + state.ToString ());
			if (state == node.GetState ())
			{
				rootNode = node;
				Debug.Log (rootNode.GetState().ToString ());
				break;
			}
			
		}
		
		Debug.Log ("Went through "+count.ToString ()+ " to get to root");
		
		if(rootNode == null)
		{
			//defaults to first node in list
			Debug.Log ("defaulting");
			rootNode = dialogueGraph.GetNodeList()[0];
		}
		
		return rootNode;
	}

	void OnGUI()
	{	
		GUI.skin = skin;
		
		if(isTalking)
		{
			//convoStart()
			int i = 0;
			if(!boxTexture)
			{
				Debug.LogError ("Dialogue Texture missing");
				return;
			}
			GUI.DrawTexture (new Rect(xPos - 40 , yPos - 35 , boxWidth + 70 , boxHeight), boxTexture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Label (new Rect(xPos,yPos, boxWidth, boxHeight), currentNode.GetDialogue ());
			GUI.Label (new Rect(xPos,yPos - 25, Screen.width - 10, 200), currentNode.GetActorName ()+ ":");
			
			
			int x = 370;
			
			foreach(DialogueEdge edge in currentNode.GetNeighbors())
			{
				int y = (int)yPos + (boxHeight + 5);
				
				if(edge.GetTo().GetCondition() != null)
				{
					if(!m.CheckCondition(edge.GetTo().GetCondition()))
					{
						continue;
					}
				}
			
				if(GUI.Button(new Rect(x,y,330,120), edge.GetTo().GetDialogue()))
				{
					//if there are replies
					if(edge.GetTo().GetNeighbors().Count >= 1)
					{
						//broken down
						/*
						List<DialogueEdge> lol = edge.GetTo().GetNeighbors();
						DialogueEdge lolz = (DialogueEdge)lol[0];
						currentNode = (DialogueNode)lolz.GetTo();
						*/
						
						//simplified
						currentNode = edge.GetTo().GetNeighbors()[0].GetTo();
					}
					else
					{
						//Convo is over
						Debug.Log ("Conversation Over");
						isTalking = false;
						NPCstate = edge.GetTo().GetNextState();
						currentNode = GetStartNode(NPCstate);
						//convoEnd()
					}
				}
				
				x+= 340;
				i++;
			}
		}
	}
	
	void Update()
	{
		if(playerColliding)
		{
			if(Input.GetKeyUp("]"))
			{
				isTalking = true;
				print("] has been pressed");
			}
		}
	
	}

	
	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == "player")
		{
			Debug.Log ("haha");
			playerColliding = true;
			Debug.Log ("COLLIDING WITH PLAYER!");
			if(isTalking == false)
			{
				isTalking = true;
			}
		}
	}
	
	void OnTriggerExit(Collider c)
	{
		if(c.gameObject.tag == "player")
		{
			playerColliding = false;
			isTalking = false;
		}
	}
	
}