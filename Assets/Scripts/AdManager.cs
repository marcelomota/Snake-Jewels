using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    public GameObject continueButton;
    public Text continueDisponivelText;
    public Text semVideoPickUpText;
    
    // Start is called before the first frame update
    private string AppId = "ca-app-pub-5126662077040738~8350445036";
    private RewardedAd rewardVideo;
    private string rewardVideoId = "ca-app-pub-5126662077040738/4076158105";
    private RewardedAd rewardVideoPickUp;
    private string rewardVideoPickUpId = "ca-app-pub-5126662077040738/1683698989";

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
       // mixer.SetFloat("Volume",barVolume.maxValue);
        // rewardVideo = RewardBasedVideoAd.Instance;
        
        RequestRewardedAd();
        MobileAds.Initialize(initStatus => { });
    }

    // Update is called once per frame
   
    public void RequestRewardedAd() {

        this.rewardVideo = new RewardedAd(rewardVideoId);
        this.rewardVideoPickUp = new RewardedAd(rewardVideoPickUpId);

        this.rewardVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        this.rewardVideo.OnUserEarnedReward += HandleRewardBasedVideoContinue;
        this.rewardVideo.OnAdClosed += HandleRewardBasedVideoClosed;

        this.rewardVideoPickUp.OnAdLoaded += HandleRewardBasedVideoLoadedGanharPickUp;
        this.rewardVideoPickUp.OnUserEarnedReward += HandleRewardBasedVideoGanharPickUp;
        this.rewardVideoPickUp.OnAdClosed += HandleRewardBasedVideoClosedGanharPickUp;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardVideo.LoadAd(request);
        this.rewardVideoPickUp.LoadAd(request);



    }
    public void ShowRewardedAd() {

        if (rewardVideo.IsLoaded())
        {
            continueDisponivelText.gameObject.SetActive(false);
            continueButton.SetActive(true);
            rewardVideo.Show();
            
        }
        else
        {
            continueDisponivelText.gameObject.SetActive(true);
            continueButton.SetActive(false);
        }
        
    }

    public void HandleRewardBasedVideoContinue(object sender, Reward args) {
        string type = args.Type;
        double amount = args.Amount;
        //RequestRewardedAd();
        LevelController.instance.ContinueGameOver();
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args) {
        
        
        RequestRewardedAd();
        
        
    
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args) {
        
    }


    public void HandleRewardBasedVideoGanharPickUp(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        //RequestRewardedAd();
        LevelController.instance.ContadorPickups(20);
    }

    public void HandleRewardBasedVideoClosedGanharPickUp(object sender, EventArgs args)
    {


        RequestRewardedAd();



    }

    public void HandleRewardBasedVideoLoadedGanharPickUp(object sender, EventArgs args)
    {

    }



    public void ShowVideoGanharPickUp() {
        if (rewardVideoPickUp.IsLoaded())
        {
            rewardVideoPickUp.Show();
        }
        else
        {
            semVideoPickUpText.gameObject.SetActive(true);
            Invoke("desativarTextoSemVideoPickUp",3f);
        }
    
    }
    public void desativarTextoSemVideoPickUp() {

        semVideoPickUpText.gameObject.SetActive(false);
    }


   
}
