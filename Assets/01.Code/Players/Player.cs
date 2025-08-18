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
        public PlayerData  _playerData {get ; private set;}
        
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
            GetComponent<PlayerCamera>().PlayerSetting(_playerData.ID);
        }
        
    }
}