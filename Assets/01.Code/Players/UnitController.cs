using System.Collections.Generic;
using Code.CoreGameLogic;
using Code.Global;
using Code.UI;
using PurrNet;
using UnityEngine;

namespace Code.Players
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject obj;
        private Piece _selectedPiece;
        private IBoard _board;

        private void Start()
        {
            playerInput.OnMouseLeftClick += HandlePiece;
            UnitUI.Instance.OnSelectBoard += HandlePieceMove;
        }

        private void OnDestroy()
        {
            playerInput.OnMouseLeftClick -= HandlePiece;
            if (UnitUI.Instance != null)
                UnitUI.Instance.OnSelectBoard -= HandlePieceMove;
        }
        
        private void HandlePieceMove()
        { 
            //UnitActionUI.Instance.ActionPanel.SetActive(true);
            List<Vector3> moves = _selectedPiece.GetMoveRange(_selectedPiece.transform.position);

            foreach (Vector3 move in moves)
            {
                Instantiate(obj,move,Quaternion.identity);
            }
        }

        private void HandlePiece()
        {
            Ray ray = mainCamera.ScreenPointToRay(playerInput.MousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.TryGetComponent(out Piece boardSquare))
            {
                if (boardSquare != null)
                {
                    _selectedPiece = boardSquare;
                    UnitUI.Instance.UnitPanel.SetActive(true);
                    UnitUI.Instance.UnitInformationTitleText.text
                        = $"AP : {_selectedPiece.ConsumptionAP.value} Gold : {_selectedPiece.ConsumptionGold.value}";
                    UnitInformationUI.Instance.UnitInformationTitleText.text = $"{_selectedPiece.PieceName}의 정보";
                    UnitInformationUI.Instance.AttackPowerText.text = $"공격력 : {_selectedPiece.AttackPower.value}";
                    UnitInformationUI.Instance.DefenseText.text = $"방어력 : {_selectedPiece.DefensePower.value}";
                    UnitInformationUI.Instance.LoyaltyText.text = $"충성도 : {_selectedPiece.Loyalty.value}";
                }
            }
        }   
    }
}