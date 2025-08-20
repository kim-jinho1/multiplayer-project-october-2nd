using Code.CoreGameLogic;
using PurrNet;

namespace Code.Players
{
    public class Player : NetworkBehaviour
    {
        public PlayerData PlayerData {get ; private set;}
        
        protected override void OnSpawned()
        {
            base.OnSpawned();
            RegisterPlayer();
        }

        [ServerRpc]
        private void RegisterPlayer()
        {
            GameManager gm = FindObjectOfType<GameManager>();
            PlayerData = gm.RegisterPlayer();
            GetComponent<PlayerCamera>().PlayerSetting(PlayerData.ID);
        }
    }
}