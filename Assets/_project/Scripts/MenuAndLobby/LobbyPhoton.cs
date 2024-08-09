using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class LobbyPhoton : MonoBehaviourPunCallbacks, IPunObservable
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

    private bool _isReady;
    private int _readyPlayers;
    private PhotonView _photonView;
    private void Start()
    {
        PhotonNetwork.Disconnect();
        if(!PhotonNetwork.IsConnected)
        {
            _createRoomBtn.interactable = false;
            _joinRoomBtn.interactable = false;
        }
        PhotonNetwork.ConnectUsingSettings();
        _photonView = GetComponentInChildren<PhotonView>();
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
        _joinRoomBtn.onClick.AddListener(JoinRoomButton);
        _readyBtn.onClick.AddListener(ReadyButton);
        _startGameBtn.onClick.AddListener(StartGameButton);

    }
    public override void OnDisable()
    {
        base.OnDisable();
        _createRoomBtn.onClick.RemoveAllListeners();
        _joinRoomBtn.onClick.RemoveAllListeners();
        _readyBtn.onClick.RemoveAllListeners();
        _startGameBtn.onClick.RemoveAllListeners();
    }
    public void ReadyButton()
    {
        _isReady = !_isReady;
        if (_isReady)
        {
            _readyPlayers++;
        }
        else
        {
            _readyPlayers--;
        }
        _photonView.RPC(nameof(SendData), RpcTarget.AllBuffered, _readyPlayers );
    }

    public void StartGameButton()
    {
        _photonView.RPC(nameof(RunGame), RpcTarget.All);
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
        if (_isReady)
        {
            _readyPlayers--;
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        ToRoom();
    }
    private void ToRoom()
    {
        _connectionRoomPanel.SetActive(false);
        _inRoomPanel.SetActive(true);
    }
    [PunRPC]
    private void SendData(int readyPlayers)
    {
        _readyPlayers = readyPlayers;
        _connectedText.text = $"Подключились {readyPlayers}/{_maxPlayers}";
        if (_readyPlayers == _maxPlayers)
        {
            _startGameBtn.interactable = true;
        }
    }
    [PunRPC]
    private void RunGame()
    {
        PhotonNetwork.LoadLevel(2);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
