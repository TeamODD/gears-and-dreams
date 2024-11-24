namespace Assets.Scripts.MaterialSelection
{
    using GearsAndDreams.GameSystems;
    using TMPro;
    using UnityEngine;

    public class GetDayCount : MonoBehaviour
    {
        private void Start()
        {
            if(GameManager.Instance!=null)
                GetComponent<TMP_Text>().text=GameManager.Instance.CurrentDay+"일차";
        }
    }
}