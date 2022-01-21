using System.Collections;
using UnityEngine;


//1.uygulamayı aç
//2.Dış mekanı seç
//3.hedefi seç
//4.Dis_Mekan.scene yüklendi
//5.GPS baslat. Konumu al
//6.Dünyayı hizala
//7.Rota hesapla
//8.Hangi nodelerden hangi sırayla geçildiğini al
//9.hedef noda doğru çizgiyi çizdir
//11.Navigasyonu başlat
//12.Bitir

public class Controller : MonoBehaviour
{

    public GameObject Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji;
    public GameObject ARCamera;
    public GameObject nodeIsaretci;
    public GameObject varisNoktasi;
    public GameObject cizgi;

    public static int HedefNoktasi;

    // Start is called before the first frame update
    IEnumerator Start()
    {

        //Sınıf tanımları
        Calculations calculations = gameObject.AddComponent<Calculations>();
        Algoritmalar algoritmalar = gameObject.AddComponent<Algoritmalar>();
        Draw draw = gameObject.AddComponent<Draw>();
        GPS gps = gameObject.AddComponent<GPS>();

        //Arayüz objesini aratmak yerine direk gönderdik.
        gps.Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji = Arayuz_GPS_Sapma_Dusmesi_Bekleme_Mesaji;

        //GPS başlat.
        //Bu fonksyion bitene kadar diğerlerini beklet.
        yield return StartCoroutine(gps.GPS_Start());

        //Dünyayı kaydır.
        calculations.Unity_Dunyasini_Hizala(gameObject, gps);

        //Rota hesapla (tüm nodeleri unitye çevir. yolu hesapla. nodeleri srasına göre döndür.
        algoritmalar.Dijkstra(gps.graph, HedefNoktasi);

        //Yüzey tanıma için 3 sn bekle.
        yield return new WaitForSeconds(3);

        //Hangi node'lerden geçilecekse onları çizdir.
        draw.Node_Ciz(gps, calculations, algoritmalar.shortestPath, nodeIsaretci, varisNoktasi);

        //Çizilen nodeler arası çizgileri çiz.
        draw.Cizgi_Ciz(cizgi, ARCamera);
    }
}
