using Code.CoreGameLogic;

namespace Code.StrategicSystem
{
    /// <summary>
    /// 기물의 충성도를 관리하고 계산하는 기능을 정의하는 인터페이스
    /// </summary>
    public interface ILoyaltyManager
    {
        /// <summary>
        /// 특정 기물의 충성도를 반환
        /// </summary>
        int GetLoyalty(Piece piece);

        /// <summary>
        /// 특정 기물의 충성도를 변경
        /// </summary>
        void ChangeLoyalty(Piece piece, int amount);

        /// <summary>
        /// 반란 발생 가능성을 확인
        /// </summary>
        bool CheckForRebellion(Piece piece);
    }
}