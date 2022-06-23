using System.Collections.Generic;
using ECS.Components;
using ECS.Components.Tags;
using SO;
using Unity.Entities;
using UnityEngine;

namespace ECS.Converters
{
    public class SpaceshipWeaponConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private WeaponPrefsSo[] _starshipWeapon;

        public static Dictionary<WeaponType, Entity> Weapons { private set; get; }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            Weapons = new Dictionary<WeaponType, Entity>();

            foreach (var starshipWeaponSo in _starshipWeapon)
            {
                var weaponEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(starshipWeaponSo.BulletPrefab,
                    GameObjectConversionSettings.FromWorld(dstManager.World, conversionSystem.BlobAssetStore));
                
                Weapons.Add(starshipWeaponSo.WeaponType, weaponEntity);

                dstManager.AddComponentData(weaponEntity, new BulletDataComponent
                {
                    Damage = starshipWeaponSo.Damage,
                    Speed = starshipWeaponSo.Speed,
                    WeaponType = starshipWeaponSo.WeaponType,
                    DestroyOnImpact = starshipWeaponSo.DestroyOnImpact
                });

                dstManager.AddComponentData(weaponEntity, new BulletTagComponent());
            }
        }
    }
}