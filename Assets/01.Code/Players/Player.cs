using Code.CoreGameLogic;
using PurrNet;
using UnityEngine;

namespace Code.Players
{
    public class Player : NetworkIdentity
    {
        public PlayerData PlayerData {get ; private set;}
        public PurrNet.PlayerID PlayerId {get ; private set;}
        
        protected override void OnSpawned()
        {
            base.OnSpawned();
            RegisterPlayer();
        }

        [ServerRpc]
        private void RegisterPlayer()
        {
            GameManager gm = FindAnyObjectByType<GameManager>();
            PlayerData = gm.RegisterPlayer();
            GetComponent<PlayerCamera>().PlayerSetting(PlayerData.ID);
        }
    }
}