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

            _gameplayView.Update(_interfaceView.AnglePress);
            _model.Update(_gameplayView.AddFood, _gameplayView.AddWater);
            _interfaceView.Update(_model.Hunger, 0);

            if (_model.Hunger < 0f || _model.Thirst < 0f)
            {
                _interfaceView.ShowGameOver();
            }

            if (_interfaceView.Pause)
            {
                _interfaceView.ShowPause();
                _gameplayView.SetPause(true);
            }

            if (_interfaceView.Continue)
            {
                _interfaceView.ShowHUD();
                _gameplayView.SetPause(false);
            }
            
            
        }
    }
}