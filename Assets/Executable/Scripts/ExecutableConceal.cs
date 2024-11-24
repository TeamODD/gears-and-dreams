namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;

    [Serializable]
    public class ExecutableConceal : ExecutableElement, IExecutionDuration
    {
        [SerializeField]
        private GameObject _concealObject;
        [field: SerializeField]
        public virtual float ExecutionDuration { get; protected set;}
        [SerializeField]
        private float _animationExponent=1;
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private float _endingAlpha;
        public override IEnumerator Initialize()
        {
            if(!_concealObject.TryGetComponent<CanvasGroup>(out _canvasGroup))
            {
                _canvasGroup=_concealObject.AddComponent<CanvasGroup>();
            }
            _canvasGroup.alpha=1f;
            yield return base.Initialize();
        }
        public override IEnumerator Execute()
        {
            float elapsedTime = 0f;

            while (elapsedTime < ExecutionDuration && !_isSkipping)
            {
                float t = elapsedTime / ExecutionDuration;

                _canvasGroup.alpha = Mathf.Lerp(1f, _endingAlpha, Mathf.Pow(t, _animationExponent));

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return base.Execute();
        }
        public override IEnumerator Finalize()
        {
            _canvasGroup.alpha = _endingAlpha;
            _concealObject.SetActive(false);
            yield return base.Finalize();
        }
        public override IEnumerator Complete()
        {
            yield return base.Complete();
        }
    }
}