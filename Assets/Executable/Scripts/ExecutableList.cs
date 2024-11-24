namespace Assets.Executable.Scripts
{
    using UnityEngine;

    public class ExecutableList : MonoBehaviour
    {
        private ExecutableElement[] _executables;
        public ExecutableElement[] Executables
        {
            get=>_executables;
        }
        private void Awake()
        {
            _executables=GetComponentsInChildren<ExecutableElement>(false);
        }
    }
}