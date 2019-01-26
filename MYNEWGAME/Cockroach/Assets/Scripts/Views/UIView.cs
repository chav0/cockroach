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
        _screens.HUD.Hungry.value = hunger;
        _screens.HUD.Thirst.value = thirst; 
    }

    public void ShowGameOver()
    {
        _screens.HUD.gameObject.SetActive(false);
        _screens.MainMenu.gameObject.SetActive(true);
    }

    public void ShowMainMenu()
    {
        throw new System.NotImplementedException();
    }

    public void ShowPause()
    {
        throw new System.NotImplementedException();
    }

    public void ShowHUD()
    {
        throw new System.NotImplementedException();
    }
}
