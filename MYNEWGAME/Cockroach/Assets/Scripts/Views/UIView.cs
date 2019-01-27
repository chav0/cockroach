using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Views;
using UnityEngine;

public class UIView : IUserInterfaceView
{
    private readonly Screens _screens; 

    public UIView(Screens screens)
    {
        _screens = screens; 
    }

    public Vector2 AnglePress => _screens.HUD.InputField.Angle;
    
    public bool Pause { get; }
    public bool NewGame { get; }
    public bool Continue { get; }

    public void Update(float hunger, float thirst)
    {
        _screens.HUD.Hungry.fillAmount = hunger;
        _screens.HUD.Thirst.fillAmount = thirst; 
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
}
