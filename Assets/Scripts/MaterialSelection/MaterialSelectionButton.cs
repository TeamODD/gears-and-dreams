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
        private Button _button;
        private CanvasGroup _canvasGroup;
        private Sequence _fadeAnimationSequence;
        [SerializeField]
        private GameObject _materialLightPrafab;
        private Vector3 _endPosition; 
        private Sequence _moveLightAnimationSequence;
        private Material _material;
        public Material Material
        {
            get=>_material;
            set
            {
                _material=value;
                _materialImage.sprite=value.Sprite;
                _button.interactable=true;
                _canvasGroup.alpha=1f;
                _fadeAnimationSequence=DOTween.Sequence(gameObject);
                _fadeAnimationSequence.Append(_canvasGroup.DOFade(0f, _fadeDuration));
                _fadeAnimationSequence.OnComplete(()=>
                {
                    _canvasGroup.alpha=0f;
                    _button.interactable=false;
                });
            }
        }
        private void Awake()
        {
            _canvasGroup=GetComponent<CanvasGroup>();
            _materialImage=GetComponent<Image>();
            _button=GetComponent<Button>();
            _button.onClick.AddListener(OnClickButton);
        }
        private void Start()
        {
            _endPosition=GameObject.Find("End Position").GetComponent<RectTransform>().position;
        }
        private void OnClickButton()
        {
            _fadeAnimationSequence.Complete();
            GameObject lightObject=Instantiate(_materialLightPrafab, transform.root);
            lightObject.GetComponent<RectTransform>().anchoredPosition=Camera.main.WorldToScreenPoint(GetComponent<RectTransform>().position);
            Material currentMaterial=Material;
            _moveLightAnimationSequence=DOTween.Sequence(lightObject);
            _moveLightAnimationSequence.Append(lightObject.transform.DOMove(_endPosition, 2f)).SetEase(Ease.InCubic);
            _moveLightAnimationSequence.OnComplete(()=>
            {
                OnCompleteLightAnimation.Invoke(currentMaterial);
                Destroy(lightObject);
            });
        }
        public UnityAction<Material> OnCompleteLightAnimation;
    }
}