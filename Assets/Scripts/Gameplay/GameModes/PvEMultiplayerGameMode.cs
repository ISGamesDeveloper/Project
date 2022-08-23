using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Gameplay
{
    public class PvEMultiplayerGameMode : GameMode
    {
        protected override void SetupGameMode()
        {
            gameModeSettings.EnableCamera = false;
            CurrentGameMode.Settings.EnableEvents = false;
            base.SetupGameMode();
        }
        protected override  void InitPlayer()
        {
            Debug.Log("StartPlayer!!!!");
            GameResourcesData _data = Resources.Load<GameResourcesData>("GameConfig");
            gameModeSettings.Player = Resources.Load<GameObject>("PhotonPlayer");

            //Debug.LogError(gameModeSettings.Player);

            //PhotonNetwork.ConnectUsingSettings();
            //PhotonNetwork.CreateRoom("LoadScene");

            PhotonNetwork.Instantiate(gameModeSettings.Player.name, gameModeSettings.PlayerSpawnPoint, Quaternion.Euler(gameModeSettings.PlayerSpawnPoint));
            //base.InitPlayer();

            //if (gameModeSettings.Player == null)
            //{
            //    gameModeSettings.Player = GameObject.Instantiate(Player, gameModeSettings.PlayerSpawnPoint, Quaternion.Euler(gameModeSettings.PlayerSpawnPoint));
            //}
        }

    }

}
