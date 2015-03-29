using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	static bool hasKey1;
	static bool hasKey2;
	static bool hasBothKeys;
	
	// Use this for initialization
	void Start () {
		hasKey1 = false;
		hasKey2 = false;
		hasBothKeys = false;
	}
	
	void Awake(){
		hasKey1 = false;
		hasKey2 = false;
		hasBothKeys = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyUp("escape"))
		{
			Application.Quit ();
		}
		
		//DEBUG: Reset Level
		if(Input.GetKeyUp("="))
		{
			print("# has been pressed");
			Application.LoadLevel("level1");
		}
	}
	
	void OnGUI()
	{
		GUI.Label (new Rect(10,10, 300, 200), "Movement: W A S D\nRun: Left-Shift\nOpen Dialogue: ]\nRestart Game: =");
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == "collider")
		{
			Debug.Log ("haha");
		}
		if(c.gameObject.tag == "key1")
		{
			DestroyObject(c.gameObject);
			hasKey1 = true;
			if(hasKey1)
			{
				Debug.Log ("Key 1 true");
			}
			CheckQuests();
		}
		if(c.gameObject.tag == "key2")
		{
			DestroyObject (c.gameObject);
			hasKey2 = true;
			Debug.Log ("Key 2 true");
			CheckQuests();
		}
	}
	
	public static bool getKey1()
	{
		return hasKey1;
	}
	
	public static bool getKey2()
	{
		return hasKey2;
	}
	
	public static bool getBothKeys()
	{
		return hasBothKeys;
	}
	
	public void CheckQuests()
	{
		if(hasKey2 && hasKey1)
		{
			hasBothKeys = true;
		}
	}
	
}
