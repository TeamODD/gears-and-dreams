namespace Assets.Scripts.MaterialSelection
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "Material", menuName = "Material/Material")]
    public class Material : ScriptableObject
    {
        [field:SerializeField]
        public string Name { get; private set; }
        [field:SerializeField]
        public Sprite Sprite { get; private set; }
    }
}