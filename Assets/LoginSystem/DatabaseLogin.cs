using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DatabaseLogin : MonoBehaviour
{
    public TMP_InputField username, pass, email;
    public TMP_Text errorM;

    public void LoginScene()
    {
        SceneManager.LoadScene("Login");
    }

    public void RegisScene()
    {
        SceneManager.LoadScene("Regis");
    }

    public void CallRegister()
    {
        if (username.text == "" || pass.text == "" || email.text == "")
        {
            errorM.text = "Isi DATA secara lengkap !";
        }

        if (!email.text.Contains("@"))
        {
            errorM.text = "Gunakan Email yang Valid";
        }
        else
        {
            StartCoroutine(Register());
        }
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", pass.text);
        form.AddField("email", email.text);

        WWW www = new WWW("http://localhost/LumberJackin/Register.php", form);
        yield return www;

        SceneManager.LoadScene("Login");
        Debug.Log(www.text);
    }

    public void CallLogin()
    {
        if (username.text == "" || pass.text == "")
        {
            errorM.text = "Username atau Password masih kosong";
        }
        else
        {
            StartCoroutine(Login());
        }   
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", pass.text);

        WWW www = new WWW("http://localhost/LumberJackin/Login.php", form);
        yield return www;

        if (www.isDone)
        {
            if (www.text.Contains("error"))
            {
                errorM.text = "Username atau Password salah";

            }
            else
            {
                SceneManager.LoadScene("S_Menu");
            }
        }

        Debug.Log(www.text);
    }

    public void CallForgotPass()
    {
        if (username.text == "")
        {
            errorM.text = "Masukkan Username terlebih dahulu";
        }
        else
        {
            StartCoroutine(ForgotPass());
        }
    }

    IEnumerator ForgotPass()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);

        WWW www = new WWW("http://localhost/LumberJackin/ForgotPass.php", form);
        yield return www;

        if (www.isDone)
        {
            SceneManager.LoadScene("Reset Pass");
        }

        Debug.Log(www.text);
    }
}
