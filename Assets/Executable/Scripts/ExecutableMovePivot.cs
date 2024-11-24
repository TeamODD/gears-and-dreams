namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;

    [Serializable]
    public class ExecutableMovePivot : ExecutableElement, IExecutionDuration
    {
        [SerializeField]
        private RectTransform _targetRectTransform;
        [field: SerializeField]
        public virtual float ExecutionDuration { get; protected set;}
        [SerializeField]
        private float _animationExponent=1;
        [SerializeField]
        private Vector2 _targetPivot;
        private Vector2 _initialPivot;
        public override IEnumerator Begin()
        {
            _initialPivot=_targetRectTransform.pivot;
            yield return base.Begin();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;

            while (elapsedTime < ExecutionDuration && !_isSkipping)
            {
                yield return null;
                float t = elapsedTime / ExecutionDuration;

                _targetRectTransform.pivot=Vector2.Lerp(_initialPivot, _targetPivot, Mathf.Pow(t, _animationExponent));

                elapsedTime += Time.deltaTime;
            }
            yield return base.Execute();
        }
        public override IEnumerator Finalize()
        {
            _targetRectTransform.pivot=_targetPivot;
            yield return base.Finalize();
        }
    }
}