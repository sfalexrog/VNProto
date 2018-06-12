using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGlue : MonoBehaviour
{

	public Text dialogText;

	public Image[] characterImages;

	private GameState gameState;

	[Serializable]
	class Wrapper<T>
	{
		public T[] Items;
	}
	
	// Use this for initialization
	void Start ()
	{
		gameState = Toolbox.RegisterComponent<GameState>();
		
		// FIXME: debug code
		
		DialogueEvent[] events = new DialogueEvent[]
		{
			new PhraseEvent(), 
			new DialogueChoiceEvent(), 
			new DialogueEvent() 
		};
		Wrapper<DialogueEvent> w = new Wrapper<DialogueEvent>();
		w.Items = events;
		Debug.Log(JsonUtility.ToJson(w, true));
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
