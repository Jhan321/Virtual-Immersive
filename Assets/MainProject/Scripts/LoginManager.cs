using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SpaceBear.VRUI;
using ReadyPlayerMe;

public class LoginManager : MonoBehaviour
{
    public InputField userField;
    public InputField passField;
    public GameObject panelWait;
    public GameObject panelAllert;
    public VRUIKeyboard keyboard;
    public VRUICheckbox checkBox;
    public Text notice;

    string nameAvatar;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Checkbox"))
        {
            userField.text = PlayerPrefs.GetString("UserName");
            passField.text = PlayerPrefs.GetString("Password");
            checkBox.isOn = true;
        }
    }

    public void SelectKeyBoard(int key)
    {
        if(key == 1)
        {
            keyboard.inputField = userField;
        }
        if (key == 2)
        {
            keyboard.inputField = passField;
        }
    }

    public void Login()
    {
        panelWait.SetActive(true);
        StartCoroutine(ConnectToPhotonServer());
    }

    public IEnumerator ConnectToPhotonServer()
    {
        if(userField.text == "" || passField.text == "")
        {
            panelWait.SetActive(false);
            panelAllert.SetActive(true);
            StartCoroutine(DisabledAlert());
            notice.text = "check the data!";
        }
        else
        {
            string user = userField.text;
            string pass = passField.text;

            string uri = "https://pentagrama.io/CellApp/login.php";
            WWWForm form = new WWWForm();
            form.AddField("user", user);
            form.AddField("password", pass);


            UnityWebRequest www = UnityWebRequest.Post(uri, form);

            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                notice.text = www.error;
                panelAllert.SetActive(true);
                panelWait.SetActive(false);
                StartCoroutine(DisabledAlert());

            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                notice.text = www.downloadHandler.text;
                panelAllert.SetActive(true);
                panelWait.SetActive(false);
                StartCoroutine(DisabledAlert());

            }

            if (notice.text != "correcto")
            {
                notice.text = "The user name or password is incorrect";
                panelAllert.SetActive(true);
                panelWait.SetActive(false);
                StartCoroutine(DisabledAlert());

            }
            else
            {
                PlayerPrefs.SetString("user", user);
                StartCoroutine(LoadAvatar());
                if(checkBox.isOn)
                {
                    PlayerPrefs.SetString("UserName", userField.text);
                    PlayerPrefs.SetString("Password", passField.text);
                    PlayerPrefs.SetString("Checkbox", "true");
                    PlayerPrefs.Save();
                }
                else if(!checkBox.isOn)
                {
                    PlayerPrefs.DeleteKey("UserName");
                    PlayerPrefs.DeleteKey("Password");
                    PlayerPrefs.DeleteKey("Checkbox");
                }
            }
        }
    }
    IEnumerator DisabledAlert()
    {
        yield return new WaitForSeconds(5);
        if(panelAllert.activeSelf)
        {
            panelAllert.SetActive(false);
        }
    }

    IEnumerator LoadAvatar()
    {
        string user = PlayerPrefs.GetString("user");

        string uri = "https://pentagrama.io/CellApp/userurl.php";

        WWWForm form = new WWWForm();
        form.AddField("user", user);
        UnityWebRequest www = UnityWebRequest.Post(uri, form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
            notice.text = www.error;
            yield return new WaitForSeconds(5);
            notice.text = "";
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            PlayerPrefs.SetString("avatar", www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
            PlayerPrefs.Save();

            LoadNewScene loadNewScene = GetComponent<LoadNewScene>();
            loadNewScene.ButtonLoadScene("Lobby");
        }
    }
}
