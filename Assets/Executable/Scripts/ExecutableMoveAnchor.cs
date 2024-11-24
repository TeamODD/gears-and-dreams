namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;

    [Serializable]
    public class ExecutableMoveAnchor : ExecutableElement, IExecutionDuration
    {
        [SerializeField]
        private RectTransform _targetRectTransform;
        [field: SerializeField]
        public virtual float ExecutionDuration { get; protected set;}
        [SerializeField]
        private float _animationExponent=1;
        [SerializeField]
        private Vector2 _targetAnchorMin;
        [SerializeField]
        private Vector2 _targetAnchorMax;
        private Vector2 _initialAnchorMin;
        private Vector2 _initialAnchorMax;
        public override IEnumerator Begin()
        {
            _initialAnchorMin=_targetRectTransform.anchorMin;
            _initialAnchorMax=_targetRectTransform.anchorMax;
            yield return base.Begin();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;

            while (elapsedTime < ExecutionDuration && !_isSkipping)
            {
                yield return null;
                float t = elapsedTime / ExecutionDuration;

                _targetRectTransform.anchorMax=_targetRectTransform.pivot=Vector2.Lerp(_initialAnchorMax, _targetAnchorMax, Mathf.Pow(t, _animationExponent));
                _targetRectTransform.anchorMin=_targetRectTransform.pivot=Vector2.Lerp(_initialAnchorMin, _targetAnchorMin, Mathf.Pow(t, _animationExponent));

                elapsedTime += Time.deltaTime;
            }
            yield return base.Execute();
        }
        public override IEnumerator Finalize()
        {
            _targetRectTransform.anchorMax=_targetAnchorMax;
            _targetRectTransform.anchorMin=_targetAnchorMin;
            yield return base.Finalize();
        }
    }
}