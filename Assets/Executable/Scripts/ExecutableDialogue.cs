namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using System.Text;
    using Unity.VisualScripting;

    [IncludeInSettings(true)]
    [UnitTitle("Executable/Dialogue")]
    [Serializable]
    public class ExecutableDialogue : ExecutableElement
    {
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private TMP_Text _wholeText;
        [SerializeField]
        private GameObject _nextDialogueIcon;
        [SerializeField]
        private string[] _dialogue;
        private int _index=0;
        private string _currentString;
        [SerializeField]
        private int _charactersPerSecond=30;
        public override IEnumerator Begin()
        {
            _isSkipping=false;
            yield return base.Begin();
        }

        public override IEnumerator Next()
        {
            while (_index < _dialogue.Length)
            {
                _text.text = "";
                _currentString = _dialogue[_index++];
                if(_wholeText!=null)
                {
                    _wholeText.text = _currentString;
                    LayoutRebuilder.ForceRebuildLayoutImmediate(_wholeText.GetComponent<RectTransform>());
                }
                yield return Execute();
                
                _isSkipping=false;
                if(_nextDialogueIcon!=null)
                {
                    _nextDialogueIcon.SetActive(true);
                }
                yield return new WaitUntil(() => _isSkipping);

                _isSkipping=false;
                if(_nextDialogueIcon!=null)
                {
                    _nextDialogueIcon.SetActive(false);
                }
            }
        }

        public override IEnumerator Execute()
        {
            StringBuilder stringBuilder=new();
            float elapseTime=0f;
            int currentIndex=0;

            while(currentIndex<_currentString.Length && !_isSkipping)
            {
                yield return null;
                elapseTime+=Time.deltaTime;
                int nextIndex=(int)(elapseTime*_charactersPerSecond);
                bool onTag=false;
                int tagNumber=0;
                for(;currentIndex<=nextIndex&&currentIndex<_currentString.Length;currentIndex++)
                {
                    stringBuilder.Append(_currentString[currentIndex]);
                    if(_currentString[currentIndex]=='<')
                    {
                        onTag=true;
                    }
                    else if(_currentString[currentIndex]=='>')
                    {
                        onTag=false;
                        elapseTime+=(float)tagNumber/_charactersPerSecond;
                        tagNumber=0;
                    }
                    if(onTag)
                    {
                        nextIndex++;
                        tagNumber++;
                    }
                }
                _text.text=stringBuilder.ToString();
            }
            _text.text = _currentString;
        }
    }
}