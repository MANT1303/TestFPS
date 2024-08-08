using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class LobbyPhoton : MonoBehaviourPunCallbacks
{
    [Header("Lobby")]
    [SerializeField] private GameObject _connectionRoomPanel;
    [SerializeField] private Button _createRoomBtn;
    [SerializeField] private Button _joinRoomBtn;
    [SerializeField] private TMP_InputField _inputField;
    [Header("Room")]
    [SerializeField] private GameObject _inRoomPanel;
    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _readyBtn;
    [SerializeField] private TextMeshProUGUI _connectedText;
    [Header("Settings")]
    [SerializeField] private int _maxPlayers;
    private void Start()
    {
        _createRoomBtn.interactable = false;
        _joinRoomBtn.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnEnable()
    {
        _createRoomBtn.onClick.AddListener(CreateRoomButton);
        _joinRoomBtn.onClick.AddListener(JoinRoomButton);
    }

    public override void OnDisable()
    {
        _createRoomBtn.onClick.RemoveAllListeners();
        _joinRoomBtn.onClick.RemoveAllListeners();
    }
    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Нет подключения!");
        }
        if (_inputField.text != null)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = _maxPlayers;
            PhotonNetwork.CreateRoom(_inputField.text, roomOptions,TypedLobby.Default);
        }
    }

    public void JoinRoomButton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Нет подключения!");
        }
        if (!_inputField.text.IsNullOrEmpty())
        {
            PhotonNetwork.JoinRoom(_inputField.text);
        }
    }


    public void ReadyButton()
    {

    }

    public void StartGameButton()
    {

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Connected to master {PhotonNetwork.IsMasterClient}");
        _createRoomBtn.interactable = true;
        _joinRoomBtn.interactable = true;
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected {cause}");
    }
    private void ToRoom()
    {
        _connectionRoomPanel.SetActive(false);
        _inRoomPanel.SetActive(true);
    }
}
