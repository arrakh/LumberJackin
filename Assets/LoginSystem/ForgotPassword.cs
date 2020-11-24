using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ForgotPassword : MonoBehaviour
{
    public InputField kode_verif, ganti_pass;
    public Button cek, ganti;
    public Text message;
    
    public void LupaPass()
    {
        StartCoroutine(Lupa());
    }

    IEnumerator Lupa()
    {
        WWWForm form = new WWWForm();
        form.AddField("kode", kode_verif.text);

        WWW www = new WWW("http://localhost/LumberJackin/CheckCode.php", form);
        yield return www;

        if (www.isDone)
        {
            if (www.text.Contains("error"))
            {
                message.text = "Kode Salah";
            }
            else
            {
                message.text = "Masukkan Kode Baru Anda";

                ganti_pass.gameObject.SetActive(true);
                ganti.gameObject.SetActive(true);
                kode_verif.gameObject.SetActive(false);
                cek.gameObject.SetActive(false);
            }
        }

        Debug.Log(www.text);
    }

    public void ResetPass()
    {
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        WWWForm form = new WWWForm();
        form.AddField("newPass", ganti_pass.text);
        form.AddField("kode", kode_verif.text);

        WWW www = new WWW("http://localhost/LumberJackin/NewPassword.php", form);
        yield return www;

        if (www.isDone)
        {
            SceneManager.LoadScene("Login");
        }
    }
}
