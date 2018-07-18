public class ChooseGenderEvent : DialogueEvent
{
    public override DialogueEventType Type
    {
        get { return DialogueEventType.TYPE_GENDER_CHOICE; }
    }

    public int nextEventId;
}
