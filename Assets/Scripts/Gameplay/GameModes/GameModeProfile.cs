using System.Collections;
using System.Collections.Generic;
using TanksEngine.LevelCreator;
using UnityEngine;

namespace TanksEngine.Gameplay 
{
    [CreateAssetMenu(fileName = "New GameModeProfile", menuName = "GameData/GameModeProfile")]
    public class GameModeProfile : ScriptableObject
    {
       public GameModeSettings Settings;
    }
    [System.Serializable]
    public class GameModeSettings 
    {
        #region[Fields]
        [Header("Settings")]
        public bool EnableEvents;
        public bool EnableCamera;
        [Header("Player")]
        [HideInInspector]
        public GameObject Player;
        [HideInInspector]
        public GameObject Camera;
        public int Coins_reward;
        public Vector3 PlayerSpawnPoint;
        [Header("Level")]
        public bool LoadLevelOnStart;
        [HideInInspector]
        public GameLevelManager levelManager;
        [Header("Enemies")]
        public bool SpawnEnemy;
        public bool AutoDetectEnemies;
        public Vector3[] EnemySpawnPoints;
        [SerializeField] public GameObject[] Enemies;
        [SerializeField] public int Num_Enemy;
        #endregion
    }
}

