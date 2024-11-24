using GearsAndDreams.GameSystems;
using GearsAndDreams.Polishing.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GearsAndDreams.Polishing
{
    public class NextButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(()=>
            {
                GameManager.Instance?.ChangeGame();
            });
        }
    }
}
