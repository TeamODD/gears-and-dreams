namespace Assets.Scripts.MaterialSelection
{
    using DG.Tweening;
    using UnityEngine;

    public class MoveCanvas : MonoBehaviour
    {
        private void Start()
        {
            transform.DOMove(new Vector3(-0, 0, 0), 1.5f).SetEase(Ease.OutCubic);
        }
    }
}