using UnityEngine;

namespace SO
{
    public enum WeaponType
    {
        Artillery
    }
    
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
    public class WeaponPrefsSo : ScriptableObject
    {
        public GameObject BulletPrefab;
        public float Speed;
        public float Damage;
        public bool DestroyOnImpact;
        public WeaponType WeaponType;
    }
}
