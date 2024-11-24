namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;

    [Serializable]
    public class ExecutableExecuteParallel : ExecutableElement, IExecutionDuration
    {
        public virtual float ExecutionDuration { get; protected set;}
        [SerializeReference]
        private ExecutableElement[] _executableElements;
        private ElementExecutor _elementExecutor;
        public override void Skip()
        {
            Array.ForEach(_executableElements, element=>element.Skip());
            base.Skip();
        }
        private float GetMaxDuration()
        {
            float maxDuration=0;
            foreach(ExecutableElement executableElement in _executableElements)
            {
                if(executableElement is IExecutionDuration executionDurationElement)
                {
                    maxDuration=Mathf.Max(maxDuration, executionDurationElement.ExecutionDuration+executableElement.WaitStartTime);
                }
                else
                {
                    throw new Exception("ExecutableExecuteParallel: An executable element is not IExecutableDuration");
                }
            }
            return maxDuration;
        }
        public override IEnumerator Initialize()
        {
            ExecutionDuration=GetMaxDuration();
            _elementExecutor=FindAnyObjectByType<ElementExecutor>();
            yield return base.Initialize();
        }
        public override IEnumerator Begin()
        {
            Array.ForEach(_executableElements, element=>_elementExecutor.StartExecutableElement(element));
            yield return base.Begin();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;
            while (elapsedTime < ExecutionDuration && !_isSkipping)
            {
                float t = elapsedTime / ExecutionDuration;

                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }
        public override IEnumerator Finalize()
        {
            Array.ForEach(_executableElements, element=>element.Skip());
            yield return base.Finalize();
        }
    }
}