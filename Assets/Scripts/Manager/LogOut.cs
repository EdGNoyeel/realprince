using System.Collections;
using System.Collections.Generic;
//using Firebase;
//using Firebase.Auth;
//using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour
{
    //public static FirebaseAuth firebaseAuth;
    //public static FirebaseUser user;

    public void Out()
    {
        //firebaseAuth = FirebaseAuth.DefaultInstance;
        //firebaseAuth.SignOut();
        SceneManager.LoadScene("Openning");
    }
}
