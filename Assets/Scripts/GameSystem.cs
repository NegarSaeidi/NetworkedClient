using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{


    GameObject submitButton, userNameInput, passwordInput, createToggle, loginToggle;

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
        }

        submitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        loginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        createToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
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
}
