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

    public int LevelCount() => levelCleared.Length;

    public void UpdateLevelCount(int newLevelCount) {
        bool[] b = new bool[newLevelCount];
        for (int i = 0; i < levelCleared.Length; i++) b[i] = levelCleared[i];
        levelCleared = b;

        TimeSpan[] tS = new TimeSpan[newLevelCount];
        for (int i = 0; i < levelClearTime.Length; i++) tS[i] = levelClearTime[i];
        levelClearTime = tS;
    }
}
