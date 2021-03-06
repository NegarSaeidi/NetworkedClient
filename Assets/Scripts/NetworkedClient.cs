using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedClient : MonoBehaviour
{

    int connectionID;
    int maxConnections = 1000;
    int reliableChannelID;
    int unreliableChannelID;
    int hostID;
    int socketPort = 5491;
    byte error;
    bool isConnected = false;
    int ourClientID;

    public bool gotReplied = false,wait=false,play=false;

    GameObject gameSystemObject;

    public string tempBuffer="here",playerTag="";
    public string shape = "..";
    public int PlayerId = -1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if(go.GetComponent<GameSystem>() != null)
            {
                gameSystemObject = go;
            }
        }

            Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SendMessageToHost(ClientToServerSignifiers.CreateAccount+ ","+ "Negar"+ ","+"abcd");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            SendMessageToHost(ClientToServerSignifiers.logInAccount + "," + "Negar" + "," + "abcd");
        }
        UpdateNetworkConnection();
    }

    private void UpdateNetworkConnection()
    {
        if (isConnected)
        {
            int recHostID;
            int recConnectionID;
            int recChannelID;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostID, out recConnectionID, out recChannelID, recBuffer, bufferSize, out dataSize, out error);

            switch (recNetworkEvent)
            {
                case NetworkEventType.ConnectEvent:
                    Debug.Log("connected.  " + recConnectionID);
                    ourClientID = recConnectionID;
                    break;
                case NetworkEventType.DataEvent:
                    string msg = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                    ProcessRecievedMsg(msg, recConnectionID);
                    //Debug.Log("got msg = " + msg);
                    break;
                case NetworkEventType.DisconnectEvent:
                    isConnected = false;
                    Debug.Log("disconnected.  " + recConnectionID);
                    break;
            }
        }
    }

    private void Connect()
    {

        if (!isConnected)
        {
            Debug.Log("Attempting to create connection");

            NetworkTransport.Init();

            ConnectionConfig config = new ConnectionConfig();
            reliableChannelID = config.AddChannel(QosType.Reliable);
            unreliableChannelID = config.AddChannel(QosType.Unreliable);
            HostTopology topology = new HostTopology(config, maxConnections);
            hostID = NetworkTransport.AddHost(topology, 0);
            Debug.Log("Socket open.  Host ID = " + hostID);

            connectionID = NetworkTransport.Connect(hostID, "192.168.1.63", socketPort, 0, out error); // server is local on network
        
            if (error == 0)
            {
                isConnected = true;

                Debug.Log("Connected, id = " + connectionID);

            }
        }
    }

    public void Disconnect()
    {
        NetworkTransport.Disconnect(hostID, connectionID, out error);
    }

    public void SendMessageToHost(string msg)
    {
        byte[] buffer = Encoding.Unicode.GetBytes(msg);
        NetworkTransport.Send(hostID, connectionID, reliableChannelID, buffer, msg.Length * sizeof(char), out error);
    }

    private void ProcessRecievedMsg(string msg, int id)
    {
      

        Debug.Log("msg recieved = " + msg + ".  connection id = " + id);
      string[] csv =  msg.Split(',');
        int signifier = int.Parse(csv[0]);
    
        if (signifier == ServerToCientSignifiers.CreateAccountFail)
        {
           
            Debug.Log("CreateAccountFailed");
        }
        else if(signifier ==ServerToCientSignifiers.CreateAccountSuccess)
        {
            gameSystemObject.GetComponent<GameSystem>().ChangeState(GameStates.MainMenu);
            Debug.Log("CreateAccountSuccess");
        }
        else if (signifier == ServerToCientSignifiers.logInFail)
        {

            Debug.Log("LoginFailed");

        }
        else if(signifier == ServerToCientSignifiers.logInSuccess)
        {
            gameSystemObject.GetComponent<GameSystem>().ChangeState(GameStates.MainMenu);
            Debug.Log("LoginSuccess");
        }
        else if (signifier == ServerToCientSignifiers.chatReply)
        {
         
            gotReplied = true;
            tempBuffer = "Player " + csv[2] + " : " + csv[1] + "\n";
        }
        else if (signifier == ServerToCientSignifiers.chatStart)
        {
            gameSystemObject.GetComponent<GameSystem>().ChangeState(GameStates.chatRoom);
          

        }
        else if (signifier == ServerToCientSignifiers.GameStart)
        {
            if (int.Parse(csv[1]) == 1)
                shape = "circle";
            else
                shape = "cross";

            gameSystemObject.GetComponent<GameSystem>().ChangeState(GameStates.tictactoe);
      

           

        }
        else if (signifier == ServerToCientSignifiers.OpponentPlay)
        {
            play = true;
            PlayerId = int.Parse(csv[1]);
            wait = false;
            playerTag = csv[2];
        }
        else if (signifier == ServerToCientSignifiers.PlayerWait)
        {
            PlayerId = int.Parse(csv[1]);
            play = false;
            wait = true;
        }




    }

    public bool IsConnected()
    {
        return isConnected;
    }


}
static public class ClientToServerSignifiers
{
    public const int CreateAccount = 1;
  public   const int logInAccount = 2;
    public const int JoinGameRoomQueue = 3;
    public const int TicTacToe = 4;
    public const int chat = 5;
    public const int Playing = 6;
}
static public class ServerToCientSignifiers
{
    public const int CreateAccountFail = 1;
    public const int logInFail = 2;

    public const int CreateAccountSuccess = 3;
    public const int logInSuccess = 4;
    public const int OpponentPlay = 5;
    public const int GameStart = 6;
    public const int chatReply = 7;
    public const int chatStart = 8;
    public const int PlayerWait = 9;
}