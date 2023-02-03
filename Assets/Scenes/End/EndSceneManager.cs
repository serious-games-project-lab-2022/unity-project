using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class EndSceneManager : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        DestroyAllPermanentObjects();
        SceneManager.LoadScene("Menu");
    }

    private void DestroyAllPermanentObjects()
    {
        Destroy(GameManager.Singleton.sharedGameState.gameObject);
        Destroy(GameManager.Singleton.scenarioManager.gameObject);
        Destroy(GameManager.Singleton.gameObject);
        Destroy(NetworkManager.Singleton.gameObject);
    }
}
