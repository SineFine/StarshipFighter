using Models.Contracts;
using Services.Contracts;

namespace Models
{
    public class ScoreModel : IScoreModel
    {
        private readonly IScoreService<int> _service;
        
        private int _bestScore;
        
        public ScoreModel(IScoreService<int> service)
        {
            _service = service;
            _bestScore = _service.Load();
        }
        
        public void SetScore(int data)
        {
            if (_bestScore > data) return;

            _bestScore = data;
            _service.Save(data);
        }
    }
}