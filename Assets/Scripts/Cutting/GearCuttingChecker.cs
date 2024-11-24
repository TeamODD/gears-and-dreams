namespace Assets.Scripts.Cutting
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.Events;

    public class GearCuttingChecker : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer[] _insideGuideLines;
        private bool _isCutting=false;
        private ObjectRotator _objectRotator;
        [SerializeField]
        private GameObject _gearPanel;
        public int CuttingCount=8;
        private void Start()
        {
            _objectRotator=FindAnyObjectByType<ObjectRotator>();
        }
        void Update()
        {
            if(CuttingCount<=0 || _objectRotator._isRotating) return;
            if (Input.GetMouseButtonDown(0))
            {
                _isCutting=true;
            }
            if (_isCutting&&Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider != null)
                {
                    foreach(var guide in _insideGuideLines)
                    {
                        if(hit.collider.gameObject == guide.gameObject)
                        {
                            OnIncorrectMotion.Invoke();
                            _isCutting=false;
                            print("Cutted Inside");
                            return;
                        }
                    }
                }
                else if(hit.collider == null)
                {
                    OnIncorrectMotion.Invoke();
                    _isCutting=false;
                    print("Cutted Outside");
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                StartCoroutine(_objectRotator.RotateObject());
                _isCutting=false;
                CuttingCount--;
                if(CuttingCount<=0)
                {
                    OnCompleteCutting.Invoke();
                    _gearPanel.transform.DOMove(Vector3.zero, 1.5f);
                    _gearPanel.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1.5f);
                }
            }
        }
        [field:SerializeField]
        private UnityEvent OnIncorrectMotion;
        [field:SerializeField]
        private UnityEvent OnCompleteCutting;
    }
}