﻿using Project.Scripts.Presenters;
using UnityEngine;

namespace Project.Scripts
{
    public class Init : MonoBehaviour
    {
        private IApplicationPresenter _presenter;
        
        public void Start()
        {
            _presenter = new ApplicationPresenter(null, null, null);
        }

        public void Update()
        {
            _presenter.Update();
        }
    }
}