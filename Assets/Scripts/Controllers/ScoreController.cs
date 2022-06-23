using Controllers.Contracts;
using ECS.Systems;
using ECSSystemEvent;
using Models.Contracts;
using Unity.Entities;

namespace Controllers
{
    public class ScoreController : SystemEventReceiver<int>, IScoreReset
    {
        private readonly IScoreView _scoreView;
        private readonly IScoreModel _scoreModel;

        private int _currentScore;
    
        public ScoreController(IScoreView scoreView, IScoreModel scoreModel) 
            : base(World.DefaultGameObjectInjectionWorld.GetExistingSystem<ScoringSystem>())
        {
            _scoreView = scoreView;
            _scoreModel = scoreModel;
        }

        public void ResetScore()
        {
            _currentScore = 0;
            _scoreView.SetScore(_currentScore);
        }

        protected override void OnValueChange(int data)
        {
            _currentScore += data;
            
            _scoreModel.SetScore(_currentScore);
            _scoreView.SetScore(_currentScore);
        }
    }
}