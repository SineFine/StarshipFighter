using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Systems
{
    public partial class InputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithoutBurst().ForEach((ref SpaceshipMoveDataComponent moveData, ref SpaceshipFireDataComponent fireData) =>
            {
                moveData.Directions.x = Input.GetAxis("Horizontal");
                moveData.Directions.y = Input.GetAxis("Vertical");

                fireData.IsPressed = Input.GetKeyDown(KeyCode.Space);
                fireData.IsHold = Input.GetKey(KeyCode.Space);
            }).Run();
        }
    }
}