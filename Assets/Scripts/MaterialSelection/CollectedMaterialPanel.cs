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
        public Dictionary<Material, int> CollectedMaterialCount;
        private void Awake()
        {
            CollectedMaterialCount=new();    
        }
        public void CollectMaterial(Material material)
        {
            CollectedMaterialCount[material]=CollectedMaterialCount.GetValueOrDefault(material, 0)+1;
            GameObject collectedMaterialIcon=Instantiate(_collectedMaterialIconPrefab, _collectedMaterialPanel);
            collectedMaterialIcon.GetComponent<Image>().sprite=material.Sprite;
        }
    }
}