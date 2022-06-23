using System.Linq;
using ECS.Components.Tags;
using ECS.Converters;
using Models.Contracts;
using Unity.Entities;

namespace Models
{
    public class GameStateModel : IGameStateModel
    {
        private static EntityManager EntityManager => World.DefaultGameObjectInjectionWorld.EntityManager;
        
        public void ResetGame()
        {
            var entityManager = EntityManager;
            
            var entities = entityManager.GetAllEntities();

            foreach (var entity in entities.Where(entity => entityManager.HasComponent(entity, typeof(DestroyWhenResetTag))))
            {
                entityManager.DestroyEntity(entity);
            }
        }

        public void StartGame()
        {
            EntityManager.Instantiate(SpaceshipConverter.Spaceship);
        }
    }
}