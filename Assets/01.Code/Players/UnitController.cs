using Code.CoreGameLogic;
using Code.Global;
using Code.UI;
using UnityEngine;

namespace Code.Players
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private Camera mainCamera;
        private Piece _selectedPiece;

        private void Awake()
        {
            playerInput.OnMouseLeftClick += HandlePiece;
        }

        private void OnDestroy()
        {
            playerInput.OnMouseLeftClick -= HandlePiece;
        }

        private void HandlePiece()
        {
            Vector3 mousePosition = playerInput.MousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            
            if (_selectedPiece is not null)
            {
                _selectedPiece = null;
            }
    
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.TryGetComponent(out Piece piece))
            {
                _selectedPiece = piece;
                UnitUI.Instance.UnitPanel.SetActive(true);
                UnitUI.Instance.UnitInformationTitleText.text
                    = $"AP : {_selectedPiece.ConsumptionAP.value} Gold : {_selectedPiece.ConsumptionGold.value}";
                UnitInformationUI.Instance.UnitInformationTitleText.text = $"{_selectedPiece.PieceName}의 정보";
                UnitInformationUI.Instance.AttackPowerText.text = $"공격력 : {_selectedPiece.AttackPower.value}";
                UnitInformationUI.Instance.DefenseText.text = $"방어력 : {_selectedPiece.DefensePower.value}";
                UnitInformationUI.Instance.LoyaltyText.text = $"충성도 : {_selectedPiece.Loyalty.value}";

            }
            else
            {
                _selectedPiece = null;
                Debug.Log("클릭한 위치에 유닛(기물)이 없습니다.");
            }
        }
    }
}