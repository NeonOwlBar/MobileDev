using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

// The following code is THIRD-PARTY from Unity's documentation for initialising the ads SDK.
// Available at https://docs.unity.com/ads/en-us/manual/ImplementingBannerAdsUnity (last accessed 22/01/2024)
// Comments have been added to explain what the code does
// Within the code below, comments included in the unity docs begin with a capital letter. Any comments by the developer begin with a lowercase letter
// Some features from the original file have been removed as they were not needed

// commented out so it cannot be used in published versions due to app-ads.txt not being implemented. Code is here as evidence
// Working version of game with example banner ads will be available in the video provided with the submission


public class BannerAds : MonoBehaviour
{
    // For the purpose of this example, these buttons are for functionality testing:
    //[SerializeField] Button _loadBannerButton;
    //[SerializeField] Button _showBannerButton;
    //[SerializeField] Button _hideBannerButton;

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    // default platform IDs
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms.

    public static bool isBannerOn = false;

    void Start()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#elif UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#endif // only applicable to Android and iOS, other platforms not considered

        //// Disable the button until an ad is ready to show:
        //_showBannerButton.interactable = false;
        //_hideBannerButton.interactable = false;

        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);

        isBannerOn = true;

        //// Configure the Load Banner button to call the LoadBanner() method when clicked:
        //_loadBannerButton.onClick.AddListener(LoadBanner);
        //_loadBannerButton.interactable = true;
    }

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");

        //// Configure the Show Banner button to call the ShowBannerAd() method when clicked:
        //_showBannerButton.onClick.AddListener(ShowBannerAd);
        //// Configure the Hide Banner button to call the HideBannerAd() method when clicked:
        //_hideBannerButton.onClick.AddListener(HideBannerAd);

        //// Enable both buttons:
        //_showBannerButton.interactable = true;
        //_hideBannerButton.interactable = true;
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    public void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    public void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    void OnDestroy()
    {
        // Clean up the listeners:
        //_loadBannerButton.onClick.RemoveAllListeners();
        //_showBannerButton.onClick.RemoveAllListeners();
        //_hideBannerButton.onClick.RemoveAllListeners();
    }
}
