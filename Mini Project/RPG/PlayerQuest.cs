namespace RPG;

public class PlayerQuest
{
    public Quest TheQuest;
    public bool IsCompleted;
    
    public PlayerQuest(Quest quest, bool isCompleted)
    {
        TheQuest = quest;
        IsCompleted = isCompleted;
    }
}