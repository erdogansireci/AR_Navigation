using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentedImage_Controller : MonoBehaviour
{

    private List<AugmentedImage> temp_AugmentedImages = new List<AugmentedImage>();

    //If'e bir daha girmeyi engeller.
    private bool flag = false;

    //Bulunan resmin indexini tutar.
    public int resimIndex = -1;

    void Update()
    {
        if (!flag)
        {
            // Get updated augmented images for this frame.
            Session.GetTrackables<AugmentedImage>(
                temp_AugmentedImages, TrackableQueryFilter.Updated);

            foreach (var image in temp_AugmentedImages)
            {
                if (image.TrackingState == TrackingState.Tracking)
                {
                    Debug.Log("Resim bulundu.");
                    resimIndex = image.DatabaseIndex;
                    flag = true;
                }
            }
        }
    }
}
