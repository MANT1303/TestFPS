using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleScene : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void ToScene(int scene)
    {
        PhotonNetwork.LoadLevel(scene);
    }
}
