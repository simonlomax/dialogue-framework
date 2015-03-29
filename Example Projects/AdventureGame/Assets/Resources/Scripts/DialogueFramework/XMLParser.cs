using UnityEngine;
using System.Collections;
using System.Xml;
using System.Diagnostics;

public class XmlParser 
{

	//TESTING
	Stopwatch s = new Stopwatch();
	//
	
	//Graph to hold parsed info
	Graph thisGraph;
	//Resource file for xml
	TextAsset xmlFile;
	//Xml Document object
	XmlDocument xml;

	//Constructor
	public XmlParser(TextAsset x)
	{
		xmlFile = x;
		xml = new XmlDocument();
	}

	//Gets the graph type by checking the root node of the xml file
	public Graph GetGraph()
	{		
		//check if xml file exists
		if(xmlFile == null)
		{
			UnityEngine.Debug.Log("FILE NOT FOUND");
			thisGraph = null;
		}
		else
		{
			UnityEngine.Debug.Log ("FILE LOADED");
			//load in the xml from the resrouce file
			xml.LoadXml (xmlFile.text);
			//gets the root element
			XmlElement root = xml.DocumentElement;

			//if root is called quiz (Quiz App)
			if (root.Name.ToUpper().Equals("QUIZ")) 
			{
				//instantiate the graph using the QuizGraph method
				thisGraph = QuizGraph();
			}
			//if root is called conversation (Adventure Game)
			else if (root.Name.ToUpper().Equals ("CONVERSATION")) 
			{
				//instantiate the graph using the ConvoGraph method
				thisGraph = ConvoGraph();
			}
			//if the root is called story (Interactive Story)
			else if (root.Name.ToUpper ().Equals ("Story"))
			{
				//instantiate the graph using the StoryGraph method
				thisGraph = StoryGraph();
			}
			//otherwise
			else
			{
				//set to null
				thisGraph = null;
			}
		}
		//return the graph
		return thisGraph;
	}

	public Graph QuizGraph()
	{
		//create local quizGraph
		Graph quizGraph = new Graph();
		//Select the question nodes
		XmlNodeList questions = xml.DocumentElement.SelectNodes("/quiz/question");

		/*
		 * The XML file needs to be iterated through twice separately. 
		 * The first time it adds all the questions to the graph
		 * The second time it adds the answers and edges between questions.
		 * This is so that there aren't any null pointers to deal with
		 * because the answers would be trying to connect on to a question
		 * that didn't exist in the graph yet. 
		 */

		//for each question in the xml file (First iteration)
		foreach (XmlNode questionNode in questions)
		{
			//create a new question node
			Node node = new Node();
			//set the dialogue of the question node to the text attribute of the xml node
			node.SetDialogue(questionNode.Attributes["text"].Value);
			//add the node to the graph
			quizGraph.AddNode(node);
		}

		//For all questions (Second Iteration)
		foreach(XmlNode questionNode in questions)
		{
			//for all child nodes (answers) in those questions
			foreach (XmlNode childNode in questionNode.ChildNodes)
			{
				//if the name of the child is answers
				if(childNode.Name.ToUpper().Equals("ANSWERS"))
				{
					//for every answer
					foreach (XmlNode answerNode in childNode.ChildNodes)
					{
						//create a node and set the dialogue to the answer nodes inner text
						Node ansNode = new Node(answerNode.InnerText);
						//if the action attribute of the answer equals true (Correct Answer)
						if(answerNode.Attributes["action"].Value.ToUpper().Equals("TRUE"))
						{
							//Get the ID of the next question in the graph (Current + 1)
							int id = int.Parse (questionNode.Attributes["id"].Value) + 1;
							//Create an edge between the answer and the next question
							quizGraph.AddEdge(ansNode,quizGraph.GetNodeList()[id]);
						}
						//Add the node to the graph
						quizGraph.AddNode (ansNode);
						//add the edge to the graph
						quizGraph.AddEdge (quizGraph.GetNodeList()[int.Parse(questionNode.Attributes["id"].Value)],ansNode);                                                  
					}
				}
			}
		}
		//Return the finished graph
		return quizGraph;
	}

	public DialogueGraph ConvoGraph()
	{
		s.Start ();
		DialogueGraph convoGraph = new DialogueGraph();
		XmlNodeList dialogues = xml.DocumentElement.SelectNodes ("/conversation/dialogue");
		
		/*
		 * Loops through all the dialogue nodes first without
		 * processing any replies, this is because to be able
		 * to link the player questions to corresponding replies
		 * the dialogues need to be accessible in the graph before
		 * they can be linked to. 
		 */
		foreach (XmlNode dialogueNode in dialogues)
		{
			DialogueNode dNode = new DialogueNode();
			if(dialogueNode.Attributes["actorname"].Value != null)
			{
				dNode.SetActorName(dialogueNode.Attributes["actorname"].Value);
			}
			if(dialogueNode.Attributes["actordialogue"].Value !=null)
			{
				dNode.SetDialogue(dialogueNode.Attributes["actordialogue"].Value);
			}
			if(dialogueNode.Attributes["id"].Value != null)
			{
				dNode.SetID (int.Parse (dialogueNode.Attributes["id"].Value));
			}
			if(dialogueNode.Attributes["state"].Value != null)
			{
				dNode.SetState(int.Parse(dialogueNode.Attributes["state"].Value));
			}
			
			convoGraph.AddNode(dNode);
		}
		
		/*
		 * Loops back through the dialogue nodes only 
		 * looking at the child node replies
		 */
		foreach(XmlNode dialogueNode in dialogues)
		{
			foreach (XmlNode childNode in dialogueNode.ChildNodes)
			{
				if(childNode.Name.ToUpper().Equals("REPLIES"))
				{
					foreach (XmlNode replyNode in childNode.ChildNodes)
					{
						DialogueNode repNode = new DialogueNode();
						if(replyNode.Attributes["text"].Value != null)
						{
							repNode.SetDialogue(replyNode.Attributes["text"].Value);
						}
						if(replyNode.Attributes["condition"].Value !=null)
						{
							repNode.SetCondition(replyNode.Attributes["condition"].Value);
						}
						if(replyNode.Attributes["goto"].Value != null)
						{
							if(replyNode.Attributes["goto"].Value.Equals ("-9999"))
							{
								//Ends the Conversation
								UnityEngine.Debug.Log("CONVO OVER");
								if(replyNode.Attributes["nextstate"].Value !=null)
								{
									repNode.SetNextState(int.Parse (replyNode.Attributes["nextstate"].Value));
								}
							}
							else
							{
								int id = int.Parse(replyNode.Attributes["goto"].Value);
								convoGraph.AddEdge(repNode, convoGraph.FindNode(id));
							}
						}
						
						convoGraph.AddNode (repNode);
						convoGraph.AddEdge (convoGraph.FindNode(int.Parse(dialogueNode.Attributes["id"].Value)),repNode);
						UnityEngine.Debug.Log(dialogueNode.Attributes["id"].Value + " to " + repNode.GetDialogue());                                          
					}
				}
			}
		}
		s.Stop();
		UnityEngine.Debug.Log("Time Elapsed for " + convoGraph.GetNodeList()[0].GetActorName() + ": " + s.Elapsed + "\nTotal Nodes: " + convoGraph.GetNodeList().Count.ToString());
		return convoGraph;
	}

	public Graph StoryGraph()
	{
		return thisGraph;
	}

}