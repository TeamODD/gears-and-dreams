namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class ExecutableFillImage : ExecutableElement, IExecutionDuration
    {
        [SerializeField]
        private Image _targetImage;
        [field: SerializeField]
        public virtual float ExecutionDuration { get; protected set;}
        [SerializeField]
        private float _animationExponent=1;
        [SerializeField]
        private float _targetFillAmount;
        private float _initialFillAmount;
        public override IEnumerator Begin()
        {
            _initialFillAmount=_targetImage.fillAmount;
            yield return base.Begin();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;

            while (elapsedTime < ExecutionDuration && !_isSkipping)
            {
                float t = elapsedTime / ExecutionDuration;

                _targetImage.fillAmount=Mathf.Lerp(_initialFillAmount, _targetFillAmount, Mathf.Pow(t, _animationExponent));

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return base.Execute();
        }
        public override IEnumerator Finalize()
        {
            _targetImage.fillAmount=_targetFillAmount;
            yield return base.Finalize();
        }
    }
}