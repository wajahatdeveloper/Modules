using System;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Sirenix.OdinInspector;

[InfoBox("This Script Exposes the Following Events\n" +
         "OnFirebaseInit\n" +
         "OnFirebaseInitFailed<string>")]
public partial class FirebaseSDK : SingletonBehaviour<FirebaseSDK>
{
    public static event Action OnFirebaseInit;
    public static event Action<string> OnFirebaseInitFailed;

    private const string LogClassName = "FirebaseSDK";

    public FirebaseApp firebaseApp;
    public FirebaseAuth firebaseAuth;
    public FirebaseUser firebaseUser;
    public DatabaseReference firebaseDb;
    
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.

#if UNITY_EDITOR
                InitCloneApp();
#else
                firebaseApp = FirebaseApp.DefaultInstance;
#endif


                InitAuthentication();
                InitDatabase();

#if UNITY_EDITOR
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);  // disable caching
#endif

                DebugX.Log($"{LogClassName} : Firebase Initialized.",LogFilters.Database, null);

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                OnFirebaseInit?.Invoke();
            }
            else
            {
                var msg = $"Could not resolve all Firebase dependencies: {dependencyStatus}";

                // Firebase Unity SDK is not safe to use here.
                DebugX.LogError($"{LogClassName} : Firebase not Initialized.\n{msg}",LogFilters.Database, null);

                OnFirebaseInitFailed?.Invoke(msg);
            }
        });
    }

#if UNITY_EDITOR
    private void InitCloneApp()
    {
        int cloneIndex = MultiPlay.Utils.GetCurrentCloneIndex();
        if (cloneIndex == 1)
        {
            var secondaryAppOptions = new AppOptions();
            secondaryAppOptions.ProjectId = "holeio-1f890";
            secondaryAppOptions.StorageBucket = "holeio-1f890.appspot.com";
            secondaryAppOptions.AppId = "1:737912260248:android:e689071ea286651d83b2ed";
            secondaryAppOptions.ApiKey = "AIzaSyB26Ab9dviCDzMCfNd_iJ77gNq2_drDnM";
            secondaryAppOptions.MessageSenderId =
                "737912260248-au3qeaencae71ii5v8oek7q9v2ahrhqr.apps.googleusercontent.com";
            secondaryAppOptions.DatabaseUrl = new Uri("https://holeio-1f890-default-rtdb.firebaseio.com");
            firebaseApp = FirebaseApp.Create(secondaryAppOptions);
        }
    }
#endif
}