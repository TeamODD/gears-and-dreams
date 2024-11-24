namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public abstract class ExecutableElement : MonoBehaviour, IExcutable
    {
        [field: SerializeField]
        public float WaitStartTime { get; private set; }
        [SerializeField]
        protected float _puaseDuration;
        [SerializeField]
        protected Button _skipButton;
        protected bool _isSkipping;
        [SerializeField]
        protected bool isRequiredClickToComplete;
        public virtual void Skip()
        {
            _isSkipping=true;
        }
        public virtual IEnumerator Initialize()
        {
            if(_skipButton!=null)
            {
                _skipButton.gameObject.SetActive(true);
                _skipButton.onClick.AddListener(Skip);
            }

            float elapsedTime=0f;
            while (elapsedTime < WaitStartTime && !_isSkipping)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return Begin();
        }
        public virtual IEnumerator Begin()
        {
            if(!_isSkipping)
            {
                yield return Next();
            }
            yield return Finalize();
        }
        public virtual IEnumerator Next()
        {
            yield return Execute();
        }

        public virtual IEnumerator Execute()
        {
            yield break;
        }
        public virtual IEnumerator Pause()
        {
            _isSkipping=false;
            float elapsedTime=0f;

            while (elapsedTime < _puaseDuration && !_isSkipping)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return Complete();
        }

        public virtual IEnumerator Finalize()
        {
            yield return Pause();
        }

        public virtual IEnumerator Complete()
        {
            if(_skipButton!=null)
            {
                _skipButton.onClick.RemoveListener(Skip);
                _skipButton.gameObject.SetActive(false);
            }
            yield break;
        }
    }
}