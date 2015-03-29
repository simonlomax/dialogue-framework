using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueGraph : Graph
{
	List<DialogueNode> nodeList;
		
	public DialogueGraph()
	{
		nodeList = new List<DialogueNode>();
	}
	
	public DialogueGraph(List<DialogueNode> n)
	{
		if(nodeList == null)
		{
			this.nodeList =  new List<DialogueNode>();
			nodeList = n;
		}
		else
		{
			this.nodeList = n;
		}
	}
	
	public DialogueNode FindNode(int id)
	{
		DialogueNode found = null;
		foreach(DialogueNode n in GetNodeList())
		{
			if (n.GetID().Equals (id))
			{
				found = n;
				break;
			}
		}
		return found;
	}
	
	public void AddNode(DialogueNode n)
	{
		nodeList.Add (n);
	}
	
	public void AddEdge(DialogueNode from, DialogueNode to)
	{
		from.GetNeighbors().Add(new DialogueEdge(from, to));
	}
	
	
	
	public void RemoveNode(DialogueNode n)
	{
		foreach(DialogueNode node in GetNodeList())
		{
			if(n == node)
			{
				GetNodeList().Remove(n);
			}
		}
	}
	
	public List<DialogueNode> GetNodeList()
	{
		return this.nodeList;
	}
	
	
}
