using System;

[Serializable]
public class SaveProgress {

    public int levelCurrent = 1;
    public bool[] levelCleared;
    public TimeSpan[] levelClearTime;

    public SaveProgress(int levelCount) {
        levelCleared = new bool[levelCount];
        levelClearTime = new TimeSpan[levelCount];
    }
}
