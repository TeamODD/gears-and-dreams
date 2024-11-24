namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class ExecutablePrompt : ExecutableElement
    {
        [SerializeField]
        private ExecutableElement _beforeSelectExecuteElement;
        [SerializeField]
        private ExecutableElement[] _executableElements;
        [SerializeField]
        private Button[] _executeButtons;
        [SerializeField]
        private ExecutableElement _afterSelectExecuteElement;
        private bool _isSelected;
        private int _executeIndex;
        private void SelectElement(int index)
        {
            _executeIndex=index;
            _isSelected=true;
        }
        private void SkipBeforeElement()
        {
            _beforeSelectExecuteElement.Skip();
        }
        private void SkipAfterElement()
        {
            _afterSelectExecuteElement.Skip();
        }
        public override IEnumerator Begin()
        {
            _skipButton.onClick.AddListener(SkipBeforeElement);
            if(_isSkipping)
            {
                _beforeSelectExecuteElement.Skip();
            }
            if(_beforeSelectExecuteElement!=null)
            {
                yield return _beforeSelectExecuteElement.Initialize();
            }
            _skipButton.onClick.RemoveListener(SkipBeforeElement);

            _isSkipping=false;
            for(int i=0;i<_executeButtons.Length;i++)
            {
                _executeButtons[i].onClick.AddListener(()=>SelectElement(i));
            }
            yield return base.Begin();
        }
        public override IEnumerator Next()
        {
            _skipButton.gameObject.SetActive(false);
            yield return new WaitUntil(()=>_isSelected);
            _skipButton.gameObject.SetActive(true);
            for(int i=0;i<_executeButtons.Length;i++)
            {
                _executeButtons[i].onClick.RemoveAllListeners();
            }
            yield return base.Next();
        }
        public override IEnumerator Execute()
        {
            _skipButton.onClick.AddListener(SkipAfterElement);
            if(_isSkipping)
            {
                _afterSelectExecuteElement.Skip();
            }
            if(_afterSelectExecuteElement!=null)
            {
                yield return _afterSelectExecuteElement.Initialize();
            }
            _skipButton.onClick.RemoveListener(SkipAfterElement);

            _skipButton.gameObject.SetActive(false);
            if(_executeIndex<_executableElements.Length&&_executableElements[_executeIndex]!=null)
            {
                yield return _executableElements[_executeIndex].Initialize();
            }
            _skipButton.gameObject.SetActive(true);
        }
    }
}