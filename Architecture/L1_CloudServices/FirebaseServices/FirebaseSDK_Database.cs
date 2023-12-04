using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

public partial class FirebaseSDK
{
    private void InitDatabase()
    {
        firebaseDb = FirebaseDatabase.DefaultInstance.RootReference;
    }

     public async Task Async_FetchData(Query query, Func<DataSnapshot, Task> onSuccess, Func<string, Task> onFailure, Func<string, Task> onNotExist)
    {
        var dbTask = query.GetValueAsync();
        var transactionId = Guid.NewGuid().ToString().Left(5);
        DebugX.Log($"{LogClassName} : Fetch Data with tId:{transactionId}.", LogFilters.API, null);

        await dbTask;

        if (dbTask.Exception != null)
        {
            var msg = $"Fetch Data with tId:{transactionId} : Failed to login task with {dbTask.Exception}";
            DebugX.Log($"{LogClassName} : {msg}.", LogFilters.API, null);
            onFailure?.Invoke(msg);
        }
        else if (dbTask.Result.Value == null)
        {
            var reason = "The Requested Data Does not Exist";
            var msg = $"Fetch Data  with tId:{transactionId} : Failed [{reason}]";
            DebugX.Log($"{LogClassName} : {msg}.", LogFilters.API, null);
            onNotExist?.Invoke(transactionId);
        }
        else
        {
            var msg = $"Fetch Data with tId:{transactionId} : Success";
            DebugX.Log($"{LogClassName} : {msg}.", LogFilters.API, null);
            onSuccess?.Invoke(dbTask.Result);
        }
    }

    public IEnumerator Routine_FetchData(Query query, Action<DataSnapshot> onSuccess, Action<string> onFailure, Action<string> onNotExist)
    {
        var dbTask = query.GetValueAsync();
        var transactionId = Guid.NewGuid().ToString().Left(5);
        DebugX.Log($"{LogClassName} : Fetch Data with tId:{transactionId}.",LogFilters.API, null);

        yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            var msg = $"Fetch Data with tId:{transactionId} : Failed to login task with {dbTask.Exception}";
            DebugX.Log($"{LogClassName} : {msg}.",LogFilters.API, null);
            onFailure?.Invoke(msg);
        }
        else if (dbTask.Result.Value == null)
        {
            var reason = "The Requested Data Does not Exist";
            var msg = $"Fetch Data  with tId:{transactionId} : Failed [{reason}]";
            DebugX.Log($"{LogClassName} : {msg}.",LogFilters.API, null);
            onNotExist?.Invoke(transactionId);
        }
        else
        {
            var msg = $"Fetch Data with tId:{transactionId} : Success";
            DebugX.Log($"{LogClassName} : {msg}.",LogFilters.API, null);
            onSuccess?.Invoke(dbTask.Result);
        }
    }

    public async Task Async_SetData(Query queryPath, object dataValue, Func<string, Task> onSuccess, Func<string, string, Task> onFailure)
    {
        var dbTask = queryPath.Reference.SetValueAsync(dataValue);
        var transactionId = Guid.NewGuid().ToString().Left(5);
        DebugX.Log($"{LogClassName} : Send Data with tId:{transactionId}.", LogFilters.API, null);

        await dbTask;

        if (dbTask.Exception != null)
        {
            var msg = $"Set Data with tId:{transactionId} : Failed to Set Data with {dbTask.Exception}";
            DebugX.Log($"{LogClassName} : {msg}.", LogFilters.API, null);
            onFailure?.Invoke(transactionId, msg);
        }
        else
        {
            var msg = $"Set Data with tId:{transactionId} : Success";
            DebugX.Log($"{LogClassName} : {msg}.", LogFilters.API, null);
            onSuccess?.Invoke(transactionId);
        }
    }

    public async Task Async_UpdateChildren(Query queryPath, IDictionary<string,object> dataDictionary, Func<string, Task> onSuccess, Func<string, string, Task> onFailure)
    {
        var dbTask = queryPath.Reference.UpdateChildrenAsync(dataDictionary);
        var transactionId = Guid.NewGuid().ToString().Left(5);
        DebugX.Log($"{LogClassName} : Update Children with tId:{transactionId}.", LogFilters.API, null);

        await dbTask;

        if (dbTask.Exception != null)
        {
            var msg = $"Update Children with tId:{transactionId} : Failed to Update Children with {dbTask.Exception}";
            DebugX.Log($"{LogClassName} : {msg}.", LogFilters.API, null);
            onFailure?.Invoke(transactionId, msg);
        }
        else
        {
            var msg = $"Update Children with tId:{transactionId} : Success";
            DebugX.Log($"{LogClassName} : {msg}.", LogFilters.API, null);
            onSuccess?.Invoke(transactionId);
        }
    }

    public async Task Async_DeleteData(Query queryPath, Func<string, Task> onSuccess, Func<string, string, Task> onFailure)
    {
        var dbTask = queryPath.Reference.RemoveValueAsync();
        var transactionId = Guid.NewGuid().ToString().Left(5);
        DebugX.Log($"{LogClassName} : Remove Data with tId:{transactionId}.", LogFilters.API, null);

        await dbTask;

        if (dbTask.Exception != null)
        {
            var msg = $"Remove Data with tId:{transactionId} : Failed to Remove Data with {dbTask.Exception}";
            DebugX.Log($"{LogClassName} : {msg}.", LogFilters.API, null);
            onFailure?.Invoke(transactionId, msg);
        }
        else
        {
            var msg = $"Remove Data with tId:{transactionId} : Success";
            DebugX.Log($"{LogClassName} : {msg}.", LogFilters.API, null);
            onSuccess?.Invoke(transactionId);
        }
    }

    public IEnumerator Routine_SetData(Query queryPath, object dataValue, Action<string> onSuccess, Action<string, string> onFailure)
    {
        var dbTask = queryPath.Reference.SetValueAsync(dataValue);
        var transactionId = Guid.NewGuid().ToString().Left(5);
        DebugX.Log($"{LogClassName} : Send Data with tId:{transactionId}.",LogFilters.API, null);

        yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            var msg = $"Set Data with tId:{transactionId} : Failed to login task with {dbTask.Exception}";
            DebugX.Log($"{LogClassName} : {msg}.",LogFilters.API, null);
            onFailure?.Invoke(transactionId, msg);
        }
        else
        {
            var msg = $"Set Data with tId:{transactionId} : Success";
            DebugX.Log($"{LogClassName} : {msg}.",LogFilters.API, null);
            onSuccess?.Invoke(transactionId);
        }
    }

}