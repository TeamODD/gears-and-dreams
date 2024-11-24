namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class ExecutableChangeColor : ExecutableElement, IExecutionDuration
    {
        [SerializeField]
        private Image _targetImage;
        [field: SerializeField]
        public virtual float ExecutionDuration { get; protected set;}
        [SerializeField]
        private float _animationExponent=1;
        [SerializeField]
        private Color _targetColor;
        private Color _initialColor;
        public override IEnumerator Begin()
        {
            _initialColor=_targetImage.color;
            yield return base.Begin();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;

            while (elapsedTime < ExecutionDuration && !_isSkipping)
            {
                float t = elapsedTime / ExecutionDuration;

                _targetImage.color=Color.Lerp(_initialColor, _targetColor, Mathf.Pow(t, _animationExponent));

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return base.Execute();
        }
        public override IEnumerator Finalize()
        {
            _targetImage.color=_targetColor;
            return base.Finalize();
        }
    }
}