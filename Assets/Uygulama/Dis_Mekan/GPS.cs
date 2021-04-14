using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS : MonoBehaviour
{
    public GameObject Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji;


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
