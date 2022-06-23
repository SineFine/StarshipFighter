using Controllers.Contracts;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Score : MonoBehaviour, IScoreView
    {
        [SerializeField] private TextMeshProUGUI _score;

        public void SetScore(int score)
        {
            _score.text = $"Score: {score}";
        }
    }
}
