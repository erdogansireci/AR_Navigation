using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculations : MonoBehaviour
{
    //Offset dünyaya yerleşecek nesnelerin konumlarını hesaplamakta kullanılacak.
    public float dunya_Offset_X;
    public float dunya_Offset_Z;

    //Enlem koordinatını metreye (x-ekseni) çevir
    //39.993534f - Orijin noktası
    public float Enlem2x(float guncel_Enlem)
    {
        float x = (guncel_Enlem - 39.993534f) * 1000000f * 0.1111111111111111111f;
        return x;
    }

    //Boylam koordinatını metreye (z-ekseni) çevir
    //32.847882f - Orijin noktası
    public float Boylam2z(float guncel_Boylam)
    {
        float z = (guncel_Boylam - 32.847882f) * 1000000f * 0.083333333333333333f;
        return z;
    }

    //GPS'den alınan son konuma göre, Unity dünyasını gerçek dünyaya hizala ve offset değerini sakla
    public void Unity_Dunyasini_Hizala(GameObject Dis_mekan_Controller,GPS gps)
    {
        float newX = Dis_mekan_Controller.transform.position.x - Enlem2x(gps.GetLatitude());
        float newZ = Dis_mekan_Controller.transform.position.z - Boylam2z(gps.GetLongitude());
        Dis_mekan_Controller.transform.position = new Vector3(newX, 0f, newZ);
        dunya_Offset_X = newX;
        dunya_Offset_Z = newZ;
    }
}
