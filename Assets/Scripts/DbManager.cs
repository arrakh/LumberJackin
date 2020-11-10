using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DbManager : MonoBehaviour
{
    public static string username;
    public static bool logIn { get { return username != null; } }
    public static void logOut()
    {
        username = null;
    }
}
