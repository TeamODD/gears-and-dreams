namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using System.Linq;

    [Serializable]
    public class ExecutableExecuteSerial : ExecutableElement, IExecutionDuration
    {
        public virtual float ExecutionDuration { get; protected set;}
        private ExecutableElement[] _executableElements;
        private int _currentIndex;
        public override void Skip()
        {
            _executableElements[_currentIndex].Skip();
            base.Skip();
        }
        public override IEnumerator Begin()
        {
            _executableElements=GetComponentsInChildren<ExecutableElement>()
            .Where(element => element != this) // 자기 자신 제외
            .ToArray();
            _isSkipping=false;
            yield return base.Begin();
        }
        public override IEnumerator Next()
        {
            for(_currentIndex=0;_currentIndex<_executableElements.Length;_currentIndex++)
            {
                _isSkipping=false;
                print(_executableElements[_currentIndex].name);
                yield return Execute();
            }
        }
        public override IEnumerator Execute()
        {
            yield return _executableElements[_currentIndex].Initialize();
        }
    }
}