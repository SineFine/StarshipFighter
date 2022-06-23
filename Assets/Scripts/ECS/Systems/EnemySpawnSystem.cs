using System.Runtime.CompilerServices;
using ECS.Components;
using ECS.Components.Singletons;
using ECS.Components.Tags;
using ECS.Converters;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    public partial class EnemySpawnSystem : SystemBase
    {
        private PlayingFieldSizeComponent _playingFieldSizes;
        private EnemySpawnTimeComponent _enemySpawnTime;
        private Random _randomGenerator;
        private float _spawnTimer;
        private Entity[] _asteroidsEntities;
        private int _asteroidsCount;

        protected override void OnCreate()
        {
            _randomGenerator = new Random(5);
            _spawnTimer = 0.0f;
            
            RequireSingletonForUpdate<PlayerTagComponent>();
        }

        protected override void OnStartRunning()
        {
            _playingFieldSizes = GetSingleton<PlayingFieldSizeComponent>();
            _enemySpawnTime = GetSingleton<EnemySpawnTimeComponent>();
            
            _asteroidsEntities = EnemyConverter.AsteroidsEntities;
            
            _asteroidsCount = _asteroidsEntities.Length;
        }

        protected override void OnUpdate()
        {
            _spawnTimer -= Time.DeltaTime;

            if (_spawnTimer > 0.0f) return;
            
            _spawnTimer = _enemySpawnTime.SpawnTime;

            var entityPrefab = GetEntityForSpawn();
            var entity = EntityManager.Instantiate(entityPrefab);
            
            EntityManager.AddComponentData(entity, new Translation
            {
                Value = GetPositionForSpawn()
            });

            EntityManager.AddComponentData(entity, GetAsteroidDataComponent(entity));
            
            EntityManager.AddComponent<EnemyTagComponent>(entity);
            EntityManager.AddComponent<DestroyAfterLeaveLeftBound>(entity);
            EntityManager.AddComponent<DestroyWhenResetTag>(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Entity GetEntityForSpawn()
        {
            var index = _randomGenerator.NextInt(_asteroidsCount);

            return _asteroidsEntities[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float3 GetPositionForSpawn()
        {
            var position =
                _randomGenerator.NextFloat3(_playingFieldSizes.UpperRightPoint, _playingFieldSizes.LowerRightPoint);
            
            return position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EnemyDataComponent GetAsteroidDataComponent(Entity entity)
        {
            var asteroidSettings = EntityManager.GetComponentData<EnemyDataSettingsComponent>(entity);

            var asteroidData = new EnemyDataComponent
            {
                Damage = asteroidSettings.Damage,
                Hp = asteroidSettings.MaximumHp,
                MoveSpeed = _randomGenerator.NextFloat(asteroidSettings.MinMoveSpeed, asteroidSettings.MaxMoveSpeed),
                RotationSpeed = _randomGenerator.NextFloat(asteroidSettings.MinRotationSpeed, asteroidSettings.MaxRotationSpeed)
            };

            return asteroidData;
        }
    }
}