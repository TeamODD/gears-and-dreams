namespace Assets.Scripts.MaterialSelection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class MaterialSelectionController : MonoBehaviour
    {
        [SerializeField]
        private CollectedMaterial _collectedMaterial;
        [SerializeField]
        private TMP_Text _remainingCountText;
        private Dictionary<Material, int> _targetMaterial;
        [SerializeField]
        private Material[] _materialPool;
        [SerializeField]
        private int _totalSelectionCount;
        private int _remainingSelectionCount;
        public int RemainingSelectionCount
        {
            get=>_remainingSelectionCount;
            set
            {
                _remainingSelectionCount=value;
                _remainingCountText.text=value.ToString();
            }
        }
        [SerializeField]
        private float _timePerSelection;
        [SerializeField]
        private MaterialSelectionButton[] _selectionSlots;
        private void Awake()
        {
            _targetMaterial=new();
            for(int i=0;i<5;i++)
            {
                int index=UnityEngine.Random.Range(1, _materialPool.Length);
                _targetMaterial[_materialPool[index]]=_targetMaterial.GetValueOrDefault(_materialPool[index], 0)+1;
            }
        }
        private void Start()
        {
            StartCoroutine(ActivateMaterialSelectionMode());
        }
        public IEnumerator ActivateMaterialSelectionMode()
        {
            RemainingSelectionCount=_totalSelectionCount;
            while(RemainingSelectionCount>0)
            {
                RemainingSelectionCount--;
                Array.ForEach(_selectionSlots, selectionSlot=>
                {
                    selectionSlot.Material=_materialPool[UnityEngine.Random.Range(1,_materialPool.Length)];
                    selectionSlot.OnCompleteLightAnimation=_collectedMaterial.CollectMaterial;
                });
                float elapsedTime=0f;
                while(elapsedTime<_timePerSelection)
                {
                    yield return null;
                    elapsedTime+=Time.deltaTime;
                }
            }
        }
    }
}