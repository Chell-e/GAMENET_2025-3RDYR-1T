using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CollectibleSpawner : NetworkBehaviour
{
    [SerializeField]
    GameObject applePrefab;
    
    public override void OnStartServer()
    {
        StartCoroutine(SpawnApplesAfterDelay());
    }

    IEnumerator SpawnApplesAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < 3; i++)
        {
            Vector2 pos = new Vector2(i * 2, -2);
            GameObject apple = Instantiate(applePrefab, pos, Quaternion.identity);
            NetworkServer.Spawn(apple);
        }
    }
}
