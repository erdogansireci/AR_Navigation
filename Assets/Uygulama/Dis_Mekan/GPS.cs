using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GPS : MonoBehaviour
{
    public GameObject Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji;

    //GPS nodelerini tutar.
    public readonly float[,] node_Coordinates = new float[9, 2]
    {
        { 39.993534f, 32.847885f},//node1 - ref
        { 39.993620f, 32.847730f},//node2 - 4.bloğa dönüş
        { 39.993458f, 32.847652f},//node3 - 6ya giden yol
        { 39.993429f, 32.847843f},//node4 - otoparka giden yol
        { 39.993324f, 32.847797f},//node5 - otopark 
        { 39.993650f, 32.847910f},//node6 - Çıkış 
        { 39.993670f, 32.847500f},//node7 - 5.bloğa dönüş
        { 39.993500f, 32.847410f},//node8 - 5.blok önü
        { 39.993520f, 32.847320f} //node9 - 5.blok kapı
    };

    //Nodeler arası bağlantıları tanımlar.
    //0 - bağlantı yok demektir.
    public readonly float[,] graph = new float[9, 9]
    {
        { 0, 16.2f, 0, 12.2f, 0, 13.1f, 0, 0, 0},
        { 0, 0, 12.8f, 0, 0, 0, 20.4f, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 12.3f, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 20.4f, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0.8f},
        { 0, 0, 0, 0, 0, 0, 0, 0, 0},
    };


    public IEnumerator GPS_Start()
    {
        // Kullanıcı GPS'i açmış mı?
        if (!Input.location.isEnabledByUser)
            yield break;
        // GPS başlat.
        Input.location.Start();

        // Max 60 sn bekle
        int maxWait = 60;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("\n" + maxWait);
        }

        // 60'sn'de başlamazsa bitir.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // Bağlanamadı durumu.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // Bağlantı başarılı. Konumu Log'a yaz.
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
                        "Waiting for the GPS accuracy to drop 6 meters or less." +
                        "\nCurrent Accuracy: " + Input.location.lastData.horizontalAccuracy;
                    Debug.Log("Metin Degistirildi!");
                    continue;
                }

                //Ekrandaki mesajı yok et ve döngüden coroutineyi durdurarak çık.
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

    //Güncel Enlem değerini döndür.
    public float GetLatitude()
    {
        return Input.location.lastData.latitude;
    }

    //Güncel Boylam değerini döndür.
    public float GetLongitude()
    {
        return Input.location.lastData.longitude;
    }

}
