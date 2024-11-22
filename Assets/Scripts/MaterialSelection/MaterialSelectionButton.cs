namespace Assets.Scripts.MaterialSelection
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public class MaterialSelectionButton : MonoBehaviour
    {
        [SerializeField]
        private float _fadeDuration;
        private Image _materialImage;
        private CanvasGroup _canvasGroup;
        private Sequence _fadeAnimationSequence;
        private Material _material;
        public Material Material
        {
            get=>_material;
            set
            {
                _material=value;
                _materialImage.sprite=value.Sprite;
                _fadeAnimationSequence.Restart();
            }
        }
        private void Start()
        {
            _canvasGroup=GetComponent<CanvasGroup>();
            _materialImage=GetComponent<Image>();

            gameObject.SetActive(false);

            _fadeAnimationSequence=DOTween.Sequence();
            _fadeAnimationSequence.OnStart(()=>gameObject.SetActive(true));
            _fadeAnimationSequence.Append(_canvasGroup.DOFade(0f, _fadeDuration));
            _fadeAnimationSequence.OnComplete(()=>gameObject.SetActive(false));
        }
        private void OnClickButton()
        {
            _fadeAnimationSequence.Complete();
        }
        public UnityAction OnCompleteAnimation;
    }
}