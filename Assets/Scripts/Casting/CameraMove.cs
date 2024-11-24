using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GearsAndDreams
{
    public class CameraMove : MonoBehaviour
    {
        public Button button;
        private void Awake()
        {
            button.gameObject.SetActive(false);
            transform.DOMove(new Vector3(0f, 1.5f, -10f), 1.5f).SetEase(Ease.OutCubic);
            StartCoroutine(ButtonAppear());
        }

        private IEnumerator ButtonAppear()
        {
            yield return new WaitForSeconds(1.5f);
            button.gameObject.SetActive(true);
        }

        public void OnButtonClicked()
        {
            button.gameObject.SetActive(false);
        }
    }
}
