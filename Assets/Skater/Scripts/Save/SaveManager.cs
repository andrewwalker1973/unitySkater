using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get { return instance; } }
    private static SaveManager instance;

    //Fields 
    public SaveState save;
    private const string saveFileName = "data.ss";  
    private BinaryFormatter formatter;

    // Actions
    public Action<SaveState> OnLoad;
    public Action<SaveState> OnSave;

    private void Awake()
    {
        instance = this;
        formatter = new BinaryFormatter();
        // Try and load previous sve state
        LoadGame();

    }


    public void LoadGame()
    {
        try
        {
           FileStream file = new FileStream(Application.persistentDataPath + saveFileName, FileMode.Open, FileAccess.Read); // "Application.persistentDataPath + saveFileName" needed for Android
            save = (SaveState)formatter.Deserialize(file); // deserilize data
            file.Close();
            OnLoad?.Invoke(save);
        }
        catch
        {
            Debug.Log("Save file not found, create a new One");
            SaveGame();
        }
    }


    public void SaveGame()
    {
        // if there is no previosu satte found, create a new one
        if (save == null)
            save = new SaveState();

        // Set tht time we try to save at
        save.LastSaveTime = DateTime.Now;

        // open a file on system and write to it 
        FileStream file = new FileStream(Application.persistentDataPath + saveFileName, FileMode.OpenOrCreate , FileAccess.Write); // "Application.persistentDataPath + saveFileName" needed for Android
        formatter.Serialize(file, save);
        file.Close();

        OnSave?.Invoke(save);

    }

}
