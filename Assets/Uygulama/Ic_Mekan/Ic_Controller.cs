using System.Collections;
using UnityEngine;

//1.Uygulama açıldı.
//2.İç mekan seçildi.
//3.Hedef seç
//4.Ic_Mekan.scene yüklendi
//5.Augmented Images başlat ve resmi al
//6.Gelen resmin indexine göre konumu bul
//7.dünyayı kaydır
//8.Rotayı hesapla
//9.Nodeleri ve varış noktasını yerleştir
//10.Nodeler arası çizgileri çiz
//11.Navigasyonu başlat.
//12.bitir

public class Ic_Controller : MonoBehaviour
{
    public static int HedefNoktasi;

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
        //Yukarıda AugmentedImages zaten başlamıştı. (controller objesi)
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

        //Dünyayı kaydır.
        nodes.Dunyayi_Kaydir(gameObject);

        //Rota hesapla.
        algoritmalar.IcMekan_Dijkstra(nodes.graph, nodes.ResimIndex_to_Node(resimIndex), HedefNoktasi);

        //Üzerinden geçilecek nodeleri sırayla al ve çizdir.(diğerindeki fonk değişir.)
        draw.Node_Ciz(nodes, algoritmalar.shortestPath, nodeIsaretci, varisNoktasi);

        //Nodeler üzerinde ikişer ikişer dolaşarak çizgileri çizdir. (diğerindeki fonk değişir.)
        draw.Cizgi_Ciz(nodes, algoritmalar.shortestPath, cizgiPrefab);

    }
}
