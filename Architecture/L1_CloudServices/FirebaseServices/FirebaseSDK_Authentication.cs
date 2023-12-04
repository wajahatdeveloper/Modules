using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public partial class FirebaseSDK
{
    private void InitAuthentication()
    {
        firebaseAuth = FirebaseAuth.DefaultInstance;
    }
}