using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueNode : Node
{
	private int id;
	private int nextState;
	private string actorImage;
	private string actorName;
	private string dialogue;
	private int state;
	List<DialogueEdge> neighbors;
	private string condition;

	public DialogueNode()
	{
		this.neighbors = new List<DialogueEdge>();
		this.dialogue = "DialogueNode";
		this.actorName = "ActorName";
		this.actorImage = "default";
	}
	
	public DialogueNode(string d)
	{
		this.dialogue = d;
		this.actorName = "ActorName";
		this.actorImage = "default";
		this.neighbors = new List<DialogueEdge>();
	}
	public DialogueNode(string d, string an)
	{
		this.actorName = an;
		this.dialogue = d;
		this.actorImage = "default";
		this.neighbors = new List<DialogueEdge>();
	}
	public DialogueNode(string d, string ai, string an)
	{
		this.actorImage = ai;
		this.actorName = an;
		this.dialogue = d;
		this.neighbors = new List<DialogueEdge>();
	}
	
	public string GetCondition()
	{
		return this.condition;
	}
	
	public void SetCondition(string c)
	{
		this.condition = c;
	}
	
	public int GetNextState()
	{
		return this.nextState;
	}
	
	public void SetNextState(int ns)
	{
		this.nextState = ns;
	}
	
	public int GetState()
	{
		return this.state;
	}
	
	public void SetState(int s)
	{
		this.state = s;
	}
	
	public string GetDialogue()
	{
		return this.dialogue;
	}
	
	public void SetDialogue(string d)
	{
		this.dialogue = d;
	}
	
	public List<DialogueEdge> GetNeighbors()
	{
		return this.neighbors;
	}
	
	public void SetNeighbors(List<DialogueEdge> n)
	{
		this.neighbors = n;
	}

	public string GetActorImage()
	{
		return this.actorImage;
	}

	public void SetActorImage(string ai)
	{
		this.actorImage = ai;
	}

	public string GetActorName()
	{
		return this.actorName;
	}

	public void SetActorName(string an)
	{
		this.actorName = an;
	}
	
	
}
