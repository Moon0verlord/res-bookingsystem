using System.Collections.Generic;

namespace RPG;

public class QuestList
{
    public List<PlayerQuest> QuestLog;

    public QuestList()
    {
        QuestLog = new List<PlayerQuest>();
    }
    
    public void AddQuest(PlayerQuest quest)
    {
        QuestLog.Add(new PlayerQuest(quest.TheQuest, false));
    }
}