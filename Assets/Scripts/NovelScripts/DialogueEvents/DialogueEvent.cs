using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public enum DialogueEventType
{
    TYPE_PHRASE = 0,
    TYPE_CHOICE = 1,
    TYPE_END = 999
}

public abstract class JsonCreationConverter<T> : JsonConverter
{
    protected abstract T Create(Type objectType, JObject jObject);

    public override bool CanWrite
    {
        get { return base.CanWrite; }
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(T).IsAssignableFrom(objectType);
    }
    
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);

        T target = Create(objectType, jObject);
        
        serializer.Populate(jObject.CreateReader(), target);

        return target;
    }
}

public class EventConverter : JsonCreationConverter<DialogueEvent>
{
    protected override DialogueEvent Create(Type objectType, JObject jObject)
    {
        var type = jObject["Type"];
        if (type != null)
        {
            switch (type.Value<int>())
            {
                case (int)DialogueEventType.TYPE_PHRASE:
                    return new PhraseEvent();
                case (int)DialogueEventType.TYPE_CHOICE:
                    return new DialogueChoiceEvent();
                case (int)DialogueEventType.TYPE_END:
                    return new FinalDialogueEvent();
                default:
                    throw new JsonException("Bad dialogue event type");
            }
        }
        throw new JsonException("No type info in JSON");
    }
}

public abstract class DialogueEvent
{
    public abstract DialogueEventType Type { get; }
    public int Id;
}
