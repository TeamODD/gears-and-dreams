namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;

    [Serializable]
    public class ExecutableRotate : ExecutableElement, IExecutionDuration
    {
        [SerializeField]
        private RectTransform _targetRectTransform;
        [field: SerializeField]
        public virtual float ExecutionDuration { get; protected set;}
        [SerializeField]
        private float _animationExponent=1f;
        [SerializeField]
        private Vector3 _targetRotation;
        private Vector3 _initialRotation;
        public override IEnumerator Begin()
        {
            _initialRotation=_targetRectTransform.eulerAngles;
            yield return base.Begin();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;

            while (elapsedTime < ExecutionDuration && !_isSkipping)
            {
                float t = elapsedTime / ExecutionDuration;

                _targetRectTransform.eulerAngles=Vector3.Lerp(_initialRotation, _targetRotation, Mathf.Pow(t, _animationExponent));

                yield return null;
                elapsedTime += Time.deltaTime;
            }
            _targetRectTransform.eulerAngles=_targetRotation;
            yield return base.Execute();
        }
        public override IEnumerator Finalize()
        {
            _targetRectTransform.eulerAngles=_targetRotation;
            yield return base.Finalize();
        }
    }
}