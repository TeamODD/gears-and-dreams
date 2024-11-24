namespace Assets.Scripts.MaterialSelection
{
    using DG.Tweening;
    using GearsAndDreams.GameSystems;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine;

    public class GetMaterial : MonoBehaviour
    {
        [SerializeField]
        private MaterialSelectionController _controller;
        [SerializeField]
        private Transform _listTransform;
        [SerializeField]
        private GameObject _listItem;
        private void Start()
        {
            foreach(Material material in _controller._targetMaterialDictionary.Keys)
            {
                MaterialListItem item=Instantiate(_listItem, _listTransform).GetComponent<MaterialListItem>();
                item.Material=material;
                item.SetNumber(_controller._targetMaterialDictionary[material]);
            }
        }
        private void OnEnable()
        {
            print("CHHHHHHH");
            transform.DOMove(new Vector3(0,0,0),1.5f).SetEase(Ease.OutCubic);
        }
    }
}