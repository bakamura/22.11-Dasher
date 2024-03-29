using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : Singleton<SaveSystem> {

    [Header("Parameters")]

    [HideInInspector] public SaveProgress progress;
    [HideInInspector] public SaveSettings settings;
    public enum SaveType {
        Progress,
        Settings
    }
    public enum AudioType {
        Music,
        Sfx
    }

    [Header("Cache")]

    private static string progressPath;
    private static string settingsPath;
    private BinaryFormatter formatter = new BinaryFormatter();
    private FileStream streamCurrent;

    protected override void Awake() {
        base.Awake();

        progressPath = Application.persistentDataPath + "/progress.data";
        settingsPath = Application.persistentDataPath + "/settings.data";
        SaveLoad();

        // Checks for updates
        int levelAmount = SceneManager.sceneCountInBuildSettings - 1;
        if (progress.LevelCount() < levelAmount) {
            progress.UpdateLevelCount(levelAmount);
            SaveUpdate(SaveType.Progress);
            Debug.Log("Level Count Updated!"); //
        }
    }

    private void SaveLoad() {
        if (File.Exists(progressPath)) {
            streamCurrent = File.Open(progressPath, FileMode.Open);
            progress = (SaveProgress)formatter.Deserialize(streamCurrent);
            streamCurrent.Close();
        }
        else {
            progress = new SaveProgress(SceneManager.sceneCountInBuildSettings - 1);
            SaveUpdate(SaveType.Progress);
        }

        if (File.Exists(settingsPath)) {
            streamCurrent = File.Open(settingsPath, FileMode.Open);
            settings = (SaveSettings)formatter.Deserialize(streamCurrent);
            streamCurrent.Close();
        }
        else {
            settings = new SaveSettings();
            SaveUpdate(SaveType.Settings);
        }
    }

    public void SaveUpdate(SaveType type) {
        if (File.Exists(type == SaveType.Progress ? progressPath : settingsPath)) {
            streamCurrent = File.Open(type == SaveType.Progress ? progressPath : settingsPath, FileMode.Open);
            formatter.Serialize(streamCurrent, type == SaveType.Progress ? (object)progress : (object)settings);
            streamCurrent.Close();
        }
        else {
            streamCurrent = File.Create(type == SaveType.Progress ? progressPath : settingsPath);
            formatter.Serialize(streamCurrent, type == SaveType.Progress ? (object)progress : (object)settings);
            streamCurrent.Close();
        }
    }

    public void SaveErase() {
        if (File.Exists(progressPath)) File.Delete(progressPath);

        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        //progress = new SaveProgress(SceneManager.sceneCountInBuildSettings - 1);
        //SaveUpdate(SaveType.Progress);
    }

    public void CompleteLevel(TimeSpan time) {
        progress.levelCleared[progress.levelCurrent - 1] = true;
        if (progress.levelClearTime[progress.levelCurrent - 1] == TimeSpan.Zero || time < progress.levelClearTime[progress.levelCurrent - 1])
            progress.levelClearTime[progress.levelCurrent - 1] = time;

        if (progress.levelCurrent < SceneManager.sceneCountInBuildSettings - 1) {
            progress.levelCurrent++;
            SaveUpdate(SaveType.Progress);
            progress.levelCurrent--;
        }

    }

    public void ToggleAudio(AudioType type, bool isActive) {
        if (type == AudioType.Music) settings.musicOn = isActive;
        else settings.sfxOn = isActive;

        SaveUpdate(SaveType.Settings);
    }

    public void ToggleHaptic(bool isActive) {
        settings.hapticOn = isActive;

        SaveUpdate(SaveType.Settings);
    }

}
