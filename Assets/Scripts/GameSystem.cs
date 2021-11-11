using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{


    GameObject submitButton, joinRoomButton, userNameInput, passwordInput, createToggle, loginToggle,UsernameLabel, PasswordLabel, playTicTacToe, chatPage, chatInput, chatBox,sendChat,tictactoePanel;
    bool inRoom = false, receivedMsg=true;
    GameObject networkedClient;
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.name == "Username")
                userNameInput = go;
            else if (go.name == "Password")
                passwordInput = go;
            else if (go.name == "Submit")
                submitButton = go;
            else if (go.name == "LoginToggle")
                loginToggle = go;
            else if (go.name == "CreateToggle")
                createToggle = go;
            else if (go.name == "NetworkedClient")
                networkedClient = go;
            else if (go.name == "JoinRoom")
                joinRoomButton = go;
            else if (go.name == "UsernameLabel")
                UsernameLabel = go;
            else if (go.name == "PasswordLabel")
                PasswordLabel= go;
            else if (go.name == "PlayTicTacToe")
                playTicTacToe = go;
            else if (go.name == "ChatPage")
                chatPage = go;
            else if (go.name == "ChatBox")
                chatBox = go;
            else if (go.name == "ChatInput")
                chatInput = go;
            else if (go.name == "SendChat")
                sendChat = go;
            else if (go.name == "Tictactoe")
                tictactoePanel= go;

        }

        submitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        sendChat.GetComponent<Button>().onClick.AddListener(SendChatPressed);
        loginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        createToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
        joinRoomButton.GetComponent<Button>().onClick.AddListener(JoinRoomPressed);
        playTicTacToe.GetComponent<Button>().onClick.AddListener(PlayTicTacToePressed);
        

       
        ChangeState(GameStates.LoginMenu);

    }
    private void Update()
    {
        if (inRoom)
        {
           
            if (networkedClient.GetComponent<NetworkedClient>().gotReplied)
            {

             
                networkedClient.GetComponent<NetworkedClient>().gotReplied = false;
                chatBox.GetComponent<Text>().text += networkedClient.GetComponent<NetworkedClient>().tempBuffer;
               
            }
        }

    }
    public void SendChatPressed()
    {
        Debug.LogWarning(chatInput.GetComponent<InputField>().GetComponentInChildren<Text>().text);
        string msg = ClientToServerSignifiers.chat + "," + chatInput.GetComponent<InputField>().GetComponentInChildren<Text>().text;
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
      
        chatInput.GetComponent<InputField>().text = string.Empty;

    }
    public void gotResponse()
    {

    }
    public void JoinRoomPressed()
    {
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.JoinGameRoomQueue+"");
        ChangeState(GameStates.waitingInQueue);
    }
    public void PlayTicTacToePressed()
    {
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToe + "");
       
    }
    public void SubmitButtonPressed()
    {
        string u = userNameInput.GetComponent<InputField>().text;
        string p = passwordInput.GetComponent<InputField>().text;

        string msg;

        if (createToggle.GetComponent<Toggle>().isOn)
            msg = ClientToServerSignifiers.CreateAccount + "," + u + "," + p;
        else
            msg = ClientToServerSignifiers.logInAccount + "," + u + "," + p;
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        
    }

    public void LoginToggleChanged(bool newValue)
    {
        createToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
       
    }

    public void CreateToggleChanged(bool newValue)
    {
        loginToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
       
    }

    public void ChangeState(int newState)
    {
        submitButton.SetActive(false);
        joinRoomButton.SetActive(false);
        userNameInput.SetActive(false);
        passwordInput.SetActive(false);
        createToggle.SetActive(false);
        loginToggle.SetActive(false);
        UsernameLabel.SetActive(false);
        PasswordLabel.SetActive(false);
        playTicTacToe.SetActive(false);
        chatPage.SetActive(false);
        tictactoePanel.SetActive(false);
        inRoom = false;

        if (newState == GameStates.LoginMenu)
        {
            submitButton.SetActive(true);
            userNameInput.SetActive(true);
            passwordInput.SetActive(true);
            createToggle.SetActive(true);
            loginToggle.SetActive(true);
            UsernameLabel.SetActive(true);
            PasswordLabel.SetActive(true);
        }
        else if (newState == GameStates.waitingInQueue)
        {
            joinRoomButton.SetActive(true);
        }
        else if (newState == GameStates.MainMenu)
        {
            joinRoomButton.SetActive(true);
        }
        else if(newState == GameStates.chatRoom)
        {
            playTicTacToe.SetActive(true);
            chatPage.SetActive(true);
            inRoom = true;
      
        }
        else if (newState == GameStates.tictactoe)
        {
          
            tictactoePanel.SetActive(true);
        }
    }
}
static public class GameStates
{
    public const int LoginMenu = 1;
    public const int MainMenu = 2;
    public const int waitingInQueue = 3;
    public const int chatRoom = 4;
    public const int tictactoe = 5;
}
