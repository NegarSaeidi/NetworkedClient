using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToePlay : MonoBehaviour
{
    SpriteRenderer spriteRendererObj;
    public Sprite[] image;
    public Sprite playerIcon,OpponentIcon;
    public bool AlreadyPlayed = false;
    GameObject networkedClient;
   public  GameObject[] adjacents;
    public GameObject turn,deactivate;
    

    private void Awake()
    {
        spriteRendererObj = GetComponent<SpriteRenderer>();


    }
    private void Start()
    {
       
        spriteRendererObj.sprite = null;
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
       
        foreach (GameObject go in allObjects)
        {

            if (go.name == "NetworkedClient")
                networkedClient = go;
        }
        if (networkedClient.GetComponent<NetworkedClient>().shape == "circle")
        {
            playerIcon = image[0];
            OpponentIcon = image[1];
        }
        else
        {
            OpponentIcon = image[0];
            playerIcon = image[1];
        }



    }
  
    private void OnMouseDown()
    {
        if (spriteRendererObj.sprite==null)
        {
            string msg = ClientToServerSignifiers.Playing + "," + gameObject.tag;
            networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
           
            

            AlreadyPlayed = true;
      
        }
    }
    private void Update()
    {
        if (AlreadyPlayed)
            spriteRendererObj.sprite = playerIcon;
    }
    public void ResetGame()
    {
        AlreadyPlayed = false;
        spriteRendererObj.sprite = null;
    }


}
