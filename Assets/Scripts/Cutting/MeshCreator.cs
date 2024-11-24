namespace Assets.Scripts.Cutting
{
    using System.Collections.Generic;
    using UnityEngine;

    public class MeshCreator : MonoBehaviour
    {
        [SerializeField]
        Material _punchMaterial;
        [SerializeField]
        GameObject _parentObject;
        List<Vector3> verticesList=new();
        private GearCuttingChecker _gearCuttingChecker;
        private void Start()
        {
            _gearCuttingChecker=FindAnyObjectByType<GearCuttingChecker>();
        }
        void Update()
        {
            if(_gearCuttingChecker.CuttingCount<=0)
            {
                return;
            }
            if(Input.GetMouseButtonDown(0))
            {
                verticesList=new();
            }
            if(Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                verticesList.Add(new Vector3(mousePos.x, mousePos.y, 0));
            }
            if(Input.GetMouseButtonUp(0))
            {
                CreateMesh(verticesList);
            }

        }
        private void CreateMesh(List<Vector3> verticesList)
        {
            int length=verticesList.Count;
            verticesList.Add((verticesList[length-1]+verticesList[0])/2);
            for(int i=1;i<length-1;i++)
            {
                verticesList.Add(ReflectPoint(verticesList[i], verticesList[0], verticesList[length-1]));
            }
            Vector3[] vertices= verticesList.ToArray();
            Mesh mesh = new Mesh();

            List<int> intsList=new();
            for(int i=0;i<vertices.Length-1;i++)
            {
                intsList.Add(length);
                intsList.Add(i);
                intsList.Add(i+1);
            }
            int[] triangles = intsList.ToArray();

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            GameObject meshObject=new();
            if(_parentObject!=null)
            {
                meshObject.transform.SetParent(_parentObject.transform, true);
            }

            MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;

            MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshRenderer.material = new Material(_punchMaterial);
        }
        private Vector3 ReflectPoint(Vector3 point, Vector3 linePoint1, Vector3 center)
        {
            Vector3 lineDir = (linePoint1 - center)/2;
            Vector3 reflected = (lineDir+center)*2 - point;

            return reflected;
        }
    }
}