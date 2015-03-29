using UnityEngine;
using System.Collections;

public class Node 
{
	private string dialogue;
	private ArrayList neighbors;
	private string condition;
	

	public Node()
	{
		this.dialogue = "Default Node";
		this.neighbors = new ArrayList();
	}

	public Node(string d)
	{
		this.dialogue = d;
		this.neighbors = new ArrayList();
	}
	
	public void SetCondition(string c)
	{
		this.condition = c;
	}
	
	public string GetCondition()
	{
		return this.condition;
	}

	public string GetDialogue()
	{
		return this.dialogue;
	}

	public ArrayList GetNeighbors()
	{
		return this.neighbors;
	}

	public void SetDialogue(string d)
	{
		this.dialogue = d;
	}

}
