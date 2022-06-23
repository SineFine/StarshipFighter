using System;
using Controllers.Contracts;
using ECS.Systems;
using ECSSystemEvent;
using Models.Contracts;
using States;
using Unity.Entities;

namespace Controllers
{
    public class GameStateController : SystemEventReceiver<bool>
    {
        private readonly IGameStateModel _gameStateModel;
        private readonly IResetMenu _resetMenu;
        private readonly IScoreReset _scoreReset;
        private readonly IGameStateChanger _gameStateChanger;

        public GameStateController(IGameStateChanger gameStateChanger, IGameStateModel gameStateModel, IScoreReset scoreReset, IResetMenu resetMenu)
            : base(World.DefaultGameObjectInjectionWorld.GetExistingSystem<SpaceshipCheckerSystem>())
        {
            _gameStateModel = gameStateModel;
            _resetMenu = resetMenu;
            _scoreReset = scoreReset;
            _gameStateChanger = gameStateChanger;
            
            _scoreReset.ResetScore();
        }

        protected override void OnValueChange(bool data)
        {
            _resetMenu.Show();
        }

        public override void Subscribe()
        {
            base.Subscribe();
            _gameStateChanger.OnStateChange += GameStateChange;
        }
        
        public override void Unsubscribe()
        {
            base.Unsubscribe();
            _gameStateChanger.OnStateChange += GameStateChange;
        }
        
        private void GameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    StartGame();
                    break;
                case GameState.Restart:
                    RestartGame();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        
        private void StartGame()
        {
            _scoreReset.ResetScore();
            _gameStateModel.StartGame();
        }
        
        private void RestartGame()
        {
            _resetMenu.Close();
            _gameStateModel.ResetGame();
            StartGame();
        }
    }
}