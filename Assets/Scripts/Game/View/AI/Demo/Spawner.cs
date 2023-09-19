using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.View.AI.Demo
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float min = -100;
        [SerializeField] private float max = 100;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int amount = 100;


        private Vector3 _min;
        private Vector3 _max;

        private void Start()
        {
            _min = Vector3.one * min;
            _max = Vector3.one * max;
            for (int i = 0; i < amount; i++)
            {
                Instantiate(prefab, GetRandomPos(), Quaternion.identity);
            }
        }


        public Vector3 GetRandomPos()
        {
            return new Vector3(Random.Range(_min.x, _max.x), 0, Random.Range(_min.z, _max.z));
        }
    }
}