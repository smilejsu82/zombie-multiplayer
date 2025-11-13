using UnityEngine;
using UnityEngine.UI;

public class LobbyMain : MonoBehaviour
{
    public UINicknameView nicknameView;
    public UILoading uiLoading;
    public UIRoomScrollview uiRoomScrollview;
    public Button createRoomButton;
    void Start()
    {
        uiRoomScrollview.Init();
        AddEventListeners();
        
        ConnectToMasterServer();
        
        createRoomButton.onClick.AddListener(() =>
        {
            Pun2Manager.instance.CreateRoom();
        });
        
        nicknameView.onClickSubmit = (nickname) =>
        {
            if (string.IsNullOrEmpty(nickname))
            {
                Debug.Log("nickname is empty");
            }
            else
            {
                Debug.Log($"nickname: {nickname}");
                JoinLobby(nickname);
            }
        };
    }

    private void JoinLobby(string nickname)
    {
        uiLoading.Show();
        Pun2Manager.instance.JoinLobby(nickname);
    }

    private void ConnectToMasterServer()
    {
        uiLoading.Show();
        Pun2Manager.instance.Init();   //마스터 서버 접속
    }

    private void AddEventListeners()
    {
        EventDispatcher.instance.AddEventHandler((int)EventEnums.EventType.OnConnectedToMaster, (eventType) =>
        {
            Debug.Log($"AddEventListeners: {(EventEnums.EventType)eventType}");
            uiLoading.Hide();
            nicknameView.gameObject.SetActive(true);
        });
        
        EventDispatcher.instance.AddEventHandler((int)EventEnums.EventType.OnJoinedLobby, (eventType) =>
        {
            Debug.Log($"AddEventListeners: {(EventEnums.EventType)eventType}");
            uiLoading.Hide();
            nicknameView.gameObject.SetActive(false);
            
            //룸 리스트 보여주고 , 방 만들기 버튼 보여주기 
            uiRoomScrollview.Show();
            createRoomButton.gameObject.SetActive(true);
        });
    }
}
