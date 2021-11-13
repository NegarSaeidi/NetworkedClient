using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReplayController : MonoBehaviour
{
    private bool isInReplay=false;
    private List<SaveRecords> records;
    private TicTacToePlay tictactoePlay;
    private float currentFrameIndex;
    public Sprite[] icons;
    public GameObject playButton;
    private bool play = false;
    private Sprite playerInitialIcon,OpponentInitialIcon;
    GameObject networkedClient;
    public Sprite[] image;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {

            if (go.name == "NetworkedClient")
                networkedClient = go;
        }
        if (networkedClient.GetComponent<NetworkedClient>().shape == "circle")
        {
            playerInitialIcon = image[0];
            OpponentInitialIcon = image[1];
        }
        else
        {
            OpponentInitialIcon = image[0];
            playerInitialIcon = image[1];
        }
        tictactoePlay = GetComponent<TicTacToePlay>();
        records = new List<SaveRecords>();
        playButton.GetComponent<Button>().onClick.AddListener(playButtonPressed);
    
    }
    private void playButtonPressed()
    {
        play = true;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (play)
        {
            play = !play;
            isInReplay = !isInReplay;
            if (isInReplay)
            {

                tictactoePlay.AlreadyPlayed = false;
                GetComponent<SpriteRenderer>().sprite = null;
                goBackToFrame(0);

            }
            else
            {
                goBackToFrame(records.Count - 1);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isInReplay)
        {
            if (GetComponent<SpriteRenderer>().sprite != null)
            {
                records.Add(new SaveRecords { SpriteName = GetComponent<SpriteRenderer>().sprite.name });

            }
            else
                records.Add(new SaveRecords { SpriteName = "" });

        }
        else
        {
            float nextFrame = currentFrameIndex + 1f;
            if (nextFrame < records.Count)
            {
                goBackToFrame(nextFrame);
            }
        }

    }
    private void goBackToFrame(float frame)
    {
        
        currentFrameIndex = frame;
        SaveRecords tempRecord = records[(int)frame];
        Debug.Log((int)frame+ "  " + records[(int)frame].SpriteName);
        if (tempRecord.SpriteName != "")
            {
           
            if (playerInitialIcon.name == tempRecord.SpriteName)
            {
                
                GetComponent<SpriteRenderer>().sprite = playerInitialIcon;
            }
            else
            {
                
                GetComponent<SpriteRenderer>().sprite = OpponentInitialIcon;
            }
            //tictactoePlay.AlreadyPlayed = true;
           

        }
            else
            {
           
            tictactoePlay.AlreadyPlayed = false;
                GetComponent<SpriteRenderer>().sprite = null;
            }
        
      

    }
    public void resetGame()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        play = false;

    }
}
