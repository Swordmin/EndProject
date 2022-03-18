using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{

    [SerializeField] private bool _startScene;

    private void Awake()
    {
        if(_startScene)
            LoadLvl("Menu");
    }

    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLvl(int id) 
    {
        SceneManager.LoadScene(id);
    }  
    public void LoadLvl(string id) 
    {
        SceneManager.LoadScene(id);
    }

}
