using System.Collections.Generic;
using Project.Scripts.Presenters;
using UnityEngine;

namespace Project.Scripts
{
    public class Init : MonoBehaviour
    {
        [SerializeField] private GameSettings GameSettings;
        [SerializeField] private Prefabs Prefabs;
        [SerializeField] private Screens Screens;
        [SerializeField] private Camera MainCamera; 
        private IApplicationPresenter _presenter;
        
        public void Start()
        {
            var cockroaches = new List<Cockroach>();

            var gameplayView = new GamePlayView(cockroaches, Prefabs, GameSettings, MainCamera);
            var interfaceView = new UIView(Screens);
            var model = new GameModel(GameSettings);
            _presenter = new ApplicationPresenter(gameplayView, interfaceView, model, Screens, GameSettings);
        }

        public void Update()
        {
            _presenter.Update();
        }
    }
}