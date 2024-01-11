using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using Firebase.Auth;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CloudServices
{
    [InfoBox("This Script Exposes the Following Events\n" +
             "OnFacebookInit\n" +
             "OnFacebookInitFailed<string>")]
    public class FacebookSDK : SingletonBehaviour<FacebookSDK>
    {
        public static event Action OnFacebookInit;
        public static event Action<string> OnFacebookInitFailed;
        public static event Action OnFacebookLoginSuccess;
        public static event Action<string> OnFacebookLoginFailed;

        private const string LogClassName = "FacebookSDK";

        public bool autoInit = false;

        private Profile profile;
        private Action<ILoginResult> callbackLoginSuccess;
        private Action<string> callbackLoginFailure;    // error

        private void Start()
        {
            if (autoInit)
            {
                Init();
            }
        }

        public void Init()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(OnInitComplete, OnHideUnity);
            }
            else
            {
                FB.ActivateApp();
            }
        }

        [Button]
        [ContextMenu(nameof(Login))]
        public void Login(Action<ILoginResult> successCallback = null, Action<string> failureCallback = null)
        {
            if (successCallback != null) { callbackLoginSuccess = successCallback; }
            if (failureCallback != null) { callbackLoginFailure = failureCallback; }

            List<string> permissions = new List<string>()
            {
                "public_profile",
                "email",
                "user-friends",
            };
            FB.LogInWithReadPermissions(permissions,AuthCallback);
        }

        [Button]
        [ContextMenu(nameof(Logout))]
        public void Logout()
        {
            FB.LogOut();
        }

        private void OnInitComplete()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();

                DebugX.Log($"{LogClassName} : Facebook Initialized\n" +
                           $"IsLoggedIn: {FB.IsLoggedIn} , IsInitialized: {FB.IsInitialized}\n" +
                           $"AccessToken: {AccessToken.CurrentAccessToken}.",LogFilters.None, gameObject);
                OnFacebookInit.InvokeSafe();
            }
            else
            {
                DebugX.Log($"{LogClassName} : Facebook Initialization Failed!", LogFilters.None, gameObject);
                OnFacebookInitFailed.InvokeSafe("Failed to Initialize the Facebook SDK");
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            if (isGameShown)
            {
                // game should resume (arrived in foreground) (facebook login page)
            }
            else
            {
                // game should pause (went to background) (facebook login page)
            }
        }

        public Profile GetFacebookProfile()
        {
            return profile;
        }

        private void AuthCallback(ILoginResult result)
        {
            if (result.Cancelled)
            {
                DebugX.Log($"{LogClassName} : User Cancelled Login.",LogFilters.None, gameObject);
                return;
            }

            if (result.Error != null)
            {
                DebugX.LogError($"{LogClassName} : {result.Error}.",LogFilters.None, gameObject);
                return;
            }

            if (FB.IsLoggedIn)
            {
                DebugX.Log($"{LogClassName} : Facebook Initialized" +
                           $"\nIsLoggedIn: {FB.IsLoggedIn} , IsInitialized: {FB.IsInitialized}" +
                           $"\n{AccessToken.CurrentAccessToken}" +
                           $"\nUserId (AccessToken): {AccessToken.CurrentAccessToken.UserId}", LogFilters.None, gameObject);

                profile = null;

                #if UNITY_ANDROID && !UNITY_EDITOR
                profile = FB.Mobile.CurrentProfile();
                DebugX.Log($"{LogClassName} :\nUserId (Profile): {profile.UserID}" +
                           $"\nName (Profile): {profile.Name}." +
                           $"\nID (Profile): {profile.UserID}." +
                           $"\nEmail (Profile): {profile.Email}." +
                           $"\nPicURL (Profile): {profile.ImageURL}." +
                           $"\nBirthday (Profile): {profile.Birthday}." +
                           $"\nAgeRange (Profile): {profile.AgeRange}." +
                           $"\nFirstName (Profile): {profile.FirstName}." +
                           $"\nMiddleName (Profile): {profile.MiddleName}." +
                           $"\nLastName (Profile): {profile.LastName}." +
                           $"\nGender (Profile): {profile.Gender}." +
                           $"\nProfileLink (Profile): {profile.LinkURL}." +
                           (profile.Hometown != null?$"\nHomeTown (Profile): {profile.Hometown}.":"") +
                           (profile.Location != null?$"\nLocation (Profile): {profile.Location}.":"") +
                           $"\nFriends (Profile): {String.Join(",", profile.FriendIDs)}."
                    ,LogFilters.None, gameObject);
                #endif

                callbackLoginSuccess.InvokeSafe(result);
                OnFacebookLoginSuccess.InvokeSafe();
            }
            else
            {
                DebugX.LogError($"{LogClassName} : Facebook Login Error.",LogFilters.None, gameObject);
                callbackLoginFailure.InvokeSafe("Facebook Login Error");
                OnFacebookLoginFailed.InvokeSafe("Facebook Login Error");
            }
        }

        public void GetFriendsPlayingThisGame(Action<List<string>> friendNameList)
        {
            Debug.Log("Calling GetFriends");
            string query = "/me/friends";
            FB.API(query, HttpMethod.GET, result =>
            {
                Debug.Log("GetFriends Result : " + result);
                var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
                try
                {
                    var friendsList = (List<object>)dictionary["data"];

                    List<string> friendNames = new List<string>();

                    foreach (var dict in friendsList)
                    {
                        Debug.Log(((Dictionary<string, object>)dict)["name"]);
                        Debug.Log("Friends: " + ((Dictionary<string, object>)dict)["name"]);
                        friendNames.Add(((Dictionary<string, object>)dict)["name"].ToString());
                    }

                    friendNameList(friendNames);
                }
                catch (Exception)
                {
                    friendNameList(default);
                }
            });
        }
    }
}