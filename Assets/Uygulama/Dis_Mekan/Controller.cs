using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//1.uygulama aç
//2.dış mekan seçildi
//3.hedef seçildi
//4.Dis_Mekan.scene yüklendi (Hedef bilgisi bu scripte aktarılmalı.)
//5.GPS baslat. Konumu al.
//6.Dünyayı kaydır.
//7.Rota hesapla
//8.Hangi nodelerden hangi sırayla geçildiğini al
//9.hedef noda doğru çizgiyi çizdir.
//10.varış node imleç koy
//11.bitir

public class Controller : MonoBehaviour
{

    public GameObject Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji;
    public GameObject mapCoords;

    // Start is called before the first frame update
    IEnumerator Start()
    {

        //Sınıf tanımları
        GPS gps = gameObject.AddComponent<GPS>();
        Calculations calculations = gameObject.AddComponent<Calculations>();

        //Arayüz objesini aratmak yerine direk gönderdik.
        gps.Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji = Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji;

        //GPS başlat ve max 6 m sapma ile konumu al.
        //Bu fonksyion bitene kadar diğerlerini beklet.
        yield return StartCoroutine(gps.GPS_Start());

        //Dünyayı kaydır.
        //Burada kendini yani Dis_Mekan_Controller objesini göndermiş olması gerek.
        calculations.Unity_Dunyasini_Hizala(gameObject, gps, mapCoords);
        Debug.Log("Gonderilen Obje: " + gameObject);

        //Rota hesapla (tüm nodeleri unitye çevir. yolu hesapla. nodeleri srasına göre döndür.


        //sırayla nodeleri al ve ikişer ikişer aralarında çizgi çiz


        //varış noktasına imleç koy

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
