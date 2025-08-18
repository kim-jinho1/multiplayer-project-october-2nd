using System;
using Code.CoreGameLogic;
using PurrNet;
using TMPro;
using UnityEngine;

namespace Code.Players
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private TMP_Text gameManager;
        private PlayerData  _playerData;
        
        protected override void OnSpawned()
        {
            base.OnSpawned();
            RegisterPlayer();
        }

        [ServerRpc]
        private void RegisterPlayer()
        {
            GameManager gm = FindObjectOfType<GameManager>();
            _playerData = gm.RegisterPlayer();
            Debug.Log(_playerData.ID);
        }
        
    }
}