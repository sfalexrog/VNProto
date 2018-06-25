public class CardGameDescription
{
    // Card game identifier. First card scenario must have an Id of 1, an Id of -1 is reserved.
    public int Id;
    // Card game name. Not used for now.
    public string CardGameName;
    // Resource name of the scenario. Must *NOT* have a .json extension! (see actor manifests,
    // the same rule applies to them)
    public string CardScriptFilename;
    // Amount of experience required to start this chapter
    public int MinXpRequired;
    // An ID of the next chapter (should be -1 if this chapter is the last one)
    public int NextCardGameId;
}
