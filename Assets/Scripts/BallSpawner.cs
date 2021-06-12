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

        // Starting coroutine wich destroys the oject after fixed time
            coroutine = DestroyObjectAfterTime(ballInstnce, 1f);
            StartCoroutine(coroutine);
    }

    private IEnumerator DestroyObjectAfterTime(NetworkObject ballObject, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // We call DestroyObjectAfterTime method on the server side so it doesn't need to be a server Rpc
        // It is already on the server-side
        ballObject.GetComponent<BallController>().Die();
    }

}
