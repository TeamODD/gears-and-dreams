namespace Assets.Scripts.MaterialSelection
{
    using DG.Tweening;
    using GearsAndDreams.GameSystems;
    using UnityEngine;

    public class MoveCanvas : MonoBehaviour
    {
        private void Start()
        {
            transform.DOMove(new Vector3(-0, 0, 0), 1.5f).SetEase(Ease.OutCubic);
        }
        public void MoveNext()
        {
            transform.DOMove(new Vector3(-18, 0, 0), 1.5f).SetEase(Ease.OutCubic).OnComplete(()=>GameManager.Instance?.ChangeGame());
        }
    }
}