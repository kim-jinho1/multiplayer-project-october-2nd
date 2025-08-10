namespace Code.Command
{
    /// <summary>
    /// 플레이어가 내리는 모든 행동(기물 이동, 정책 실행 등)을 하나의 통일된 명령 객체로 추상화하는 인터페이스입니다.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 명령이 실행될 때 호출되는 메서드입니다.
        /// </summary>
        void Execute();

        /// <summary>
        /// 명령 실행이 완료되었는지 여부를 나타내는 속성입니다.
        /// </summary>
        bool IsComplete { get; }

        /// <summary>
        /// (선택 사항) 명령을 취소할 때 호출되는 메서드입니다.
        /// </summary>
        void Undo();
    }
}