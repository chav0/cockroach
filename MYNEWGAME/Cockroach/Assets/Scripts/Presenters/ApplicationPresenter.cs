using Project.Scripts.Models;
using Project.Scripts.Views;

namespace Project.Scripts.Presenters
{
    public class ApplicationPresenter : IApplicationPresenter
    {
        private readonly IGameplayView _gameplayView;
        private readonly IUserInterfaceView _interfaceView;
        private readonly IApplicationModel _model;

        public ApplicationPresenter(IGameplayView gameplayView, IUserInterfaceView interfaceView,
            IApplicationModel model)
        {
            _gameplayView = gameplayView;
            _interfaceView = interfaceView;
            _model = model;
        }
        
        public void Update()
        {
            //each update we do same things as always:
            
            //get and use inputs
            
            //update model and react to state change if it exists
            
            //update views in correct order using input and model data
            
            //react to gameplay state change, for example gameOver
        }
    }
}