using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseLogin : MonoBehaviour
{
    public InputField username, pass;
    public Button submit;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", pass.text);

        WWW www = new WWW("http://localhost/LumberJackin/Register.php", form);
        yield return www;

        Debug.Log(www.text);
    }

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", pass.text);

        WWW www = new WWW("http://localhost/LumberJackin/Login.php", form);
        yield return www;

        Debug.Log(www.text);

        if (DbManager.logIn)
        {
            Debug.Log("Player = " + DbManager.username);
        }
    }
}
