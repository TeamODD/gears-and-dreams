namespace Assets.Scripts.Cutting
{
    using System.Collections;
    using DG.Tweening;
    using UnityEngine;

    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField]
        private float _animationDuration;
        private GameObject _object;
        private bool _isRotating;
        private void Awake()
        {
            _object=gameObject;    
        }
        private void Update()
        {
            if(!_isRotating&&Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(rotateObject());
            }
        }
        IEnumerator rotateObject()
        {
            _isRotating=true;
            transform.DOLocalRotate(transform.eulerAngles+new Vector3(0,0, -45), _animationDuration);
            yield return new WaitForSeconds(_animationDuration);
            _isRotating=false;
        }
    }
}