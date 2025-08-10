using Code.CoreGameLogic;

namespace Code.StrategicSystem
{
    /// <summary>
    /// 기물의 충성도를 관리하고 계산하는 기능을 정의하는 인터페이스입니다.
    /// </summary>
    public interface ILoyaltyManager
    {
        /// <summary>
        /// 특정 기물의 충성도를 반환합니다.
        /// </summary>
        int GetLoyalty(Piece piece);

        /// <summary>
        /// 특정 기물의 충성도를 변경합니다.
        /// </summary>
        void ChangeLoyalty(Piece piece, int amount);

        /// <summary>
        /// 반란 발생 가능성을 확인합니다.
        /// </summary>
        bool CheckForRebellion(Piece piece);
    }
}