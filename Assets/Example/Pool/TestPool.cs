using System.Threading.Tasks;
using UnityEngine;

namespace WEngine.Test
{
    public class TestPool : MonoBehaviour
    {
        private Camera _camera;
        private string poolCubeKey = "Cube";
        private string poolSphere = "Sphere";

        private bool isCube = true;
        private PrefabPool _cubePool;
        private PrefabPool _spherePool;
        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
            _cubePool = new PrefabPool(GameObject.CreatePrimitive(PrimitiveType.Cube));

            CoreEngine.Pool.AddPool(poolCubeKey, _cubePool);
            _spherePool = new PrefabPool(GameObject.CreatePrimitive(PrimitiveType.Sphere));

            CoreEngine.Pool.AddPool(poolSphere, _spherePool);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var point = GetPoint();
                var key = isCube ? poolCubeKey : poolSphere;
                var go = CoreEngine.Pool.Get(key);
                go.name = key;
                go.transform.SetPositionAndRotation(point, Quaternion.identity);
                go.SetActive(true);
                DisposePrefabs(isCube ? _cubePool : _spherePool, go);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                isCube = !isCube;
            }
        }

        private async void DisposePrefabs(PrefabPool PrefabPool, GameObject go)
        {
            await Task.Delay(3000);
            if (isCube)
            {
                PrefabPool.Remove(go);
            }
            else
                PrefabPool.Remove(go);
        }

        private Vector3 GetPoint()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
    }
}