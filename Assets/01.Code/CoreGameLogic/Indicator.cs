using UnityEngine;

namespace Code.CoreGameLogic
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private GameObject indicator;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Unit"))
            {
                indicator.transform.localScale = new Vector3(0.6f, 0.3f, 0.6f);
            }
        }
    }
}