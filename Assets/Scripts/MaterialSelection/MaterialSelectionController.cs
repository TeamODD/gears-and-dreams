namespace Assets.Scripts.MaterialSelection
{
    using System;
    using System.Collections;
    using UnityEngine;
    
    public class MaterialSelectionController : MonoBehaviour
    {
        [SerializeField]
        private Material[] _materialPool;
        [SerializeField]
        private int _totalSelectionCount;
        private int _remainingSelectionCount;
        [SerializeField]
        private float _timePerSelection;
        [SerializeField]
        private MaterialSelectionButton[] _selectionSlots;
        public IEnumerator ActivateMaterialSelectionMode()
        {
            _remainingSelectionCount=_totalSelectionCount;
            while(_remainingSelectionCount>0)
            {
                Array.ForEach(_selectionSlots, selectionSlot=>selectionSlot.Material=_materialPool[UnityEngine.Random.Range(1,_materialPool.Length)]);
                float elapsedTime=0f;
                while(elapsedTime<_timePerSelection)
                {
                    yield return null;
                    elapsedTime+=Time.deltaTime;
                }
                _remainingSelectionCount--;
            }
        }
    }
}