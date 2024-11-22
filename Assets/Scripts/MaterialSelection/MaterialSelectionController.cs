namespace Assets.Scripts.MaterialSelection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class MaterialSelectionController : MonoBehaviour
    {
        [SerializeField]
        private CollectedMaterial _collectedMaterial;
        [SerializeField]
        private TMP_Text _remainingCountText;
        private Dictionary<Material, int> _targetMaterialDictionary;
        private Dictionary<Material, int> _remainingMaterialDictionary;
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
        private float _fadeDuration;
        [SerializeField]
        private MaterialSelectionButton[] _selectionSlots;
        private void Awake()
        {
            _targetMaterialDictionary=new();
            _remainingMaterialDictionary=new();
            for(int i=0;i<5;i++)
            {
                int index=UnityEngine.Random.Range(1, _materialPool.Length-1);
                _targetMaterialDictionary[_materialPool[index]]=_targetMaterialDictionary.GetValueOrDefault(_materialPool[index], 0)+1;
                _remainingMaterialDictionary[_materialPool[index]]=_remainingMaterialDictionary.GetValueOrDefault(_materialPool[index], 0)+1;
                print(_materialPool[index].name);
            }
        }
        private void Start()
        {
            StartCoroutine(ActivateMaterialSelectionMode());
        }
        public IEnumerator ActivateMaterialSelectionMode()
        {
            Array.ForEach(_selectionSlots, selectionSlot=>
            {
                selectionSlot.FadeDuration=_fadeDuration;
                selectionSlot.GetComponent<Button>().onClick.AddListener(CompleteButtonSequence);
                selectionSlot.OnCompleteLightAnimation=_collectedMaterial.CollectMaterial;
                selectionSlot.OnClickMaterial=RemoveRemainingMaterial;
            });
            
            RemainingSelectionCount=_totalSelectionCount;
            while(RemainingSelectionCount>0)
            {
                RemainingSelectionCount--;

                foreach(Material material in _remainingMaterialDictionary.Keys)
                {
                    print(material.name+": "+_remainingMaterialDictionary[material]);
                }
                Material answerMaterial=_remainingMaterialDictionary.Keys.ElementAt(UnityEngine.Random.Range(0, _remainingMaterialDictionary.Count));
                int answerIndex=UnityEngine.Random.Range(0, _selectionSlots.Length);
                for(int i=0;i<_selectionSlots.Length;i++)
                {
                    if(answerIndex==i)
                    {
                        _selectionSlots[i].Material=answerMaterial;
                    }
                    else
                    {
                        _selectionSlots[i].Material=_materialPool[UnityEngine.Random.Range(1,_materialPool.Length)];
                    }
                }

                float elapsedTime=0f;
                while(elapsedTime<_timePerSelection)
                {
                    yield return null;
                    elapsedTime+=Time.deltaTime;
                }
            }
        }
        private void CompleteButtonSequence()
        {
            Array.ForEach(_selectionSlots, selectionSlot=>selectionSlot.GetComponent<Button>().interactable=false);
        }
        private void RemoveRemainingMaterial(Material material)
        {
            if(_remainingMaterialDictionary.TryGetValue(material, out int count))
            {
                _remainingMaterialDictionary[material]--;
                if(_remainingMaterialDictionary[material]<=0)
                {
                    _remainingMaterialDictionary.Remove(material);
                }
            }
        }
    }
}