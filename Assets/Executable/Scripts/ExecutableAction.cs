namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;

    [Serializable]
    public class ExecutableAction : ExecutableElement
    {
        public UnityEvent _action;
        [SerializeField]
        private float _executionDuration;
        public override IEnumerator Begin()
        {
            _action.Invoke();
            yield return base.Begin();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _executionDuration && !_isSkipping)
            {
                float t = elapsedTime / _executionDuration;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return base.Execute();
        }
    }
}