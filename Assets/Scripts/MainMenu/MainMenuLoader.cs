using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TanksEngine.Data;
using TanksEngine.SaveLoad;
namespace TanksEngine
{
    public class MainMenuLoader : MonoBehaviour
    {
        public Main_Data MainData;
        public bool SkipMainMenu;

        private string saveFilePath;
        private string saveFileName = "/SaveFile.json";

        private void OnEnable()
        {
            Global_EventManager.eventSaveGame += OnGameSave;
        }

        private void OnDestroy()
        {
            Global_EventManager.eventSaveGame -= OnGameSave;
        }

        private void Start()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
            saveFilePath = Application.dataPath + saveFileName;
#endif
            Invoke(nameof(TryLoadGameData), 0.5f);
        }
        public void TryLoadGameData()
        {
            SaveLoadSystem.Load_GameData(saveFilePath, MainData);
            SaveLoadSystem.Save_GameData(saveFilePath);
        }
        private void OnGameSave()
        {
            SaveLoadSystem.Save_GameData(saveFilePath);
        }
    }

}
