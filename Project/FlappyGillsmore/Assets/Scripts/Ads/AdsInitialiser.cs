using UnityEngine;
using UnityEngine.Advertisements;

// The following code is THIRD-PARTY from Unity's documentation for initialising the ads SDK.
// Available at https://docs.unity.com/ads/en-us/manual/InitializingTheUnitySDK (last accessed 22/01/2024)
// comments have been added to explain what the code does


public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    // must reference the project's Game ID for each platform here
    [SerializeField] string _androidGameId;
    // not currently built for release on the App Store
    [SerializeField] string _iOSGameId;
    // whether ads are being tested or not
    [SerializeField] bool _testMode = true;
    // current active game id
    private string _gameId;

    void Awake()
    {
        // init early
        InitializeAds();
    }

    public void InitializeAds()
    {
        // define game id depending on current platform
#if UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        // will initialise ads if not already done so, and ads are supported
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            // this should be done as early in the game's run-time lifecycle as possible, preferably at launch
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}