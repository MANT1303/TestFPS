using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
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
        PhotonNetwork.Disconnect();
        if(!PhotonNetwork.IsConnected)
        {
            _createRoomBtn.interactable = false;
            _joinRoomBtn.interactable = false;
        }
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Нет подключения!");
        }
        if (!_inputField.text.IsNullOrEmpty())
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = _maxPlayers;
            PhotonNetwork.CreateRoom(_inputField.text, roomOptions, TypedLobby.Default);
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

    public override void OnEnable()
    {
        base.OnEnable();
        _createRoomBtn.onClick.AddListener(CreateRoomButton);

    }
    public override void OnDisable()
    {
        base.OnDisable();
        _createRoomBtn.onClick.RemoveAllListeners();
    }
    public void ReadyButton()
    {

    }

    public void StartGameButton()
    {

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Connected to master {PhotonNetwork.CloudRegion}");
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
