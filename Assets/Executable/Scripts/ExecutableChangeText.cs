namespace Assets.Executable.Scripts
{
    using System;
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class ExecutableChangeText : ExecutableElement
    {
        [SerializeField]
        private TMP_Text _targetTextObject;
        [TextArea]
        [SerializeField]
        private string _changeText;
        public override IEnumerator Finalize()
        {
            _targetTextObject.text=_changeText;
            LayoutRebuilder.ForceRebuildLayoutImmediate(_targetTextObject.GetComponent<RectTransform>());
            yield return base.Finalize();
        }
    }
}