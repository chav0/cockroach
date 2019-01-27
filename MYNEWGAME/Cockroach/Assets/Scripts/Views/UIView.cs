using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Views;
using Project.Scripts.Views.UserInput;
using UnityEngine;

public class UIView : IUserInterfaceView
{
    private readonly Screens _screens;
    private float _deathTimer;
    private string _oldMessage;
    private UIMessage _pause = new UIMessage(); 
    private UIMessage _continue = new UIMessage(); 

    public UIView(Screens screens)
    {
        _screens = screens;
        _screens.HUD.Pause.onClick.AddListener(_pause.Set);
        _screens.Pause.Continue.onClick.AddListener(_continue.Set);
    }

    public Vector2 AnglePress => _screens.HUD.InputField.Angle;

    public bool Pause => _pause.TryGet(); 
    public bool NewGame { get; }
    public bool Continue => _continue.TryGet(); 

    public void Update(float hunger, float thirst)
    {
        _screens.HUD.Hungry.fillAmount = hunger;
        _screens.HUD.Thirst.fillAmount = thirst;

        if (Time.time - _deathTimer > 1)
        {
            _screens.HUD.header.gameObject.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        _screens.MainMenu.gameObject.SetActive(false);
        _screens.Defeat.gameObject.SetActive(true);
        _screens.HUD.gameObject.SetActive(false);
        _screens.Pause.gameObject.SetActive(false);
    }

    public void ShowMainMenu()
    {
        _screens.MainMenu.gameObject.SetActive(true);
        _screens.Defeat.gameObject.SetActive(false);
        _screens.HUD.gameObject.SetActive(false);
        _screens.Pause.gameObject.SetActive(false);
    }

    public void ShowPause()
    {
        _screens.MainMenu.gameObject.SetActive(false);
        _screens.Defeat.gameObject.SetActive(false);
        _screens.HUD.gameObject.SetActive(false);
        _screens.Pause.gameObject.SetActive(true);
    }

    public void ShowHUD()
    {
        _screens.MainMenu.gameObject.SetActive(false);
        _screens.Defeat.gameObject.SetActive(false);
        _screens.HUD.gameObject.SetActive(true);
        _screens.Pause.gameObject.SetActive(false);
    }

    public void ShowDeath(string deathMarker)
    {
        if (deathMarker == null || deathMarker.Equals(string.Empty))
        {
            _screens.HUD.header.gameObject.SetActive(false);
        }
        else
        {
            if (!deathMarker.Equals(_oldMessage))
            {
                _screens.HUD.header.gameObject.SetActive(true);
                _screens.HUD.header.text = deathMarker;
                _screens.HUD.header2.text = deathMarker;
                _oldMessage = deathMarker; 
                _deathTimer = Time.time;
                Debug.Log(deathMarker);
            }
        }
    }
}
