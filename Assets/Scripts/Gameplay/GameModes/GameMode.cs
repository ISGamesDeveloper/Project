using System.Collections;
using System.Collections.Generic;
using TanksEngine.LevelCreator;
using UnityEngine;

namespace TanksEngine.Gameplay
{
    public abstract class GameMode : MonoBehaviour
    {
        public GameModeProfile CurrentGameMode;
        protected GameModeSettings gameModeSettings;
        /// <summary>
        /// Вызывает инициализаторы
        /// </summary>
        private void Start()
        {
            InitGameModeSettings();
            SetupGameMode();
        }
        protected virtual void SetupGameMode()
        {
            if (gameModeSettings != null)
            {
                InitPlayer();
                InitCamera();
                InitEnemies();
                InitEvents();
                InitLevel();
            }
            else
            {
                Debug.LogError("Not finded GameMode Settings!");
            }

        }
        #region[EventsSetup]
        private void SubscribeEnvets()
        {
            if (gameModeSettings != null && gameModeSettings.EnableEvents)
            {
                Global_EventManager.eventOnEnemyDie += HandlerOnEnemyDie;
                Global_EventManager.eventOnPlayerDie += HandlerOnPlayerDie;
            }

        }

        private void OnDestroy()
        {
            Global_EventManager.eventOnEnemyDie -= HandlerOnEnemyDie;
            Global_EventManager.eventOnPlayerDie -= HandlerOnPlayerDie;
        }
        #endregion
        #region[Initialisators]
        private void InitGameModeSettings()
        {
            gameModeSettings = CurrentGameMode.Settings;
            if (gameModeSettings != null)
            {
                gameModeSettings.levelManager = GetComponent<GameLevelManager>();
            }

        }
        /// <summary>
        /// Создает игрока
        /// </summary>
        protected virtual void InitPlayer()
        {
            GameResourcesData _data = Resources.Load<GameResourcesData>("GameConfig");
            if (gameModeSettings.Player == null)
            {
                gameModeSettings.Player = GameObject.Instantiate(_data.Player, gameModeSettings.PlayerSpawnPoint, Quaternion.Euler(gameModeSettings.PlayerSpawnPoint));
            }
            Global_EventManager.CallOnPlayerLoaded(gameModeSettings.Player);
        }
        /// <summary>
        /// Создает камеру
        /// </summary>
        protected virtual void InitCamera()
        {
            if (gameModeSettings.EnableCamera)
            {
                GameResourcesData _data = Resources.Load<GameResourcesData>("GameConfig");
                if (gameModeSettings.Camera == null)
                {
                    gameModeSettings.Camera = GameObject.Instantiate(_data.MainCamera);
                    gameModeSettings.Camera.GetComponent<CameraController>().Player = gameModeSettings.Player.transform;
                }
            }
            
        }
        /// <summary>
        /// Подгружает врагов и вызвает функцию их спавна
        /// </summary>
        protected virtual void InitEnemies()
        {
            if (gameModeSettings.SpawnEnemy)
            {
                GameResourcesData _data = Resources.Load<GameResourcesData>("GameConfig");

                gameModeSettings.Enemies = _data.Enemies;
                gameModeSettings.Num_Enemy = gameModeSettings.Enemies.Length;

                BotsSpawner.SpawnBots(_data.Enemies, gameModeSettings.EnemySpawnPoints);
            }
            if (gameModeSettings.AutoDetectEnemies)
            {
                GameObject[] findedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                gameModeSettings.Enemies = findedEnemies;
                Debug.Log("Finded " + findedEnemies.Length + "enemies");
            }
        }
        /// <summary>
        /// Вызывает события, после инициализации игрового режима
        /// </summary>
        protected virtual void InitEvents()
        {
            SubscribeEnvets();
            //Global_EventManager.CallOnCoinsUpdate();
        }
        /// <summary>
        /// Инициализирует игровую локацию
        /// </summary>
        protected virtual void InitLevel()
        {
            if (gameModeSettings.LoadLevelOnStart)
            {
                gameModeSettings.levelManager.LoadLevelRandom();
            }

        }
        #endregion
        #region[EventsHandlers]
        /// <summary>
        /// Обрабатывает смерть врага
        /// </summary>
        /// <param name="enemy"></param>
        protected virtual void HandlerOnEnemyDie(GameObject enemy)
        {
            if (enemy.GetComponent<Bot_Info>())
            {
                gameModeSettings.Coins_reward += enemy.GetComponent<Bot_Info>().reward;
                Global_EventManager.CallOnCoinsUpdate();
            }
            gameModeSettings.Num_Enemy--;
            if (gameModeSettings.Num_Enemy <= 0)
            {
                GameWin(); //Победа - если врагов не осталось
            }
        }
        /// <summary>
        /// Обрабатывает смерть игрока
        /// </summary>
        protected virtual void HandlerOnPlayerDie()
        {
            GameLose(); //Поражение - если игрок умер
        }
        /// <summary>
        /// Обрабатывает событие победы
        /// </summary>
        protected virtual void GameWin()
        {
            Debug.Log("Win!");
            Global_EventManager.CallOnGameWin(); //Вызов события для UI
            gameModeSettings.Player.SetActive(false);

            GiveReward();
        }
        /// <summary>
        /// Выдает награду игроку
        /// </summary>
        protected virtual void GiveReward()
        {

        }
        /// <summary>
        /// Обрабатывает событие поражения
        /// </summary>
        protected virtual void GameLose()
        {
            Debug.Log("Lose!");
            Global_EventManager.CallOnGameLose();//Вызов события для UI
        }
        #endregion
    }

}

