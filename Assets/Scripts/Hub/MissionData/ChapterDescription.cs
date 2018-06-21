public class ChapterDescription
{
    // Chapter identifier. First chapter must have an Id of 1, an Id of -1 is reserved.
    public int Id;
    // Chapter name. Will be displayed for the user.
    public string ChapterName;
    // Resource name of the scenario. Must *NOT* have a .json extension! (see actor manifests,
    // the same rule applies to them)
    public string ScenarioFilename;
    // Amount of experience required to start this chapter
    public int MinXpRequired;
    // An ID of the next chapter (should be -1 if this chapter is the last one)
    public int NextChapterId;
}
