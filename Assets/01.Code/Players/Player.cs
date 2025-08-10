using Code.StrategicSystem;

namespace Code.Players
{
    /// <summary>
    /// 게임에 참여하는 플레이어를 나타내는 열거형입니다.
    /// </summary>
    public enum PlayerID
    {
        Player1,
        Player2
    }

    /// <summary>
    /// 게임 내 플레이어의 정보를 담는 클래스입니다.
    /// 플레이어의 식별 정보와 국정 상태를 관리합니다.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 플레이어의 고유 식별자입니다.
        /// </summary>
        public PlayerID ID { get; private set; }

        /// <summary>
        /// 플레이어의 닉네임 또는 이름입니다.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 플레이어의 국정 상태(기술, 경제 등)에 접근하는 인터페이스입니다.
        /// </summary>
        public IPlayerNationState NationState { get; private set; }

        public Player(PlayerID id, string name, IPlayerNationState nationState)
        {
            ID = id;
            Name = name;
            NationState = nationState;
        }
    }
}