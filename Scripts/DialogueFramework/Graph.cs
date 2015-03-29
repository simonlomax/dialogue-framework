using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph
{
	//Attributes
	List<Node> nodeList;
	
	//constructor
	public Graph()
	{
		this.nodeList = new List<Node>();
	}
	
	public Graph(List<Node> a)
	{
		if(nodeList == null)
		{
			this.nodeList =  new List<Node>();
			nodeList = a;
		}
		else
		{
			this.nodeList = a;
		}
	}
	
	//methods
	public void AddNode(Node n)
	{
		nodeList.Add(n);
	}
	
	public void AddEdge(Node from, Node to)
	{
		from.GetNeighbors().Add(new Edge(from, to));
	}
	
	public void RemoveNode(Node n)
	{
		foreach(Node node in GetNodeList())
		{
			if(n == node)
			{
				GetNodeList().Remove(n);
			}
		}
	}
	
	public Node FindNode(int id)
	{
		Node found = null;
		foreach(Node n in GetNodeList())
		{
			if (n.GetID().Equals (id))
			{
				found = n;
				break;
			}
		}
		return found;
	}
	
	public List<Node> GetNodeList()
	{
		return this.nodeList;
	}
	
	public void DisplayGraph()
	{
		string edges = "";
		//DISPLAYING GRAPH
		foreach (Node n in GetNodeList())
		{
			edges = "";
			
			foreach(Edge edge in n.GetNeighbors())
			{
				edges += "\nEdge: " + edge.GetFrom().GetID().ToString() + " to " + edge.GetTo().GetDialogue();
				//Debug.Log("NAME: " + n.GetDialogue() + "\nFrom " + edge.GetFrom().GetDialogue() + " to " + edge.GetTo().GetDialogue() + "\nCost: " + edge.GetCost());
			}
			edges +="\n\n";
			Debug.Log ("Name: " + n.GetDialogue() + edges);
		}
	}
}
