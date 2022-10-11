using Newtonsoft.Json;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Auth : MonoBehaviour
{
    [Header("Login")]
    public GameObject loginPanel;
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;

    [Header("SignUp")]
    public GameObject signUpPanel;
    public TMP_InputField signUpUserName;
    public TMP_InputField signUpEmail;
    public TMP_InputField signUpPassword;

    public void ShowLoginScreen()
    {
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);
	}

    public void ShowSignUpScreen()
    {
		signUpPanel.SetActive(true);
		loginPanel.SetActive(false);
	}

    #region Login
    [System.Serializable]
    public class LoginRequest
    {
        public string email;
        public string password;
    }
    
    [System.Serializable]
    public class LoginResponseSuccess
    {
        public string scope;
        public string _id;
        public string name;
        public string email;
        public DateTime createdAt;
        public DateTime updatedAt;
        public int __v;
        public int iat;
        public string accessToken;
    }
    
    [System.Serializable]
    public class LoginResponseFailure
    {
        public class Error
        {
        }

        public string status;
        public string message;
        public Error error;
    }

    public void OnClick_Login()
    {
        if (loginEmail.text.IsNullOrEmpty())
        {
            PopupMessage.Instance.Show("Email must not be empty",sign: PopupMessage.PopupSign.WARNING);
            return;
        }
        
        if (!loginEmail.text.IsValidEmail())
        {
            PopupMessage.Instance.Show("Email must be valid",sign: PopupMessage.PopupSign.WARNING);
            return;
        }
        
        if (loginPassword.text.IsNullOrEmpty())
        {
            PopupMessage.Instance.Show("Password must not be empty",sign: PopupMessage.PopupSign.WARNING);
            return;
        }
        
        WaitPanel.Instance.Show();
        
        DoLogin();
    }

    public void DoLogin()
    {
        string payload = JsonConvert.SerializeObject(
            new LoginRequest()
            {
                email = loginEmail.text,
                password = loginPassword.text
            }
        );
        
        StartCoroutine(ApiHelper.Post("https://skirmish.microperts.com/users/sign-in",
            OnLoginSuccess,OnLoginError,payload));
    }

    private void OnLoginSuccess(string result)
    {
        LoginResponseSuccess responseSuccess = JsonConvert.DeserializeObject<LoginResponseSuccess>(result);
        string email = responseSuccess.email;
        string username = responseSuccess.name;
        string accessToken = responseSuccess.accessToken;
        Debug.Log($"{email} {username} {accessToken}");
        
        PlayerPrefs.SetString("email",email);
        PlayerPrefs.SetString("username",username);
        PlayerPrefs.SetString("accessToken",accessToken);
        
        SceneManagerX.LoadNextScene();
    }
    
    private void OnLoginError(string result)
    {
        WaitPanel.Instance.Hide();
        
        LoginResponseFailure responseFailure = JsonConvert.DeserializeObject<LoginResponseFailure>(result);
        Debug.Log(responseFailure.status + " " + responseFailure.message);
        PopupMessage.Instance.Show(responseFailure.message,responseFailure.status,sign: PopupMessage.PopupSign.WARNING);
    }

    #endregion

    #region SignUp
    [System.Serializable]
    public class SignUpRequest
    {
        public string name;
        public string email;
        public string password;
    }
    [System.Serializable]
    public class SignUpResponseSuccess
    {
        public string scope;
        public string _id;
        public string name;
        public string email;
        public DateTime createdAt;
        public DateTime updatedAt;
        public int __v;
    }
    
    [System.Serializable]
    public class SignUpResponseFailure
    {
        public class Error
        {
        }

        public string status;
        public string message;
        public Error error;
    }
    public void OnClick_SignUp()
    {
        if (signUpUserName.text.IsNullOrEmpty())
        {
            PopupMessage.Instance.Show("Username must not be empty",sign: PopupMessage.PopupSign.WARNING);
            return;
        }
        
        if (signUpEmail.text.IsNullOrEmpty())
        {
            PopupMessage.Instance.Show("Email must not be empty",sign: PopupMessage.PopupSign.WARNING);
            return;
        }
        
        if (!signUpEmail.text.IsValidEmail())
        {
            PopupMessage.Instance.Show("Email must be valid",sign: PopupMessage.PopupSign.WARNING);
            return;
        }
        
        if (signUpPassword.text.IsNullOrEmpty())
        {
            PopupMessage.Instance.Show("Password must not be empty",sign: PopupMessage.PopupSign.WARNING);
            return;
        }
        
        WaitPanel.Instance.Show();
        
        DoSignup();
    }

    public void DoSignup()
    {
        string payload = JsonConvert.SerializeObject(
            new SignUpRequest()
            {
                name = signUpUserName.text,
                email = signUpEmail.text,
                password = signUpPassword.text
            }
        );
        
        StartCoroutine(ApiHelper.Post("https://skirmish.microperts.com/users/sign-up",
            OnSignUpSuccess,OnSignUpError,payload));
    }

    private void OnSignUpSuccess(string result)
    {
        signUpPanel.SetActive(false);
        loginPanel.SetActive(true);
        
        PopupMessage.Instance.Show("Success");
        WaitPanel.Instance.Hide();
        
        SignUpResponseSuccess responseSuccess = JsonConvert.DeserializeObject<SignUpResponseSuccess>(result);
        string email = responseSuccess.email;
        string username = responseSuccess.name;
        Debug.Log($"{email} {username}");
    }
    
    private void OnSignUpError(string result)
    {
        WaitPanel.Instance.Hide();
        
        SignUpResponseFailure responseFailure = JsonConvert.DeserializeObject<SignUpResponseFailure>(result);
        Debug.Log(responseFailure.status + " " + responseFailure.message);
        PopupMessage.Instance.Show(responseFailure.message.ToString(),responseFailure.status.ToString(),sign: PopupMessage.PopupSign.WARNING);
    }

    #endregion
}
