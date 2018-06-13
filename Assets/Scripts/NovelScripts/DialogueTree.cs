using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTree : MonoBehaviour
{
    private GameState _gameState;
    
    private Dictionary<int, DialogueEvent> _events;
    private Dictionary<string, DialogueActor> _actors;

    public UiGlue glue;

    private int currentId = 1;
    
    void Awake()
    {
        if (_events == null)
        {
            _events = new Dictionary<int, DialogueEvent>();
        }
        _gameState = Toolbox.RegisterComponent<GameState>();
        int sceneId = _gameState.currentScene;
        String sceneFilename = "Assets/Scenarios/Text/Scenario_" + sceneId + ".json";
        Debug.Log("Loading scene data from " + sceneFilename);
        
        StreamReader input = new StreamReader(sceneFilename);

        var eventsList = JsonConvert.DeserializeObject<List<DialogueEvent>>(input.ReadToEnd(), new EventConverter());

        foreach (var ev in eventsList)
        {
            Debug.Log("Registering event id " + ev.Id);
            if (_events.ContainsKey(ev.Id))
            {
                Debug.LogError("Event id " + ev.Id + " already present!");
            }
            _events[ev.Id] = ev;
        }
        
        _actors = new Dictionary<string, DialogueActor>();
        
        // Load actor data
        var actorStream = new StreamReader("Assets/Scenarios/Resources/Actors/manifest.json");
        var actors = JsonConvert.DeserializeObject<List<DialogueActor>>(actorStream.ReadToEnd());
        foreach (var actor in actors)
        {
            _actors[actor.name] = actor;
        }
    }

    void Start()
    {
        SetUiForEvent(getEventById(currentId));
    }

    public DialogueEvent getEventById(int id)
    {
        if (!_events.ContainsKey(id))
        {
            Debug.LogError("Something asked for impossible key: " + id);
            throw new Exception("Key not found: " + id);
        }

        return _events[id];
    }

    public void SetUiForEvent(DialogueEvent ev)
    {
        if (ev.GetType() == typeof(PhraseEvent))
        {
            glue.ShowPhraseUi((PhraseEvent)ev);
        }
        else if (ev.GetType() == typeof(DialogueChoiceEvent))
        {
            glue.ShowChoiceUi((DialogueChoiceEvent)ev);
        }
        else if (ev.GetType() == typeof(FinalDialogueEvent))
        {
            glue.ShowEndingUi((FinalDialogueEvent)ev);
        }
        else
        {
            Debug.LogError("Unhandled event type " + ev.GetType());
        }
    }

    public void OnChoice(int choice)
    {
        var currentEvent = (DialogueChoiceEvent) getEventById(currentId);
        currentId = currentEvent.choices[choice].resultId;
        SetUiForEvent(getEventById(currentId));
    }

    public void OnForward()
    {
        var currentEvent = (PhraseEvent) getEventById(currentId);
        currentId = currentEvent.nextEventId;
        SetUiForEvent(getEventById(currentId));
    }

    public void OnDefender()
    {
        var currentEvent = (DialogueChoiceEvent) getEventById(currentId);
        glue.ShowChoiceUi(currentEvent, true);
    }

    /**
     * Get a list of images that will be used for actors
     */
    public string[] GetUsedActorImages()
    {
        var actors = new HashSet<string>();
        // Look through all events to see which actors are used
        foreach (var ev in _events.Values)
        {
            if (ev.GetType() == typeof(PhraseEvent))
            {
                var pev = (PhraseEvent) ev;
                actors.Add(pev.speakerName);
            }
        }

        var actorSpriteList = new List<string>();
        foreach (var actor in actors)
        {
            if (_actors.ContainsKey(actor))
            {
                foreach (var emotion in _actors[actor].emotions.Values)
                {
                    actorSpriteList.Add(emotion);
                }    
            }
            else
            {
                Debug.LogWarning("Unable to find actor " + actor + " in manifest");
            }
        }
        return actorSpriteList.ToArray();
    }
    
    /**
     * Get the appropriate image for the on-screen actor
     */
    public string GetCurrentActorImage()
    {
        var currentEvent = (PhraseEvent) getEventById(currentId);
        var currentActorName = currentEvent.speakerName;
        // TODO: store emotions as event data
        var actorEmotion = "default";
        DialogueActor currentActor;
        if (_actors.TryGetValue(currentActorName, out currentActor))
        {
            string emotionValue;
            if (currentActor.emotions.TryGetValue(actorEmotion, out emotionValue))
            {
                return emotionValue;
            }

            return currentActor.emotions["default"];
        }

        return null;
    }
}
