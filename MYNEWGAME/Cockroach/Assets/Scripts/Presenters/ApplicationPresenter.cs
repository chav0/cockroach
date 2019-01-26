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
            _interfaceView.Update(0, 0);
            _model.Update();
            _gameplayView.Update(_interfaceView.AnglePress);
        }
    }
}