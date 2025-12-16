using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PickupCollectible : NetworkBehaviour
{
    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        NetworkServer.Destroy(gameObject);
    }
}
