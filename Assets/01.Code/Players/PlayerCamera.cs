using TMPro;
using UnityEngine;

namespace Code.Players
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private GameObject playerPos;
        [SerializeField] private GameObject mainCamera;
        private readonly Quaternion _player1 = Quaternion.Euler(47, 0, 0);
        private readonly Vector3 _playerPos1 = new Vector3(2.5f, 8, -4);
        private readonly Quaternion _player2 = Quaternion.Euler(47, 180, 0);
        private readonly Vector3 _playerPos2 = new Vector3(2.5f,8,12);

        public void PlayerSetting(PlayerID playerID)
        {
            if (playerID == PlayerID.Player1)
            {
                playerPos.transform.position = _playerPos1;
                mainCamera.transform.rotation = _player1;
            }
            if (playerID == PlayerID.Player2)
            {
                playerPos.transform.position = _playerPos2;
                mainCamera.transform.rotation = _player2;
            }
        }
    }
}