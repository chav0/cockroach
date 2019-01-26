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
            var newCockroach = Instantiate(Prefabs.Cockroach).GetComponent<Cockroach>();
            newCockroach.CockroachCreate(Time.time, Age.Adult);
            var cockroaches = new List<Cockroach>(); 
            cockroaches.Add(newCockroach);
            var gameplayView = new GamePlayView(cockroaches, Prefabs.Cockroach.gameObject, GameSettings, MainCamera);
            var interfaceView = new UIView(Screens);
            var model = new GameModel();
            _presenter = new ApplicationPresenter(gameplayView, interfaceView, model, Screens);
        }

        public void Update()
        {
            _presenter.Update();
        }
    }
}