using Project.Scripts.Models;
using Project.Scripts.Views;
using UnityEngine;

namespace Project.Scripts.Presenters
{
    public class ApplicationPresenter : IApplicationPresenter
    {
        private readonly IGameplayView _gameplayView;
        private readonly IUserInterfaceView _interfaceView;
        private readonly IApplicationModel _model;
        private readonly Screens _screens;
        private readonly GameSettings _settings; 

        private float _pregnantTimer;
        private float _deathTimer;
        private bool _isMenu = true;
        private bool _isPause; 

        public ApplicationPresenter(IGameplayView gameplayView, IUserInterfaceView interfaceView,
            IApplicationModel model, Screens screens, GameSettings settings)
        {
            _gameplayView = gameplayView;
            _interfaceView = interfaceView;
            _model = model;
            _screens = screens;
            _settings = settings; 
        }   
        
        public void Update()
        {
            if (_interfaceView.Pause)
            {
                _gameplayView.SetPause(true);
                _interfaceView.ShowPause();
                _isPause = true; 
            }

            if (!_isPause && !_isMenu)
            {
                _gameplayView.Update(_interfaceView.AnglePress);
                _model.Update(_gameplayView.AddFood, _gameplayView.AddWater, _gameplayView.CockNum);
                _interfaceView.Update(_model.Hunger / _model.FullHunger, _model.Thirst / _model.FullThirst);

                if (_gameplayView.IsGameOver)
                {
                    _interfaceView.ShowGameOver();
                    _isMenu = true; 
                }
                else
                {
                    if ((_model.Hunger / _model.FullHunger < _settings.DeathPorog || _model.Thirst / _model.FullThirst < _settings.DeathPorog) &&
                        Time.time - _deathTimer > 3f)
                    {
                        _gameplayView.DeathCockroach();
                        _deathTimer = Time.time;
                    }

                    if (_model.Hunger / _model.FullHunger > _settings.PregnantPorog && Time.time - _pregnantTimer > _settings.PregnantIntervals)
                    {
                        _gameplayView.SetPregnant();
                        _pregnantTimer = Time.time;
                        Debug.Log("PREGNANT");
                    }

                    if (_interfaceView.Pause)
                    {
                        _interfaceView.ShowPause();
                        _gameplayView.SetPause(true);
                    }

                    _interfaceView.ShowDeath(_gameplayView.DeathMarker);
                }
            }

            if (_interfaceView.Continue)
            {
                _interfaceView.ShowHUD();
                _gameplayView.SetPause(false);
                _isPause = false; 
            }

            if (_interfaceView.NewGame)
            {
                _gameplayView.CreateCockroach(Age.Adult);
                _deathTimer = Time.time; 
                _interfaceView.ShowHUD();
                _isMenu = false; 
            }

            if (_interfaceView.Defeat)
            {
                _gameplayView.ResetWorld();
                _interfaceView.ShowMainMenu();
                _isMenu = true;
                _interfaceView.Defeat = false; 
            }
        }
    }
}