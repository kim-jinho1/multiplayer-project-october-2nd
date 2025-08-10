using UnityEngine;
using Code.StrategicSystem;

namespace Code.UI
{
    public class BoardUI : MonoBehaviour
    {
        public void MovePieceView(PieceView pieceView, Vector2 to)
        {
            pieceView.transform.position = new Vector3(to.x, 0, to.y);
            pieceView.CurrentPosition = to;
            Debug.Log($"{pieceView.LogicalPiece.PieceName}가 {to} 위치로 이동했습니다.");
        }
    }
}