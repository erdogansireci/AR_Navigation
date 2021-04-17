using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1.Uygulama açıldı.
//2.İç mekan seçildi.
//3.Ic_Mekan.scene yüklendi
//4.Hedef seç (ekranın üstüne yerleştirilen panel ile)
//5.Augmented Images başlat ve resmi al.
//6.Gelen resmin indexine göre konumu bul.
//7.dünyayı kaydır
//8.Rotayı hesapla
//9.Nodeleri ve varış noktasını yerleştir
//10.Nodeler arası çizgileri çiz
//11.bitir


public class Ic_Controller : MonoBehaviour
{
    //Ekran ortasında okutma yardımı için işaretçi belircek.
    public GameObject FitToScanOverlay;
    public GameObject nodeIsaretci;
    public GameObject varisNoktasi;
    public GameObject cizgiPrefab;

    private int resimIndex;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        AugmentedImage_Controller controller = gameObject.AddComponent<AugmentedImage_Controller>();
        Algoritmalar algoritmalar = gameObject.AddComponent<Algoritmalar>();
        Node_Konumlari nodes = gameObject.AddComponent<Node_Konumlari>();
        Ic_Draw draw = gameObject.AddComponent<Ic_Draw>();

        //Resim bulunup bulunmadığını kontrol et.
        //Yukarıda AugmentedImages zaten başlamıştı.
        while(true)
        {
            if(controller.resimIndex != -1)
            {
                FitToScanOverlay.SetActive(false);
                resimIndex = controller.resimIndex;
                controller.enabled = false;
                break;
            }
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Resim Index:" + resimIndex);

        //Dünyayı kaydır.
        nodes.Dunyayi_Kaydir(gameObject);
        Debug.Log("Dunya kaydırma tamam." + "\nOffset's: " + nodes.dunya_Offset_X +
            "\n" + nodes.dunya_Offset_Z);

        //Rota hesapla.
        algoritmalar.IcMekan_Dijkstra(nodes.graph, nodes.ResimIndex_to_Node(resimIndex), 5);
        Debug.Log("Rota hesaplandı.");

        //Üzerinden geçilecek nodeleri sırayla al ve çizdir.(diğerindeki fonk değişir.)
        draw.Node_Ciz(nodes, algoritmalar.shortestPath, nodeIsaretci, varisNoktasi);
        Debug.Log("Node çizdirme başarılı.");

        //Nodeler üzerinde ikişer ikişer dolaşarak çizgileri çizdir. (diğerindeki fonk değişir.)
        draw.Cizgi_Ciz(nodes, algoritmalar.shortestPath, cizgiPrefab);
        Debug.Log("Çizgi çizdirme başarılı.");

    }
}
