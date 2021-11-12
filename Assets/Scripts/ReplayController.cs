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
    // Start is called before the first frame update
    void Start()
    {
        tictactoePlay = GetComponent<TicTacToePlay>();
        records = new List<SaveRecords>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            
            isInReplay = !isInReplay;
            if(isInReplay)
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
            Debug.Log(records.Count+ "  " + records[records.Count-1].SpriteName);
        }
        else
        {
            float nextFrame = currentFrameIndex+1f;
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

            if(tempRecord.SpriteName=="cross")
                GetComponent<SpriteRenderer>().sprite = icons[0];
            else
                GetComponent<SpriteRenderer>().sprite = icons[1];
            tictactoePlay.AlreadyPlayed = true;
           

        }
            else
            {
            Debug.Log("nullt");
            tictactoePlay.AlreadyPlayed = false;
                GetComponent<SpriteRenderer>().sprite = null;
            }
        
      

    }
}
