namespace Assets.Executable.Scripts
{
    using System.Collections;
    using UnityEngine;

    public class ElementExecutor : MonoBehaviour
    {
        [SerializeField]
        private ExecutableElement _executableElement;
        private void Start()
        {
            StartCoroutine(Initialize());
        }
        private IEnumerator Initialize()
        {
            if(_executableElement==null)
            {
                yield break;
            }
            yield return _executableElement.Initialize();
        }
        public void StartExecutableElement(ExecutableElement executableElement)
        {
            StartCoroutine(ExecuteExecutableElement(executableElement));
        }
        private IEnumerator ExecuteExecutableElement(ExecutableElement executableElement)
        {
            yield return executableElement.Initialize();
        }
    }
}