using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerReadyState : MonoBehaviour
{
    GameNetworkRoomPlayer _roomPlayer;

    [SerializeField]
    TextMeshProUGUI nameText;

    [SerializeField]
    TextMeshProUGUI readyText;
    public GameNetworkRoomPlayer RoomPlayer => _roomPlayer;

    private void Start()
    {
        SetReady(false);
    }

    public void SetRoomPlayer(GameNetworkRoomPlayer roomPlayer)
    {
        _roomPlayer = roomPlayer;
        _roomPlayer.OnChangeReady += SetReady;
        _roomPlayer.OnChangeName += SetName;
        _roomPlayer.OnDisconnected += DestroyState;

        SetName(_roomPlayer.playerOnlineName);
        SetReady(_roomPlayer.readyToBegin);
    }

    public void DestroyState()
    {
        Destroy(this);
    }

    public void SetName(string playerName)
    {
        nameText.text = playerName;
    }

    public void SetReady(bool isReady)
    {
        readyText.text = isReady ? "Ready" : "Not Ready";
        readyText.color = isReady ? Color.green : Color.red;
    }
}
