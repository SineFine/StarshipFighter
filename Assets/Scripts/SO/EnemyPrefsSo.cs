using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
    public class EnemyPrefsSo : ScriptableObject
    {
        public GameObject EnemyPrefab;
        public float MaximumHP;
        public float MinMoveSpeed;
        public float MaxMoveSpeed;
        public float MinRotationSpeed;
        public float MaxRotationSpeed;
        public float Damage;
        public int VictoryPoint;
    }
}
