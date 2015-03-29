using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node 
{
	private int id;
	
	private int scoreValue = 10;
	private int lifeValue = 1;

	private string dialogue;
	private List<Edge> neighbors;

	public Node()
	{
		this.id = -9999;
		this.dialogue = "Base Node";
		this.neighbors = new List<Edge>();
	}

	public Node(string d)
	{
		this.dialogue = d;
		this.neighbors = new List<Edge>();
	}
	
	public Node(int i)
	{
		this.id = i;
		this.neighbors = new List<Edge>();
	}
	
	public Node(string d, int i)
	{
		this.id = i;
		this.dialogue = d;
		this.neighbors = new List<Edge>();
	}
	
	public int GetID()
	{
		return this.id;
	}
	
	public void SetID(int i)
	{
		this.id = i;
	}
	
	
	public int GetScoreValue()
	{
		return this.scoreValue;
	}

	public int GetLifeValue()
	{
		return this.lifeValue;
	}

	public string GetDialogue()
	{
		return this.dialogue;
	}

	public List<Edge> GetNeighbors()
	{
		return this.neighbors;
	}

	public void SetDialogue(string d)
	{
		this.dialogue = d;
	}

}
