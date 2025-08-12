using UnityEngine;
using Code.CoreGameLogic;

namespace Code.UI
{
    /// <summary>
    /// 기물의 시각적 표현을 담당하는 유니티 컴포넌트
    /// 논리적 Piece 객체와 연결
    /// </summary>
    public class PieceView : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler inputHandler;
        
        // 이 뷰와 연결된 논리적 Piece 객체에 대한 참조
        public IPiece LogicalPiece { get; private set; }
        
        // 보드 상에서의 현재 위치
        public Vector2 CurrentPosition { get; set; }
        
        
        // 논리적 Piece와 뷰를 연결하는 메서드
        public void Initialize(IPiece logicalPiece, Vector2 position)
        {
            LogicalPiece = logicalPiece;
            CurrentPosition = position;
        }

        // 유니티의 마우스 클릭 이벤트를 감지
        private void OnMouseDown()
        {
            if (inputHandler != null)
            {
                inputHandler.HandlePieceClick(this);
            }
        }
    }
}