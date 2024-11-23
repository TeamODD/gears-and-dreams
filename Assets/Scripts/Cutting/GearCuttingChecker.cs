namespace Assets.Scripts.Cutting
{
    using UnityEngine;
    using UnityEngine.Events;

    public class GearCuttingChecker : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer[] _insideGuideLines;
        private bool _isCutting=false;
        private ObjectRotator _objectRotator;
        private void Start()
        {
            _objectRotator=FindAnyObjectByType<ObjectRotator>();
        }
        void Update()
        {
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
            }
        }
        [field:SerializeField]
        private UnityEvent OnIncorrectMotion;
    }
}