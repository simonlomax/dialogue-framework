using UnityEngine;
using System.Collections;

public class GameConditionManager : MonoBehaviour
{
	
	public bool CheckCondition(string condition)
	{
		//if("true" == condition)	return true;
		//if("playerCarryingKey" == condition)	return player.IsCarryingKey();
		//if("timerRunOut" == condition)	return (timer.SecondsRemaining() < 0);
		
		if(condition.Equals ("done")) return Player.getBothKeys();
		if(condition.Equals ("-9999")) return true;
		if(condition == "key1")
		{
			return Player.getKey1();
		}
		if(condition.Equals ("key2")) return Player.getKey2();
		return false;				
	}
	
	
}

/*
===========================================

if( edge.ConditionIsTrue() )
{
	AddToOPlayerRelyList(edge);
}

bool ConditionIsTrue()
{
	string conditionString = edge.GetConditionString();
	return GameConditionManager.CheckCondition( conditionString )
}

==========================================

	<conversation>
		<dialogue id = "0" state ="1" actorname = "Guard" actordialogue = "Hello, Stranger. What can I do for you?">
		<replies>
		<reply text = "I must see His Majesty. My village requires aid immediately!" goto = "1" condition="true"/>
		</replies>
		</dialogue>
		
*/