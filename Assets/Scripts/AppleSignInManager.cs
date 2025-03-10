using System;
using System.Text;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;

public class AppleSignInManager : MonoBehaviour
{
    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    // public void SignInWithApple()
    // {
    //     string rawNonce = GenerateNonce();
    //     string hashedNonce = HashNonce(rawNonce);

    //     // iOS에서 Apple 로그인 호출
    //     AppleAuthManager.Instance.LoginWithAppleId(rawNonce, hashedNonce, (credential) =>
    //     {
    //         if (credential == null)
    //         {
    //             Debug.LogError("Apple Sign-In Failed");
    //             return;
    //         }

    //         string appleIdToken = credential.IdentityToken;

    //         if (!string.IsNullOrEmpty(appleIdToken))
    //         {
    //             SignInWithFirebase(appleIdToken, rawNonce);
    //         }
    //         else
    //         {
    //             Debug.LogError("Apple ID Token is null or empty.");
    //         }
    //     });
    // }

    // Firebase에 Apple 토큰 전달
    private void SignInWithFirebase(string appleIdToken, string rawNonce)
    {
        Firebase.Auth.Credential credential =
            Firebase.Auth.OAuthProvider.GetCredential("apple.com", appleIdToken, rawNonce, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Firebase Auth Failed: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log($"✅ Apple Sign-In Successful: {newUser.DisplayName} ({newUser.UserId})");
        });
    }

    // Nonce 생성 메서드
    private string GenerateNonce()
    {
        const int length = 32;
        var random = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(random);
        }
        return Convert.ToBase64String(random)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    // Nonce 해싱 메서드
    private string HashNonce(string nonce)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] nonceBytes = Encoding.UTF8.GetBytes(nonce);
            byte[] hashBytes = sha256.ComputeHash(nonceBytes);
            return Convert.ToBase64String(hashBytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}