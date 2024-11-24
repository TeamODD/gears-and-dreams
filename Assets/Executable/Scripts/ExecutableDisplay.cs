namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;

    [Serializable]
    public class ExecutableDisplay : ExecutableElement, IExecutionDuration
    {
        [SerializeField]
        private GameObject _displayObject;
        [field: SerializeField]
        public virtual float ExecutionDuration { get; protected set;}
        [SerializeField]
        private float _animationExponent=1;
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private float _staringAlpha;
        public override IEnumerator Initialize()
        {
            if(!_displayObject.TryGetComponent<CanvasGroup>(out _canvasGroup))
            {
                _canvasGroup=_displayObject.AddComponent<CanvasGroup>();
            }
            _canvasGroup.alpha=_staringAlpha;
            _displayObject.SetActive(true);
            yield return base.Initialize();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;

            while (elapsedTime < ExecutionDuration && !_isSkipping)
            {
                float t = elapsedTime / ExecutionDuration;

                _canvasGroup.alpha = Mathf.Lerp(_staringAlpha, 1f, Mathf.Pow(t, _animationExponent));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return base.Execute();
        }
        public override IEnumerator Finalize()
        {
            _canvasGroup.alpha = 1f;
            yield return base.Finalize();
        }
    }
}