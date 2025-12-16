using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatSystem : NetworkBehaviour
{
    [SerializeField]
    GameObject chatCanvas;

    [SerializeField]
    TMPro.TMP_InputField inputField;

    [SerializeField]
    TextMeshProUGUI chatText;

    public static Action<string> onSendMessage;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        chatCanvas.SetActive(true);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        onSendMessage += OnMessageReceive;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrWhiteSpace(inputField.text))
        {
            var pc = GetComponent<PlayerController>();
            string senderName = pc.playerName;

            CmdSendMessage(senderName, inputField.text);
            inputField.text = "";
        }
    }

    [Command]
    private void CmdSendMessage(string name, string message)
    {
        RpcSendMessage($"\n {name}: {message}");
    }

    [ClientRpc]
    private void RpcSendMessage(string message)
    {
        onSendMessage?.Invoke(message);
    }

    private void OnMessageReceive(string message)
    {
        chatText.text += message;
    }
}
