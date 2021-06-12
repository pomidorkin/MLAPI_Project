using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using System;

// Нам нужно передать игрока, которого мы выбирает в фокус сабскрайберам, для этого создаем
// свой класс передаваемых аргументов
public class FocusEventArgs : EventArgs
{
    public NetworkObject NetworkObject { get; set; }
}

public class ChoosePayerOnScene : NetworkBehaviour
{
    private NetworkObject focus;
    private Camera mainCamera;

    // Declaring an EventHandler
    public event EventHandler<FocusEventArgs> FocusChanged;

    // Start is called before the first frame update
    void Start()
    {
        // We need camera to determine where we click on the scene
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!IsOwner) { return; }
        if (!Input.GetMouseButtonDown(0)) { return; }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) { return; }

        if (hit.transform.GetComponent<NetworkObject>() != null)
        {
            OnFocusChanged(hit);
        }

    }

    private void OnFocusChanged(RaycastHit hit)
    {
        // Assigning our new focus and firing an event telling subscribers
        // that we clicked on a NetworkObject
        focus = hit.transform.GetComponent<NetworkObject>();
        FocusChanged(this, new FocusEventArgs() { NetworkObject = focus});
    }

}
