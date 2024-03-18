using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameUI : MonoBehaviour
{
    // banner ad implementation
    public BannerAds bannerAd;

    // UI empty object
    public GameObject uiObject;

    // banner ad timers
    // duration ad shows
    public float adOnDuration = 10.0f;
    // duration no ad is shown
    public float adOffDuration = 5.0f;
    // tracks duration
    public float timer = 0.0f;

    // banner ad status
    public bool isAdShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        uiObject = gameObject;

        // load banner if not already loaded elsewhere
        if (!Advertisement.Banner.isLoaded)
        {
            bannerAd.LoadBanner();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // increase timer
        timer += Time.deltaTime;

        // if ad needs to be shown
        if (!isAdShowing && timer >= adOffDuration)
        {
            // show banner
            bannerAd.ShowBannerAd();

            // update ad status
            isAdShowing = true;
            //reset timer
            timer = 0.0f;
        }
        // if add needs to be removed
        else if (isAdShowing && timer >= adOnDuration)
        {
            // hide banner
            // unsure if this allows different ads, or simply hides and shows the same ad repeatedly
            bannerAd.HideBannerAd();

            // update ad status
            isAdShowing = false;
            //reset timer
            timer = 0.0f;
        }
    }

    public void Deactivate()
    {
        bannerAd.HideBannerAd();
        GameObject.Destroy(uiObject);
    }
}
