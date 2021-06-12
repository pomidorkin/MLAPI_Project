using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using System;

public class PerformActionOnPlayer : NetworkBehaviour
{
    private NetworkObject focus;
    ChoosePayerOnScene choosePayerOnScene;
    // Start is called before the first frame update
    void Start()
    {
        choosePayerOnScene = GetComponent<ChoosePayerOnScene>();
        // Listening to when the player click on a NetworkObject
        choosePayerOnScene.FocusChanged += OnFocusChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }

        // When we have somebody in focus and when an H ker is pressed 
        // we try to heal the player on the server side (ServerRpc)
        if (!Input.GetKeyDown(KeyCode.H) || !focus) { return; }
        HealChosenPlayerServerRpc();
    }

    [ServerRpc]
    private void HealChosenPlayerServerRpc()
    {
        Debug.Log("Trying to heal player " + focus.GetInstanceID());
    }

    public void OnFocusChanged(object source, FocusEventArgs e)
    {
        focus = e.NetworkObject;
        Debug.Log(focus.transform.name);
    }
}
