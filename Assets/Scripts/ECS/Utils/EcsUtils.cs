using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace ECS.Utils
{
    public static class EcsUtils
    {
        public static bool CheckEntityCollisionByGroup<TAGroup, TBGroup>(CollisionEvent collisionEvent, 
            [ReadOnly] ComponentDataFromEntity<TAGroup> aGroup,
            [ReadOnly] ComponentDataFromEntity<TBGroup> bGroup,
            out Entity entityA, out Entity entityB) where TAGroup : struct, IComponentData
                                                    where TBGroup : struct, IComponentData
        {
            entityA = Entity.Null;
            entityB = Entity.Null;
            
            var innerEntityA = collisionEvent.EntityA;
            var innerEntityB = collisionEvent.EntityB;

            var entityAInAGroup = aGroup.HasComponent(innerEntityA);
            var entityBInAGroup = aGroup.HasComponent(innerEntityB);

            var entityAInBGroup = bGroup.HasComponent(innerEntityA);
            var entityBInBGroup = bGroup.HasComponent(innerEntityB);

            var hasA = entityAInAGroup || entityBInAGroup;
            var hasB = entityAInBGroup || entityBInBGroup;

            if (!(hasA && hasB)) return false;

            entityA = entityAInAGroup ? innerEntityA : innerEntityB;
            entityB = entityAInBGroup ? innerEntityA : innerEntityB;

            return true;
        }
    }
}