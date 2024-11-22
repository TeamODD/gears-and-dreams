namespace Assets.Scripts.MaterialSelection
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class CollectedMaterial : MonoBehaviour
    {
        [SerializeField]
        private Transform _collectedMaterialPanel;
        [SerializeField]
        private GameObject _collectedMaterialIconPrefab;
        public Dictionary<Material, int> _collectedMaterialCount;
        private void Awake()
        {
            _collectedMaterialCount=new();    
        }
        public void CollectMaterial(Material material)
        {
            _collectedMaterialCount[material]=_collectedMaterialCount.GetValueOrDefault(material, 0)+1;
            GameObject collectedMaterialIcon=Instantiate(_collectedMaterialIconPrefab, _collectedMaterialPanel);
            collectedMaterialIcon.GetComponent<Image>().sprite=material.Sprite;
        }
    }
}