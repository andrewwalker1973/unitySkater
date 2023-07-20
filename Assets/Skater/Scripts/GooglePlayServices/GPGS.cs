using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;


public class GPGS : MonoBehaviour
{
    public Text statusTxt;
    public bool isConnectedtoGooglePlay;
    // Start is called before the first frame update

    public static GPGS Instance { get { return instance; } }
    private static GPGS instance;

    void Start()
    {
        instance = this;
        statusTxt.text = "Authenticating...";
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication); //automatically creates the build configuration, activates it and authenticates the user.
    }
    internal void ProcessAuthentication(SignInStatus status) // called on start, this handles the call back method received from play games platform and processes the result.
    {
        if (status == SignInStatus.Success)
        {
            statusTxt.text = "Successful Sign In";
            isConnectedtoGooglePlay = true;
        }
        else if (status == SignInStatus.InternalError)
        {
            statusTxt.text = "Failed due to internal error";
            isConnectedtoGooglePlay = false;
        }
        else if (status == SignInStatus.Canceled)
        {
            statusTxt.text = "Failed due to Canceled status";
            isConnectedtoGooglePlay = false;
        }
        else
        {
            statusTxt.text = "This should never be triggered, not one of the call back responses.";
        }
       
    }
    public void TriggerManualSignIn() // the script attached to the button to trigger a manual signin.
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }
}