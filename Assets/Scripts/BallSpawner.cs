using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class BallSpawner : NetworkBehaviour
{
    [SerializeField] NetworkObject ballPrefab;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }
        if (!Input.GetMouseButtonDown(0)) { return; }

        SpawnBallServerRpc(transform.position);
    }

    [ServerRpc]
    private void SpawnBallServerRpc(Vector3 spawnPos)
    {
        // Spawning ball on the server side
        NetworkObject ballInstnce = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

        // We to spawn the ball and give the ownership to the player who spawned the ball
        ballInstnce.SpawnWithOwnership(OwnerClientId);
        ballInstnce.GetComponent<BallController>().Die();
    }

}
