namespace Assets.Scripts.Cutting
{
    using DG.Tweening;
    using GearsAndDreams.GameSystems;
    using UnityEngine;

    public class CameraMover : MonoBehaviour
    {
        private void Start()
        {
            transform.DOMove(new Vector3(0,0,-10),1.5f).SetEase(Ease.OutCubic);
        }
        public void MoveNext()
        {
            transform.DOMove(new Vector3(20, 0, -10), 1.5f).SetEase(Ease.OutCubic);
            GameManager.Instance?.ChangeGame();
        }
    }
}