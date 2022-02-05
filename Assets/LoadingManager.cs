using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;

public class LoadingManager : MonoBehaviour
{
    public Canvas loadingCanvas;
    public Text loadingText;

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
        loadingCanvas.gameObject.SetActive(true);
        loadingText.text = "Loading...";
    }

    public void LoadingComplete()
    {
        loadingText.text = "Complete!";
        loadingCanvas.gameObject.SetActive(false);
    }
}
