using UnityEngine;

// Could be a struct?
public class SaveData {

    public bool musicEnabled;
    public bool sfxEnabled;
    public Language language;

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
