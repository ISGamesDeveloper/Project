using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace TanksEngine.Character
{
    public class OnlinePlayerController : CharacterEntityController
    {
        private PhotonView view;

        protected override void SetupLocalPlayer()
        {
            //if is Photon mine -> localPlayer = true /else -> false
            if (view == null)
            {
                view = GetComponent<PhotonView>();
            }

            if (view.IsMine)
            {
                characterEntity.IsLocalPlayer = true;
            }
            else
            {
                characterEntity.IsLocalPlayer = false;
            }
        }
        protected override void Start()
        {
            if (view == null)
            {
                view = GetComponent<PhotonView>();
            }
            
            if (view.IsMine)
            {

                GameResourcesData _data = Resources.Load<GameResourcesData>("GameConfig");
                var Camera = GameObject.Instantiate(_data.MainCamera);
                Camera.GetComponent<CameraController>().Player = transform;
                Debug.Log("Photon player connected");
            }
        }

        protected override void FixedUpdate()
        {
            if (view.IsMine)
            {
                base.FixedUpdate();
            }
        }
    }
}
    