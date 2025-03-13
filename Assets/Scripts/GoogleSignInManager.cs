using Firebase.Auth;
using UnityEngine;

public class GoogleSignInManager : MonoBehaviour
{
    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignInWithGoogle(string googleIdToken, string googleAccessToken)
    {
        // Firebase 인증 처리
        Firebase.Auth.Credential credential =
            Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);

        auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
        {
            if (authTask.IsCanceled || authTask.IsFaulted)
            {
                Debug.LogError("Firebase Auth Failed: " + authTask.Exception);
                return;
            }

            FirebaseUser newUser = authTask.Result;
            Debug.Log($"✅ User signed in successfully: {newUser.DisplayName} ({newUser.UserId})");
        });
    }
}