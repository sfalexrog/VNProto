using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActor
{
	public string name;
	public Dictionary<string, string> emotions;

	public DialogueActor()
	{
		emotions = new Dictionary<string, string>();
	}
	
}
