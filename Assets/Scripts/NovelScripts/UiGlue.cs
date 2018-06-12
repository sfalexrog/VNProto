using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Newtonsoft.Json;

public class UiGlue : MonoBehaviour
{

	public Text dialogText;

	public Image[] characterImages;

	private GameState gameState;

	// Use this for initialization
	void Start ()
	{
		gameState = Toolbox.RegisterComponent<GameState>();
		
		// FIXME: debug code
		
		DialogueEvent[] events = new DialogueEvent[]
		{
			new PhraseEvent(), 
			new DialogueChoiceEvent(), 
			new FinalDialogueEvent() 
		};

		String serialized = JsonConvert.SerializeObject(events, Formatting.Indented);
		Debug.Log(serialized); //JsonConvert.SerializeObject(events, Formatting.Indented /*, new JsonConverter[]{new EventConverter()}*/));
		var deserializedEvents = JsonConvert.DeserializeObject<List<DialogueEvent>>(serialized, new EventConverter());
		foreach (var ev in deserializedEvents)
		{
			Debug.Log("event has type " + ev.GetType());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void returnToHub()
	{
		gameState.currentScene += 1;
		SceneManager.LoadScene(0);
	}
	
}
