using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;


public class AuthManager : MonoBehaviour
{
    public TextMeshProUGUI loginStatus; // 로그인 상태 텍스트
    public TMP_InputField emailField;  // 이메일 입력 필드
    public TMP_InputField passwordField; // 비밀번호 입력 필드
    public Button signInButton; // 로그인 버튼

    public TMP_InputField newEmailField; // 새 이메일 입력 필드
    public TMP_InputField newPasswordField; // 새 비밀번호 입력 필드

    public GameObject resetMailSentPopup; // 비밀번호 재설정 팝업
    private bool canOpenPopup = false; // 팝업 열림 여부 플래그

    Firebase.Auth.FirebaseAuth auth;
    private FirebaseUser user; // 현재 로그인된 사용자

    private bool isFirebaseReady = false; // Firebase 초기화 여부
    private bool isSignInOnProgress = false; // 로그인 진행 중 여부

    [SerializeField]
    FireStoreManager fireStoreManager;

    private void Start()
    {
        signInButton.interactable = false;

        // Firebase 초기화
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                isFirebaseReady = true;
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase is ready.");
            }
            else
            {
                Debug.LogError("Could not resolve Firebase dependencies: " + task.Result);
                isFirebaseReady = false;
            }

            signInButton.interactable = isFirebaseReady;
        });

        // 저장된 이메일과 비밀번호 자동 입력
        if (PlayerPrefs.HasKey("Email"))
            emailField.text = PlayerPrefs.GetString("Email");

        if (PlayerPrefs.HasKey("Password"))
            passwordField.text = PlayerPrefs.GetString("Password");
    }

    private void Update()
    {
        if (canOpenPopup)
        {
            resetMailSentPopup.SetActive(true);
            canOpenPopup = false;
        }
    }

    /// <summary>
    /// 회원가입
    /// </summary>
    public void Join()
    {
        if (!isFirebaseReady) return;

        string email = newEmailField.text.Trim();
        string password = newPasswordField.text.Trim();

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

        // Firebase user has been created.
        Firebase.Auth.AuthResult result = task.Result;
        Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            result.User.DisplayName, result.User.UserId);
        });
    }

    /// <summary>
    /// 로그인
    /// </summary>
    public void SignIn()
    {
        if (!isFirebaseReady || isSignInOnProgress) return;

        isSignInOnProgress = true;
        signInButton.interactable = false;

        string email = emailField.text.Trim();
        string password = passwordField.text.Trim();

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
                StatusManager.instance.uid=result.User.UserId;
                
                
                
            
            });
            StartCoroutine(LoadMainScene());
    }
    


    /// <summary>
    /// 비밀번호 재설정
    /// </summary>
    public void ResetPassword()
    {
        if (!isFirebaseReady) return;

        string email = emailField.text.Trim();

        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Password reset failed: " + task.Exception);
                loginStatus.text = "비밀번호 재설정 실패: 이메일을 확인하세요.";
                return;
            }

            Debug.Log($"Password reset email sent to: {email}");
            loginStatus.text = "비밀번호 재설정 이메일이 전송되었습니다.";
            canOpenPopup = true;
        });
    }

    /// <summary>
    /// 게스트 계정 로그인
    /// </summary>
    public void GuestLogin()
    {
        if (!isFirebaseReady || isSignInOnProgress) return;

        isSignInOnProgress = true;
        signInButton.interactable = false;

        string email = "guest@yourgame.com"; // 게스트 계정 이메일
        string password = "guestpassword"; // 게스트 계정 비밀번호

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            isSignInOnProgress = false;
            signInButton.interactable = true;

            if (task.IsFaulted)
            {
                Debug.LogError("Guest sign-in failed: " + task.Exception);
                loginStatus.text = "게스트 로그인 실패.";
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.Log($"Guest user signed in: {user.Email} ({user.UserId})");

            loginStatus.text = "게스트 로그인 성공!";
            StartCoroutine(LoadMainScene());
        });
    }

    /// <summary>
    /// 메인 씬 로드
    /// </summary>
    private IEnumerator LoadMainScene()
    {
        // 2초의 로딩 지연 (기존의 wait)
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log("gotoToilet");

        // uid가 null이 아닌지 1초마다 확인
        while (string.IsNullOrEmpty(StatusManager.instance.uid))
        {
            Debug.Log("UID is null, waiting...");
            yield return new WaitForSeconds(1f); // 1초 대기 후 다시 확인
        }

        // UID가 유효한 경우 FireStoreManager.Login() 호출
        Debug.Log("UID is valid. Calling FireStoreManager.Login() now.");
        fireStoreManager.Login();
        yield return new WaitForSeconds(2f);

        // 씬 전환
        SceneManager.LoadScene("Toilet");
    }
}
