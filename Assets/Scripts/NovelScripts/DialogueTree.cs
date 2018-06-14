using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueTree : MonoBehaviour
{
    private GameState _gameState;
    
    private Dictionary<int, DialogueEvent> _events;
    private Dictionary<string, DialogueActor> _actors;
    private Dictionary<string, string> _backgrounds;

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
        String sceneFilename = "Scenarios/Text/Scenario_" + sceneId;
        Debug.Log("Loading scene data from " + sceneFilename);

        var sceneJson = Resources.Load<TextAsset>(sceneFilename);

        var eventsList = JsonConvert.DeserializeObject<List<DialogueEvent>>(sceneJson.text, new EventConverter());

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
        var actorJson = Resources.Load<TextAsset>("Actors/manifest");
        var actors = JsonConvert.DeserializeObject<List<DialogueActor>>(actorJson.text);
        foreach (var actor in actors)
        {
            _actors[actor.name] = actor;
        }
        
        _backgrounds = new Dictionary<string, string>();
        // Load background data
        var backgroundsJson = Resources.Load<TextAsset>("Backgrounds/manifest");
        var backgrounds = JsonConvert.DeserializeObject<List<DialogueBackground>>(backgroundsJson.text);
        foreach (var background in backgrounds)
        {
            _backgrounds[background.name] = background.image;
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
        else if (ev.GetType() == typeof(BackgroundChangeEvent))
        {
            var bev = (BackgroundChangeEvent) getEventById(currentId);
            currentId = bev.nextEventId;
            glue.ChangeBackground(bev);
            SetUiForEvent(getEventById(currentId));
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
        if (_gameState.currentPower >= currentEvent.defenderCost)
        {
            _gameState.currentPower -= currentEvent.defenderCost;
            glue.ShowChoiceUi(currentEvent, true);
        }
        else
        {
            Debug.LogError("This button should have been hidden! Oh well, never trust what you didn't write yourself.");
        }
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
                // Some phrases may have no speakers,
                // they obviously don't have an actor image
                if (pev.speakerName != null)
                {
                    actors.Add(pev.speakerName);    
                }
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
     * Get a list of images that will be used for backgrounds
     */
    public string[] GetUsedBackgroundImages()
    {
        var backgrounds = new HashSet<string>();
        // Look through all events to see which actors are used
        foreach (var ev in _events.Values)
        {
            if (ev.GetType() == typeof(BackgroundChangeEvent))
            {
                var bev = (BackgroundChangeEvent) ev;
                backgrounds.Add(bev.backgroundName);
            }
        }

        var backgroundSpriteList = new List<string>();
        foreach (var bg in backgrounds)
        {
            string backImg;
            if (_backgrounds.TryGetValue(bg, out backImg))
            {
                backgroundSpriteList.Add(backImg);
            }
            else
            {
                Debug.LogWarning("Unable to find background " + bg + " in manifest");
            }
        }

        return backgroundSpriteList.ToArray();
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
        if (currentActorName != null && _actors.TryGetValue(currentActorName, out currentActor))
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
    
    /**
     * Get the background image by its name
     */
    public string GetBackgroundImageByName(string backgroundName)
    {
        return _backgrounds[backgroundName];
    }

    /**
     * Return to the main hub, conditionally incrementing scene counter
     */
    public void FinalizeChapter()
    {
        var ev = getEventById(currentId);
        if (ev.GetType() == typeof(FinalDialogueEvent))
        {
            var fev = (FinalDialogueEvent) ev;
            if (fev.isChapterCompleted)
            {
                _gameState.currentScene += 1;
            }
        }
        
        SceneManager.LoadScene(0);
    }
    
}
