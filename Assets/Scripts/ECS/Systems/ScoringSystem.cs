using System;
using ECS.Components;
using ECS.Components.Tags;
using ECSSystemEvent;
using Unity.Collections;
using Unity.Entities;

namespace ECS.Systems
{
    [UpdateBefore(typeof(DestroySystem))]
    public partial class ScoringSystem : SystemBase, ISystemValueChange<int>
    {
        public event Action<int> OnValueChange;

        private NativeArray<int> _pointContainer;

        protected override void OnCreate()
        {
            _pointContainer = new NativeArray<int>(1, Allocator.Persistent);
        }

        protected override void OnStartRunning()
        {
            _pointContainer[0] = 0;
        }

        protected override void OnDestroy()
        {
            _pointContainer.Dispose();
        }

        protected override void OnUpdate()
        {
            var pointContainer = _pointContainer;

            Entities
                .WithAll<DestroyByPlayerTagComponent>()
                .ForEach((in PointForKillComponent point) =>
                {
                    pointContainer[0] += point.Points;
                }).Run();
            
            OnValueChange?.Invoke(pointContainer[0]);
        }
    }
}