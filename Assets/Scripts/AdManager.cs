using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    string gameId = "3907975";
    string myPlacementId = "GGMC19";
    

    private void Awake()
    {
        ShowAd();
    }


    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);
    }

    private void OnDisable()
    {
        Advertisement.RemoveListener(this);
    }

    public void ShowAd()
    {
        StartCoroutine(ShowAdWhenReady());
    }

    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady(myPlacementId))
        {
            yield return null;
        }
        Advertisement.Show(myPlacementId);
    }

    #region UnityAdLister interfaces
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            // Do something here.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do something here;
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not fhinish due to an error");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == myPlacementId)
        {
            //  Advertisement.Show(myPlacementId);
        }

    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("OnUnityAdsDidError :" + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        if (placementId == myPlacementId)
        {
            //  Advertisement.Show(myPlacementId);
        }
    }

    #endregion
}