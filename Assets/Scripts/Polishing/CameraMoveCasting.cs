using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GearsAndDreams.Polishing
{
    public class CameraMoveCasting : MonoBehaviour
    {
        public Button button;
        private void Awake()
        {
            button.gameObject.SetActive(false);
            transform.DOMove(new Vector3(0f, 0f, -10f), 1.5f).SetEase(Ease.OutCubic);
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
