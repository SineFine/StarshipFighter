using System;

namespace States
{
    public interface IGameStateChanger
    {
        event Action<GameState> OnStateChange;
    }
}