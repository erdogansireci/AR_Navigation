using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS : MonoBehaviour
{
    public GameObject Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji;

    //private readonly float[,] node_Coordinates = new float[5, 2]
    //{
    //    { 40.225874f, 28.873428f},//node1 - kütüphanenin ordaki yol
    //    { 40.226050f, 28.873815f},//node2 - halkbank atm yanı 
    //    { 40.225636f, 28.874204f},//node3 - fakülte taşının yanı
    //    { 40.226205f, 28.875343f},//node4 - fakülte girişe dönüş
    //    { 40.225794f, 28.875706f} //node5 - fakülte kapı
    //};

    //private readonly float[,] graph = new float[5, 5]
    //{
    //    { 0, 3, 5, 0, 0},
    //    { 3, 0, 2, 9, 0},
    //    { 5, 2, 0, 6, 0},
    //    { 0, 9, 6, 0, 2},
    //    { 0, 0, 0, 2, 0}
    //};

    //bunlar ankara için bursa yukarıda
    public readonly float[,] node_Coordinates = new float[5, 2]
    {
        { 39.993534f, 32.847885f},//node1 - 
        { 39.993567f, 32.847700f},//node2 - 
        { 39.993458f, 32.847652f},//node3 - 
        { 39.993429f, 32.847843f},//node4 - 
        { 39.993324f, 32.847797f} //node5 - 
    };

    //online distance calculatordan yararlanıldı.
    public readonly float[,] graph = new float[5, 5]
    {
        { 0, 16.2f, 21.6f, 12.2f, 24.5f},
        { 16.2f, 0, 12.8f, 19.6f, 28.2f},
        { 21.6f, 12.8f, 0, 16.6f, 19.4f},
        { 12.2f, 19.6f, 16.6f, 0, 12.3f},
        { 24.5f, 28.2f, 19.4f, 12.3f, 0}
    };


    public IEnumerator GPS_Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;
        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 60;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("\n" + maxWait);
        }

        // Service didn't initialize in 60 seconds
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            Debug.Log("Location: " + Input.location.lastData.latitude + " " +
                Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " +
                Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            //
            //Burada konum alındı ama sapma hala yüksek. Aşağıdaki kontroller ile sapmanın düşmesi beklenir.
            //

            //Arayüzde yukarıdaki mesajı görüntüle.
            Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji.SetActive(true);

            //Sapmanın 6m veya altına düşmesini bekle ve arayüzde mesaj görüntüle
            while (true)
            {

                //Sapmayı kontrol et.
                if (Input.location.lastData.horizontalAccuracy > 6)
                {
                    yield return new WaitForSeconds(1);
                    Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji.GetComponent<Text>().text =
                        "AR deneyiminin başlaması için GPS hassasiyetinin düşmesini bekleyiniz." +
                        "\nGereken sapma: 6 \nŞuan ki sapma: " + Input.location.lastData.horizontalAccuracy;
                    Debug.Log("Metin Degistirildi!");
                    continue;
                }

                //ekrandaki mesajı yok et ve döngüden coroutineyi durdurarak çık.
                Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji.SetActive(false);
                yield break;
            }
        }

    }

    //GPS'i durdur.
    public void GPS_Stop()
    {
        Input.location.Stop();
    }

    //En sonki Enlem değerini döndür.
    public float GetLatitude()
    {
        return Input.location.lastData.latitude;
    }

    //En sonki Boylam değerini döndür.
    public float GetLongitude()
    {
        return Input.location.lastData.longitude;
    }

}
