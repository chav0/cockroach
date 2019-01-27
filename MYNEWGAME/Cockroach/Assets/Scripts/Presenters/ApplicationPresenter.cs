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

        private float _pregnantTimer;
        private float _deathTimer;
        private bool _isMenu;
        private bool _isPause; 

        public ApplicationPresenter(IGameplayView gameplayView, IUserInterfaceView interfaceView,
            IApplicationModel model, Screens screens)
        {
            _gameplayView = gameplayView;
            _interfaceView = interfaceView;
            _model = model;
            _screens = screens;           
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
                }
                else
                {
                    if ((_model.Hunger / _model.FullHunger < 0.2f || _model.Thirst / _model.FullThirst < 0.2f) &&
                        Time.time - _deathTimer > 3f)
                    {
                        _gameplayView.DeathCockroach();
                        _deathTimer = Time.time;
                    }

                    if (_model.Hunger / _model.FullHunger > 0.9f && Time.time - _pregnantTimer > 5f)
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
            
            
        }
    }
}