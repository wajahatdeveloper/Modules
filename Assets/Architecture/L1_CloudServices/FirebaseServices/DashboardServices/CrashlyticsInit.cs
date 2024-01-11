using UnityEngine;

//* using Firebase.Crashlytics;

namespace CloudServices
{
    public class CrashlyticsInit : MonoBehaviour
    {
        private const string LogClassName = "0_FirebaseCrashlytics";

        private void OnEnable()
        {
            FirebaseSDK.OnFirebaseInit += Handle_OnFirebaseInit;
            FirebaseSDK.OnFirebaseInitFailed += Handle_OnFirebaseInitFailed;
        }

        private void Handle_OnFirebaseInit()
        {
            // When this property is set to true, Crashlytics will report all
            // uncaught exceptions as fatal events. This is the recommended behavior.
            //* Crashlytics.ReportUncaughtExceptionsAsFatal = true;

            // Set a flag here for indicating that your project is ready to use Firebase.
            DebugX.Log($"{LogClassName} : Crashlytics Initialized Successfully.",LogFilters.None, null);
        }

        private void Handle_OnFirebaseInitFailed(string err)
        {
            DebugX.Log($"{LogClassName} : Crashlytics Not Initialized.",LogFilters.None, null);
        }
    }
}