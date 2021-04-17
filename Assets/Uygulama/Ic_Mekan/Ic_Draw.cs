using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ic_Draw : MonoBehaviour
{

    public void Node_Ciz(Node_Konumlari nodes, List<int> shortestPath, GameObject nodeIsaretci, GameObject varisNoktasi)
    {
        Debug.Log("Node_Ciz girildi.");

        //gelen nodeleri Node_Konumları sınıfındaki node_Coordinates ile eşleştir.
        //Bulunan nodeleri ekrana çizdir.
        foreach (var item in shortestPath)
        {
            //Objeyi oluştur.
            var nodePoint = Instantiate(nodeIsaretci,
                new Vector3(nodes.node_Coordinates[item, 0] - nodes.dunya_Offset_X, -1.2f,
                            nodes.node_Coordinates[item, 1] - nodes.dunya_Offset_Z),
                Quaternion.Euler(0f, 0f, 0f));

            //Obje konumunda anchor oluştur.
            Anchor anchor = Session.CreateAnchor(new Pose(new Vector3(nodePoint.transform.position.x,
                nodePoint.transform.position.y, nodePoint.transform.position.z), nodePoint.transform.rotation));

            //Nodeyi anchorun çocuğu yaparak AR tarafından izlenmesini sağla.
            nodePoint.transform.parent = anchor.transform;
        }

        //Varış noktasına imleç koy.
        var sonNode = shortestPath[shortestPath.Count - 1];
        var varis = Instantiate(varisNoktasi,
            new Vector3(nodes.node_Coordinates[sonNode, 0] - nodes.dunya_Offset_X, -0.6f, 
                        nodes.node_Coordinates[sonNode, 1] - nodes.dunya_Offset_Z), 
            Quaternion.Euler(0f, 0f, 0f));

        Anchor anchor2 = Session.CreateAnchor(new Pose(new Vector3(varis.transform.position.x, 0f, varis.transform.position.z),
             varis.transform.rotation));

        varis.transform.parent = anchor2.transform;
    }

    //Node_Konumları sınıfından node koordinatlarını al ve herbiri arasında çizgi çiz.
    //Burada Dış mekandaki gibi kamera konumundan çizmeye gerek yok. Çünkü kameranın
    //olduğu yer zaten başlangıç noktası.
    public void Cizgi_Ciz(Node_Konumlari nodes, List<int> shortestPath, GameObject cizgiPrefab)
    {
        Debug.Log("Cizgi_Ciz girildi.");

        for (int i = 0; i < shortestPath.Count - 1; i++)
        {
            Vector3 node1_Konum = new Vector3(nodes.node_Coordinates[i, 0] - nodes.dunya_Offset_X , -1.2f,
                                              nodes.node_Coordinates[i, 1] - nodes.dunya_Offset_Z);
            Vector3 node2_Konum = new Vector3(nodes.node_Coordinates[i + 1, 0] - nodes.dunya_Offset_X, -1.2f,
                                              nodes.node_Coordinates[i + 1, 1] - nodes.dunya_Offset_Z);

            //İki nokta arasındaki farkı al. (Çizgi uzunluğu için)
            Vector3 iki_Nokta_Arasi_Fark = node2_Konum - node1_Konum;

            //İki vektör arasındaki yön. (Çizgi rotasyonu için)
            Vector3 directionFromTopToBottom = iki_Nokta_Arasi_Fark.normalized;
            Quaternion rotationFromAToB =
                Quaternion.LookRotation(directionFromTopToBottom, Vector3.up);

            //Çizgi konumunu hesapla ve başlat.
            //İki vektör toplamı bölü iki, orta noktayı verir.
            Vector3 cizgi_Konum = node1_Konum + node2_Konum;
            cizgi_Konum.Scale(new Vector3(0.5f, 0.5f, 0.5f));
            var cizgi = Instantiate(cizgiPrefab, cizgi_Konum,
                Quaternion.Euler(0f, 0f, 0f));

            //Çizgi yönünü AtoB olarak ayarla.
            cizgi.transform.rotation = rotationFromAToB;

            //Çizgiyi aradaki mesafeye göre ölçekle
            cizgi.transform.localScale = new Vector3(0.01f, 0.01f, iki_Nokta_Arasi_Fark.magnitude);

            //Anchor üret ve çizgiyi anchorun çocuğu yap.
            Anchor anchor = Session.CreateAnchor(new Pose(new Vector3(cizgi.transform.position.x, cizgi.transform.position.y,
                cizgi.transform.position.z), cizgi.transform.rotation));
            cizgi.transform.parent = anchor.transform;


        }

    }
}
