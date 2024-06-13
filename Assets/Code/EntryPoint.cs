using Assets.Code.UI;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public void Start()
    {
        BaseWindow.Get<MainWindow>().Show();
    }
    
}