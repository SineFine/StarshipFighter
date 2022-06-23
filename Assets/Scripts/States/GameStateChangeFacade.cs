using System;
using System.Runtime.CompilerServices;

namespace States
{
    public class GameStateChangeFacade : IGameStateChanger
    {
        public event Action<GameState> OnStateChange
        {
            add => Subscribe(value);
            remove => Unsubscribe(value);
        }

        private readonly IGameStateChanger[] _gameStateChangers;

        public GameStateChangeFacade(params IGameStateChanger[] gameStateChanger)
        {
            _gameStateChangers = gameStateChanger;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Subscribe(Action<GameState> value)
        {
            for (var i = 0; i < _gameStateChangers.Length; i++)
            {
                _gameStateChangers[i].OnStateChange += value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe(Action<GameState> value)
        {
            for (var i = 0; i < _gameStateChangers.Length; i++)
            {
                _gameStateChangers[i].OnStateChange -= value;
            }
        }
    }
}