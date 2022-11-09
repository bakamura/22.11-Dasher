using UnityEngine;

// Could be a struct?
public class SaveData {

    public bool musicEnabled;
    public bool sfxEnabled;
    public Language language = Language.english;

    public uint arcadeHighScore;

    public bool[][] levelCompletion;
    public double[][] firstClearTime; // For data collection
    public double[][] fastestClearTime;

}

// Is it needed
public enum Language {
    english,
    portuguese,
    spanish,
    french,
    italian,
    german,
    japanese,
    chinese
}
