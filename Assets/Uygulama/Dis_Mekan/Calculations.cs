using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculations : MonoBehaviour
{
    //Offset dunyaya yerleşecek nesneelerin konumlarını hesaplamakta kullanılacak.
    public float dunya_Offset_X;
    public float dunya_Offset_Z;

    //Kütüphane merrdivenlerinin tam ortası orijinimiz
    //40.22578,28.87332

    //Enlem koordinatını Unity cinsinden koordinata (x-ekseni) çevir. (HESAPLAMAYI BURSAYAS ÇEVİRMEYİ UNUTMA. BUNLAR ANKARA İÇİN)
    //39.993534f - Seçilen (0,0) noktasının enlem koordinatı
    //0.1111111111111111111f - unitye geçirme katsayısı, bursa için yeniden hesaplanacak
    public float Enlem2x(float guncel_Enlem)
    {
        float x = (guncel_Enlem - 39.993534f) * 1000000f * 0.1111111111111111111f;
        return x;
    }

    //Boylam koordinatını Unity cinsinden koordinata (z-ekseni) çevir. (HESAPLAMAYI BURSAYAS ÇEVİRMEYİ UNUTMA. BUNLAR ANKARA İÇİN)
    //32.847882f - Seçilen (0,0) noktasının boylam koordinatı
    //0.083333333333333333f - unitye geçirme katsayısı, bursa için yeniden hesaplanacak
    public float Boylam2z(float guncel_Boylam)
    {
        float z = (guncel_Boylam - 32.847882f) * 1000000f * 0.083333333333333333f;
        return z;
    }

    //GPS'den alınan son konuma göre, Unity dünyasını gerçek dünyaya hizala ve offset değerini sakla
    public void Unity_Dunyasini_Hizala(GameObject Dis_mekan_Controller,GPS gps, GameObject mapCoords)
    {
        float newX = Dis_mekan_Controller.transform.position.x - Enlem2x(gps.GetLatitude());
        float newZ = Dis_mekan_Controller.transform.position.z - Boylam2z(gps.GetLongitude());
        Dis_mekan_Controller.transform.position = new Vector3(newX, 0f, newZ);
        dunya_Offset_X = newX;
        dunya_Offset_Z = newZ;
        Debug.Log("Lat: " + gps.GetLatitude() + "\nLong: " + gps.GetLongitude());
        Debug.Log("mapX: " + newX + "\nmapZ: " + newZ);
        mapCoords.GetComponent<Text>().text = "mapX: " + newX + "\nmapZ: " + newZ +
            "\nLat: " + gps.GetLatitude() + "\nLong: " + gps.GetLongitude();
    }

    //public void Unity_Dunyasi_Aci_Ayarla(GameObject Dis_mekan_Controller, Compass compass)
    //{
    //    Debug.Log("Compass Degree: " + compass.trueHeading);
    //    float derece_Sıfır_Noktasi = 308f;
    //    derece_Offset_Y = compass.trueHeading - derece_Sıfır_Noktasi;
    //    Dis_mekan_Controller.transform.Rotate(0f, derece_Offset_Y, 0f, Space.World);
    //    Debug.Log("DisMekanRotation: " + Dis_mekan_Controller.transform.rotation.y +
    //        "\ndereceOffsetY: " + derece_Offset_Y +
    //        "\nCompass Degree: " + compass.trueHeading);
    //}

}
