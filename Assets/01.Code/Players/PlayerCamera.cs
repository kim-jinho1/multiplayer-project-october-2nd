using UnityEngine;

namespace Code.Players
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private GameObject mainCamera;
        private Quaternion player1 = Quaternion.Euler(47, 0, 0);
        private Quaternion player2 = Quaternion.Euler(47, 180, 0);

        private void OnEnable()
        {
            if(transform.position.z >= 1)
                mainCamera.transform.rotation = player2;
            else
                mainCamera.transform.rotation = player1;
        }
    }
}