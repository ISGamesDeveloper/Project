using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TanksEngine.UI
{
    public class PausePanelUI : MonoBehaviour
    {
        public void ClickButtonContinue()
        {
            gameObject.SetActive(false);
        }
        public void ClickButtonExit()
        {
            gameObject.SetActive(false);

            MainMenu.SkipMenu = true;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(1);
        }
    }

}
