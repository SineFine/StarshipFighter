using Services.Contracts;
using UnityEngine;

namespace Services
{
    public class PlayerPrefsScoreSaverService : IScoreService<int>
    {
        private const string ScorePrefsName = "Score";
        
        public void Save(int data)
        {
            PlayerPrefs.SetInt(ScorePrefsName, data);
        }

        public int Load()
        {
            if (!PlayerPrefs.HasKey(ScorePrefsName))
            {
                Save(0);
            }
            
            return PlayerPrefs.GetInt(ScorePrefsName);
        }
    }
}