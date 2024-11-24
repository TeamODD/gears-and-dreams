namespace Assets.Executable.Scripts
{
    using System.Collections;
    public interface IExcutable
    {
        public void Skip();
        //초기화 메서드+다음 메서드
        public IEnumerator Begin();
        //완료가 됐을 때 다음으로 넘어가기 위한 초기화 메서드
        public IEnumerator Next();
        //실행 메서드
        public IEnumerator Execute();
        //중간 분기에서 기다리는 메서드
        public IEnumerator Pause();
        //실행 메서드를 중단하고 바로 완료찍는 메서드
        public IEnumerator Finalize();
        //실행 간 목표 지점으로 완료찍는 메서드
        public IEnumerator Complete();
        //초기화 함수 (예: 시작 시작 설정(몇 초 기다리기) 등)
        public IEnumerator Initialize();
    }
}