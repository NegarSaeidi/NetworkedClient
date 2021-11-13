using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateCells : MonoBehaviour
{
    GameObject networkedClient,tictactoePlay;
    public GameObject[] adjacents;
    public GameObject turn, deactivate;
    public GameObject TL, TM, TR, ML, MM, MR, DL, DM, DR;
    public GameObject win,replayButton ,playButton;
    // Start is called before the first frame update
    void Start()
    {
        deactivate.SetActive(false);
        win.SetActive(false);
       
       // turn.GetComponent<Text>().text = "Your Turn!";
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {

            if (go.name == "NetworkedClient")
                networkedClient = go;
            else if (go.name == "TR")
                tictactoePlay = go;
            else if (go.name == "Replay")
                replayButton= go;
         
        }
        replayButton.SetActive(false);
        playButton.SetActive(false);
        replayButton.GetComponent<Button>().onClick.AddListener(replayButtonPressed);

    }
    public void replayButtonPressed()
    {
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToe + "");
        for (int i = 0; i < adjacents.Length; i++)
        {
            adjacents[i].GetComponent<SpriteRenderer>().sprite = null;
            adjacents[i].GetComponent<TicTacToePlay>().ResetGame();
        }
        ResetGame();
    }
    private void ResetGame()
    {
        deactivate.SetActive(false);
        win.SetActive(false);
        replayButton.SetActive(false);
        playButton.SetActive(false);
       
        TL.GetComponent<SpriteRenderer>().sprite = null;
        TM.GetComponent<SpriteRenderer>().sprite = null;
        TR.GetComponent<SpriteRenderer>().sprite = null;
        DL.GetComponent<SpriteRenderer>().sprite = null;
        DM.GetComponent<SpriteRenderer>().sprite = null;
        DR.GetComponent<SpriteRenderer>().sprite = null;
        ML.GetComponent<SpriteRenderer>().sprite = null;
        MM.GetComponent<SpriteRenderer>().sprite = null;
        MR.GetComponent<SpriteRenderer>().sprite = null;

        TL.GetComponent<ReplayController>().resetGame();
        TM.GetComponent<ReplayController>().resetGame();
        TR.GetComponent<ReplayController>().resetGame();
        ML.GetComponent<ReplayController>().resetGame();
        MM.GetComponent<ReplayController>().resetGame();
        MR.GetComponent<ReplayController>().resetGame();
        DL.GetComponent<ReplayController>().resetGame();
        DM.GetComponent<ReplayController>().resetGame();
        DR.GetComponent<ReplayController>().resetGame();

        networkedClient.GetComponent<NetworkedClient>().wait = false;
        networkedClient.GetComponent<NetworkedClient>().play = false;
        networkedClient.GetComponent<NetworkedClient>().playerTag = "";
    }
        // Update is called once per frame
        void Update()
    {
        checkWinning();
        if (networkedClient.GetComponent<NetworkedClient>().wait)
        {
           
           
            deactivate.SetActive(true);

        }
        else if ((networkedClient.GetComponent<NetworkedClient>().play))
        {
         
            deactivate.SetActive(false);

            for (int i = 0; i < adjacents.Length; i++)
            {
                if (adjacents[i].tag == networkedClient.GetComponent<NetworkedClient>().playerTag)
                {


                    adjacents[i].GetComponent<SpriteRenderer>().sprite = tictactoePlay.GetComponent<TicTacToePlay>().OpponentIcon;
                }
            }
            // networkedClient.GetComponent<NetworkedClient>().playerTag = "";
            //turn.GetComponent<Text>().text = "Your Turn!";


        }
    }
    private void checkWinning()
    {

        if ((TL.GetComponent<SpriteRenderer>().sprite != null) && (TM.GetComponent<SpriteRenderer>().sprite != null) && (TR.GetComponent<SpriteRenderer>().sprite != null))
            if ((TL.GetComponent<SpriteRenderer>().sprite.name == TM.GetComponent<SpriteRenderer>().sprite.name) && (TM.GetComponent<SpriteRenderer>().sprite.name == TR.GetComponent<SpriteRenderer>().sprite.name))
            {

                if (TL.GetComponent<SpriteRenderer>().sprite.name == "cross")
                    win.GetComponent<Text>().text = "X WON!";
                else
                    win.GetComponent<Text>().text = "O WON!";
                win.SetActive(true);
                playButton.SetActive(true);
                replayButton.SetActive(true);
            }
        if ((ML.GetComponent<SpriteRenderer>().sprite != null) && (MM.GetComponent<SpriteRenderer>().sprite != null) && (MR.GetComponent<SpriteRenderer>().sprite != null))
            if ((ML.GetComponent<SpriteRenderer>().sprite.name == MM.GetComponent<SpriteRenderer>().sprite.name) && (MM.GetComponent<SpriteRenderer>().sprite.name == MR.GetComponent<SpriteRenderer>().sprite.name))
            {
                if (ML.GetComponent<SpriteRenderer>().sprite.name == "cross")
                    win.GetComponent<Text>().text = "X WON!";
                else
                    win.GetComponent<Text>().text = "O WON!";
                win.SetActive(true);
                playButton.SetActive(true);
                replayButton.SetActive(true);
            }
        if ((DL.GetComponent<SpriteRenderer>().sprite != null) && (DM.GetComponent<SpriteRenderer>().sprite != null) && (DR.GetComponent<SpriteRenderer>().sprite != null))
            if ((DL.GetComponent<SpriteRenderer>().sprite.name == DM.GetComponent<SpriteRenderer>().sprite.name) && (DM.GetComponent<SpriteRenderer>().sprite.name == DR.GetComponent<SpriteRenderer>().sprite.name))
            {
                if (DL.GetComponent<SpriteRenderer>().sprite.name == "cross")
                    win.GetComponent<Text>().text = "X WON!";
                else
                    win.GetComponent<Text>().text = "O WON!";
                win.SetActive(true);
                playButton.SetActive(true);
                replayButton.SetActive(true);
            }

        if ((TL.GetComponent<SpriteRenderer>().sprite != null) && (ML.GetComponent<SpriteRenderer>().sprite != null) && (DL.GetComponent<SpriteRenderer>().sprite != null))
            if ((TL.GetComponent<SpriteRenderer>().sprite.name == ML.GetComponent<SpriteRenderer>().sprite.name) && (ML.GetComponent<SpriteRenderer>().sprite.name == DL.GetComponent<SpriteRenderer>().sprite.name))
            {
                if (TL.GetComponent<SpriteRenderer>().sprite.name == "cross")
                    win.GetComponent<Text>().text = "X WON!";
                else
                    win.GetComponent<Text>().text = "O WON!";
                win.SetActive(true);
                playButton.SetActive(true);
                replayButton.SetActive(true);
            }
        if ((TM.GetComponent<SpriteRenderer>().sprite != null) && (MM.GetComponent<SpriteRenderer>().sprite != null) && (DM.GetComponent<SpriteRenderer>().sprite != null))
            if ((TM.GetComponent<SpriteRenderer>().sprite.name == MM.GetComponent<SpriteRenderer>().sprite.name) && (MM.GetComponent<SpriteRenderer>().sprite.name == DM.GetComponent<SpriteRenderer>().sprite.name))
            {

                if (TM.GetComponent<SpriteRenderer>().sprite.name == "cross")
                    win.GetComponent<Text>().text = "X WON!";
                else
                {
                    win.GetComponent<Text>().text = "O WON!";

                }
                win.SetActive(true);
                playButton.SetActive(true);
                replayButton.SetActive(true);

            }
        if ((TR.GetComponent<SpriteRenderer>().sprite != null) && (MR.GetComponent<SpriteRenderer>().sprite != null) && (DR.GetComponent<SpriteRenderer>().sprite != null))
            if ((TR.GetComponent<SpriteRenderer>().sprite.name == MR.GetComponent<SpriteRenderer>().sprite.name) && (MR.GetComponent<SpriteRenderer>().sprite.name == DR.GetComponent<SpriteRenderer>().sprite.name))
            {
                Debug.Log("3c");
                if (TR.GetComponent<SpriteRenderer>().sprite.name == "cross")
                    win.GetComponent<Text>().text = "X WON!";
                else
                    win.GetComponent<Text>().text = "O WON!";
                win.SetActive(true);
                playButton.SetActive(true);
                replayButton.SetActive(true);
            }

        if ((TL.GetComponent<SpriteRenderer>().sprite != null) && (MM.GetComponent<SpriteRenderer>().sprite != null) && (DR.GetComponent<SpriteRenderer>().sprite != null))
            if ((TL.GetComponent<SpriteRenderer>().sprite.name == MM.GetComponent<SpriteRenderer>().sprite.name) && (MM.GetComponent<SpriteRenderer>().sprite.name == DR.GetComponent<SpriteRenderer>().sprite.name))
            {
                if (TL.GetComponent<SpriteRenderer>().sprite.name == "cross")
                    win.GetComponent<Text>().text = "X WON!";
                else
                    win.GetComponent<Text>().text = "O WON!";
                win.SetActive(true);
                playButton.SetActive(true);
                replayButton.SetActive(true);
            }
        if ((TR.GetComponent<SpriteRenderer>().sprite != null) && (MM.GetComponent<SpriteRenderer>().sprite != null) && (DL.GetComponent<SpriteRenderer>().sprite != null))
            if ((TR.GetComponent<SpriteRenderer>().sprite.name == MM.GetComponent<SpriteRenderer>().sprite.name) && (MM.GetComponent<SpriteRenderer>().sprite.name == DL.GetComponent<SpriteRenderer>().sprite.name))
            {
                if (TR.GetComponent<SpriteRenderer>().sprite.name == "cross")
                    win.GetComponent<Text>().text = "X WON!";
                else
                    win.GetComponent<Text>().text = "O WON!";
                win.SetActive(true);
                playButton.SetActive(true);
                replayButton.SetActive(true);
            }


    }
}
