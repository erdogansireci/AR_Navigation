using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//1.uygulama aç
//2.dış mekan seçildi
//3.hedef seçildi
//4.Dis_Mekan.scene yüklendi (Hedef bilgisi bu scripte aktarılmalı.)
//4.5. pusula verilerini al ve açıyı ayarla
//5.GPS baslat. Konumu al.
//6.Dünyayı kaydır.
//7.Rota hesapla
//8.Hangi nodelerden hangi sırayla geçildiğini al
//9.hedef noda doğru çizgiyi çizdir.
//11.bitir

public class Controller : MonoBehaviour
{

    public GameObject ARCamera;
    public GameObject Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji;
    public GameObject mapCoords;
    public GameObject nodeIsaretci;
    public GameObject varisNoktasi;
    public GameObject cizgi;

    Compass compass = new Compass();
    // Start is called before the first frame update
    IEnumerator Start()
    {

        //Sınıf tanımları
        GPS gps = gameObject.AddComponent<GPS>();
        Calculations calculations = gameObject.AddComponent<Calculations>();
        Algoritmalar algoritmalar = gameObject.AddComponent<Algoritmalar>();
        Draw draw = gameObject.AddComponent<Draw>();

        //Arayüz objesini aratmak yerine direk gönderdik.
        gps.Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji = Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji;

        //GPS başlat ve max 6 m sapma ile konumu al.
        //Bu fonksyion bitene kadar diğerlerini beklet.
        yield return StartCoroutine(gps.GPS_Start());

        //Dünyayı kaydır.
        calculations.Unity_Dunyasini_Hizala(gameObject, gps, mapCoords);

        //Rota hesapla (tüm nodeleri unitye çevir. yolu hesapla. nodeleri srasına göre döndür.
        algoritmalar.Dijkstra(gps.graph, 4);
        Debug.Log("Rota hesaplandı.");

        mapCoords.GetComponent<Text>().text += "\nkamerayı oynatın.";
        yield return new WaitForSeconds(3);
        mapCoords.GetComponent<Text>().text += "\n3 sn beklendi.";

        //GPSden nodeleri al. Navigasyon algoritmasına ver. ordan node sırasını al.
        //gidilecek nodeleri çizdir.
        draw.Node_Ciz(gps, calculations, algoritmalar.shortestPath, nodeIsaretci, varisNoktasi, mapCoords);
        Debug.Log("Node çizdirme başarılı.");

        //sırayla nodeleri al ve ikişer ikişer aralarında çizgi çiz
        draw.Cizgi_Ciz(cizgi, ARCamera, calculations, mapCoords);
        Debug.Log("Çizgiler çizdirildi.");

        mapCoords.GetComponent<Text>().text +=
                "\nÇizgiler çizdirildi.";


    }

    // Update is called once per frame
    void Update()
    {

    }
}
