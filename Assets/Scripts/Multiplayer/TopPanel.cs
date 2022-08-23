using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour
{
    private readonly string connectionStatusMassage = "    Connection Status: ";

    [Header("UI References")]
    public Text ConnectionStatusText;

    public void Update()
    {
        ConnectionStatusText.text = connectionStatusMassage + PhotonNetwork.NetworkClientState;
    }
}
