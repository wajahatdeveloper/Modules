using System;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

namespace CloudServices
{
    public partial class FirebaseSDK
    {
        private void InitAuthentication()
        {
            firebaseAuth = FirebaseAuth.DefaultInstance;
        }

        public void SignInAnonymously(Action<AuthResult> onSuccess, Action<string> onFailure)
        {
            firebaseAuth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task => {
                if (task.IsCanceled) {
                    Debug.LogError("SignInAnonymouslyAsync was canceled.");
                    onFailure.InvokeSafe("SignInAnonymouslyAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                    onFailure.InvokeSafe("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                    return;
                }

                var authResult = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    authResult.User.DisplayName, authResult.User.UserId);
                onSuccess.InvokeSafe(authResult);
            });
        }

        public void SignInUsingCredentials(Credential credential, Action<AuthResult> onSuccess, Action onFailure)
        {
            firebaseAuth.SignInAndRetrieveDataWithCredentialAsync(credential)
                .ContinueWithOnMainThread(task => {
                    if (task.IsCanceled)
                    {
                        DebugX.LogError($"{LogClassName} : SignInAndRetrieveDataWithCredentialAsync was canceled.",
                            LogFilters.None, gameObject);
                        onFailure.InvokeSafe();
                        return;
                    }

                    if (task.IsFaulted)
                    {
                        DebugX.LogError($"{LogClassName} : SignInAndRetrieveDataWithCredentialAsync encountered an error.",
                            LogFilters.None, gameObject);
                        onFailure.InvokeSafe();
                        return;
                    }

                    AuthResult result = task.Result;
                    DebugX.Log($"{LogClassName} : User signed in successfully: " +
                               $"{result.User.DisplayName} ({result.User.UserId})",
                        LogFilters.None, gameObject);
                    onSuccess.InvokeSafe(result);
                });
        }

        public void LinkAccountUsingCredentials(Credential credential, Action<AuthResult> onSuccess, Action onFailure)
        {
            firebaseAuth.CurrentUser.LinkWithCredentialAsync(credential)
                .ContinueWithOnMainThread(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("LinkWithCredentialAsync was canceled.");
                    onFailure.InvokeSafe();
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("LinkWithCredentialAsync encountered an error: " + task.Exception);
                    onFailure.InvokeSafe();
                    return;
                }

                AuthResult result = task.Result;
                Debug.LogFormat("Credentials successfully linked to Firebase user: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
                onSuccess.InvokeSafe(result);
            });
        }
    }
}