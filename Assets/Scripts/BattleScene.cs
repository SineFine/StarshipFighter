using System.Collections.Generic;
using Controllers;
using ECSSystemEvent;
using Models;
using Services;
using States;
using UI;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Score _score;
    [SerializeField] private ResetMenu _resetMenu;
    [SerializeField] private MainMenu _mainMenu;

    private List<BaseSystemEventReceiver> _eventReceivers;
    
    private void Awake()
    {
        _eventReceivers = new List<BaseSystemEventReceiver>();
        
        var scoreService = new PlayerPrefsScoreSaverService();
        
        _mainMenu.SetBestScore(scoreService.Load());
        
        var scoreController = new ScoreController(_score, new ScoreModel(scoreService));

        var gameStateController = new GameStateController(new GameStateChangeFacade(_resetMenu, _mainMenu),
            new GameStateModel(), scoreController, _resetMenu);

        _eventReceivers.AddRange(new BaseSystemEventReceiver[]
        {
            new HealthController(_healthBar),
            scoreController,
            gameStateController
        });
    }

    private void OnEnable()
    {
        foreach (var eventReceiver in _eventReceivers)
        {
            eventReceiver.Subscribe();
        }
    }

    private void OnDisable()
    {
        foreach (var eventReceiver in _eventReceivers)
        {
            eventReceiver.Unsubscribe();
        }
    }

}