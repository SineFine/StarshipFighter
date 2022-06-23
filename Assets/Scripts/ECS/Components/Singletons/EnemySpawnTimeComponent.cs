using Unity.Entities;

namespace ECS.Components.Singletons
{
    [GenerateAuthoringComponent]
    public struct EnemySpawnTimeComponent : IComponentData
    {
        public float SpawnTime;
    }
}
