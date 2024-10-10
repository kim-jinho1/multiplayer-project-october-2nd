using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterSelection : NetworkBehaviour
{
    [SerializeField] private List<GameObject> Animals = new List<GameObject>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private GameObject characterSelectionPanel;
    [SerializeField]
    private GameObject canvasObject;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!base.IsOwner)
        {
            canvasObject.SetActive(false);
        }
    }

    public void SpawnFish()
    {
        characterSelectionPanel.SetActive(false);
        Spawn(0,0,LocalConnection);
    }
    
    public void SpawnCube()
    {
        characterSelectionPanel.SetActive(false);
        Spawn(1,1,LocalConnection);
    }

    [ServerRpc(RequireOwnership = false)]
    void Spawn(int spawnIndex,int spawnPointIndex,NetworkConnection conn)
    {
        GameObject player = Instantiate(Animals[spawnIndex],spawnPoints[spawnPointIndex].position, Quaternion.identity);
        Spawn(player,conn);
    }
}