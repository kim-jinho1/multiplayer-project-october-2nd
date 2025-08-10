using UnityEngine; // Debug.Log를 사용하기 위함 (디버깅용)

namespace Code.Players
{
    public enum PlayerID
    {
        Player1,
        Player2,
        Neutral
    }

    public class Player
    {
        public PlayerID ID { get; private set; }
        // 예시로 다른 속성을 추가할 수 있습니다.
        public string Name { get; private set; }
        public int Gold { get; private set; }

        // DependencyContainer에서 호출하는 방식에 맞게 PlayerID만 인자로 받도록 수정
        public Player(PlayerID id)
        {
            ID = id;
            Name = id.ToString(); // 기본 이름은 ID로 설정
            Gold = 1000; // 초기 골드 설정
            Debug.Log($"플레이어 생성: {ID}, 초기 골드: {Gold}");
        }

        // 필요하다면 다른 생성자 오버로드를 추가할 수 있습니다.
        public Player(PlayerID id, string name, int initialGold)
        {
            ID = id;
            Name = name;
            Gold = initialGold;
            Debug.Log($"플레이어 생성: {ID} ({Name}), 초기 골드: {Gold}");
        }

        // 플레이어의 골드를 추가하는 메서드 (예시)
        public void AddGold(int amount)
        {
            Gold += amount;
            Debug.Log($"{ID}의 골드가 {amount}만큼 변경되어 현재 골드: {Gold}");
        }
    }
}