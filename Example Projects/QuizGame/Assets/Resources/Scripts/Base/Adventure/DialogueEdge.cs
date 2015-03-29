using UnityEngine;
using System.Collections;

public class DialogueEdge : Edge
{
	private DialogueNode to;
	private DialogueNode from;
	
	public DialogueEdge()
	{
		from = new DialogueNode();
		to = new DialogueNode();
	}
	
	public DialogueEdge(DialogueNode from, DialogueNode to)
	{
		this.to = to;
		this.from = from;
	}
	
	public DialogueNode GetTo()
	{
		return this.to;
	}
	
	public DialogueNode GetFrom()
	{
		return this.from;
	}
	
	public void SetTo(DialogueNode t)
	{
		this.to = t;
	}
	
	public void SetFrom(DialogueNode f)
	{
		this.from = f;
	}
	
}
