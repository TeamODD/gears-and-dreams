namespace Assets.Scripts.MaterialSelection
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class MaterialListItem : MonoBehaviour
    {
        private Material _material;
        public Material Material
        {
            get=>_material;
            set
            {
                _material=value;
                GetComponent<Image>().sprite=value.Sprite;
            }
        }
        public void SetNumber(int number)
        {
            GetComponentInChildren<TMP_Text>().text="x"+number.ToString();
        }
    }
}