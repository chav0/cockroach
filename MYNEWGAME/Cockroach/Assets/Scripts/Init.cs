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

            for (int i = 0; i < 10; i++)
            {
                var newCockroach = Instantiate(Prefabs.Cockroach).GetComponent<Cockroach>();
                newCockroach.CockroachCreate(Time.time, Age.Adult);
                newCockroach.transform.position = new Vector3(Random.Range(0f, 3f), Random.Range(0f, 3f));
                newCockroach.speedScaler = Random.Range(0.98f, 1.02f);
                newCockroach.transform.localScale = Vector3.one * Random.Range(0.9f, 1.1f);
                newCockroach.lerp = Random.Range(0.85f, 0.95f);
                cockroaches.Add(newCockroach);
            }

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