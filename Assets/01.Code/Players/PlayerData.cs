using UnityEngine; // Debug.Log를 사용하기 위함 (디버깅용)

namespace Code.Players
{
    public enum PlayerID
    {
        Player1,
        Player2
    }

    public class PlayerData
    {
        public PlayerID ID { get; private set; }
        public string Name { get; private set; }
        public int Gold { get; set; }
        public int AP { get; set; }
        
        private PurrNet.PlayerID PlayerID { get; set; }
        
        public PlayerData(PlayerID id, string name, int initialGold)
        {
            ID = id;
            Name = name;
            Gold = initialGold;
        }
        
        public void AddGold(int amount)
        {
            Gold += amount;
        }

        public void AddAP(int amount)
        {
            AP += amount;
        }
    }
}