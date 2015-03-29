using UnityEngine;
using System.Collections;

public class Edge
{
	private Node to;
	private Node from;
	
	public Edge()
	{
		from = new Node();
		to = new Node();
	}
	
	public Edge(Node from, Node to)
	{
	
		this.to = to;
		this.from = from;
	}
	
	public Node GetTo()
	{
		return this.to;
	}
	
	public Node GetFrom()
	{
		return this.from;
	}
	
	public void SetTo(Node t)
	{
		this.to = t;
	}
	
	public void SetFrom(Node f)
	{
		this.from = f;
	}

}
