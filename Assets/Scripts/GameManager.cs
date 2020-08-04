using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Controls controls;
    public static GameManager instance;

    private void Awake()
    {
        if (controls == null) controls = new Controls();
        controls.Kart.Enable();

        instance = this;
    }



}
