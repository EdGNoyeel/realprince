using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaveManager : MonoBehaviour
{
    public bool IsFirebaseReady { get; private set; }
    public bool IsSignInOnProgress { get; private set; }

    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public Button signInButton;
    [SerializeField]
    GameObject reallyLeave;
    [SerializeField]
    GameObject tryLeave;
    [SerializeField]
    GameObject confirmed;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;
    //public static FirebaseUser user;
    // Start is called before the first frame update
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        if (PlayerPrefs.HasKey("Email"))
        {
            emailField.text = PlayerPrefs.GetString("Email");
        }
        if (PlayerPrefs.HasKey("Password"))
        {
            passwordField.text = PlayerPrefs.GetString("Password");
        }
        signInButton.interactable = false;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(continuationAction: task =>
        {
            var result = task.Result;

            if (result != DependencyStatus.Available)
            {
                Debug.LogError(message: result.ToString());
                IsFirebaseReady = false;
            }
            else
            {
                IsFirebaseReady = true;

                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
            }

            signInButton.interactable = IsFirebaseReady;
        });
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    void OnDestroy()
    {
        //auth.StateChanged -= AuthStateChanged;
        //auth = null;
    }

    public void ReSignIn()
    {
        Firebase.Auth.FirebaseUser userNew = auth.CurrentUser;

        // Get auth credentials from the user for re-authentication. The example below shows
        // email and password credentials but there are multiple possible providers,
        // such as GoogleAuthProvider or FacebookAuthProvider.
        Firebase.Auth.Credential credential =
            Firebase.Auth.EmailAuthProvider.GetCredential(emailField.text, passwordField.text);

        if (userNew != null)
        {
            userNew.ReauthenticateAsync(credential).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("ReauthenticateAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("ReauthenticateAsync encountered an error: " + task.Exception);
                    return;
                }
                
                
            });
        }
        else
            Debug.Log(userNew);

        changePN();
    }
    void changePN()
    {
        reallyLeave.SetActive(true);
        Debug.Log("User reauthenticated successfully.");

        tryLeave.SetActive(false);
    }

    public void LeaveUs()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            user.DeleteAsync().ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("DeleteAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User deleted successfully.");
                
            });
        }
        confirmed.SetActive(true);
    }

    public void LoadOpenning()
    {
        //Debug.Log("로딩할때 하트:_" + StatusManager.instance.heartCount);
        SceneManager.LoadScene("Openning");
    }
}
