namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;

    [Serializable]
    public class ExecutableScale : ExecutableElement, IExecutionDuration
    {
        [SerializeField]
        private RectTransform _targetRectTransform;
        [field: SerializeField]
        public virtual float ExecutionDuration { get; protected set;}
        [SerializeField]
        private float _animationExponent=1;
        [SerializeField]
        private Vector3 _targetScale;
        private Vector3 _initialScale;
        public override IEnumerator Begin()
        {
            _initialScale=_targetRectTransform.localScale;
            yield return base.Begin();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;

            while (elapsedTime < ExecutionDuration && !_isSkipping)
            {
                float t = elapsedTime / ExecutionDuration;

                _targetRectTransform.localScale=Vector3.Lerp(_initialScale, _targetScale, Mathf.Pow(t, _animationExponent));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        public override IEnumerator Finalize()
        {
            _targetRectTransform.localScale=_targetScale;
            yield return base.Finalize();
        }
    }
}