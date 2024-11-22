namespace Assets.Scripts.MaterialSelection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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
        private List<Material> _targetMaterialList;
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
            _targetMaterialList=new();
            for(int i=0;i<5;i++)
            {
                int index=UnityEngine.Random.Range(1, _materialPool.Length-1);
                _targetMaterialList.Add(_materialPool[index]);
                _targetMaterialDictionary[_materialPool[index]]=_targetMaterialDictionary.GetValueOrDefault(_materialPool[index], 0)+1;
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
            });
            
            RemainingSelectionCount=_totalSelectionCount;
            while(RemainingSelectionCount>0)
            {
                RemainingSelectionCount--;
                int answerIndex=UnityEngine.Random.Range(1, _selectionSlots.Length);
                for(int i=0;i<_selectionSlots.Length;i++)
                {
                    _selectionSlots[i].Material=i!=answerIndex
                    ?_materialPool[UnityEngine.Random.Range(1,_materialPool.Length)]
                    :_targetMaterialList[RemainingSelectionCount];
                    _selectionSlots[i].OnCompleteLightAnimation=_collectedMaterial.CollectMaterial;
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
    }
}