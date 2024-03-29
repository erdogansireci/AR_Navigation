﻿using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public List<List<float>> node_Konumlari = new List<List<float>>();

    // Üzerinden gidilecek nodeleri çizer.
    public void Node_Ciz(GPS gps, Calculations calculations, List<int> shortestPath, GameObject nodeIsaretci, GameObject varisNoktasi)
    {
        Debug.Log("Node_Ciz girildi.");

        //gelen nodeleri gps sınıfındaki node_Coordinates ile eşleştir.
        //Alınan koordinatları metreye çevir.
        foreach (var item in shortestPath)
        {

            var nodePoint = Instantiate(nodeIsaretci,
                new Vector3(calculations.Enlem2x(gps.node_Coordinates[item,0]) - calculations.dunya_Offset_X, -1.3f, 
                            calculations.Boylam2z(gps.node_Coordinates[item,1])- calculations.dunya_Offset_Z),
                Quaternion.Euler(0f, 0f, 0f));

            //Node pozisyonunda bir anchor oluştur.
            Anchor anchor = Session.CreateAnchor(new Pose(new Vector3(nodePoint.transform.position.x, nodePoint.transform.position.y,
                nodePoint.transform.position.z), nodePoint.transform.rotation));

            //Nodeyi anchorun çocuğu yaparak ARCore tarafından izlenmesini sağla.
            nodePoint.transform.parent = anchor.transform;

            //Node pozisyonlarını çizgileri çizdirmede kullanılmak üzere sakla.
            node_Konumlari.Add(new List<float> {
                nodePoint.transform.position.x, nodePoint.transform.position.z });
        }

        //Varış noktasına imleç koy.
        var sonNode = node_Konumlari[node_Konumlari.Count - 1];
        var varis = Instantiate(varisNoktasi,
            new Vector3(sonNode[0], -1.1f, sonNode[1]), Quaternion.Euler(0f, 0f, 0f));
        varis.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        Anchor anchor2 = Session.CreateAnchor(new Pose(new Vector3(varis.transform.position.x, varis.transform.position.y, 
            varis.transform.position.z), varis.transform.rotation));
        varis.transform.parent = anchor2.transform;
    }

    //Nodeler arası çizgileri çizer.
    public void Cizgi_Ciz(GameObject cizgiPrefab, GameObject ARCamera)
    {
        bool flag = false;
        for (int i = 0; i < node_Konumlari.Count - 1; i++)
        {
            List<float> node1 = new List<float>();
            List<float> node2 = new List<float>();

            //İçeri girerse kameradan ilk nodeye doğru çizim için gerekli
            //tanımlar yapılır. girmezse diğer nodeler araları çizdirilir.
            if (flag == false)
            {
                node1.Add(ARCamera.transform.position.x);
                node1.Add(ARCamera.transform.position.z);
                node2 = node_Konumlari[i];
                flag = true;
                i--;
            }
            else
            {
                node1 = node_Konumlari[i];
                node2 = node_Konumlari[i + 1];
            }

            Vector3 node1_Konum = new Vector3(node1[0], -1.3f, node1[1]);
            Vector3 node2_Konum = new Vector3(node2[0], -1.3f, node2[1]);

            //İki nokta arasındaki farkı al. (çizgi uzunluğu için)
            Vector3 iki_Nokta_Arasi_Fark = node2_Konum - node1_Konum;
            Debug.Log("Vector fark: " + iki_Nokta_Arasi_Fark.x + " " + iki_Nokta_Arasi_Fark.y + " " + iki_Nokta_Arasi_Fark.z);

            //İki vektör arasındaki yönü hesapla. (çizginin yönü için)
            Vector3 directionFromTopToBottom = iki_Nokta_Arasi_Fark.normalized;
            Quaternion rotationFromAToB =
                  Quaternion.LookRotation(directionFromTopToBottom, Vector3.up);

            //Çizgi konumunu hesapla ve başlat.
            //İki vektor toplamı bölü iki, orta noktayı verir.
            Vector3 cizgi_Konum = node1_Konum + node2_Konum;
            cizgi_Konum.Scale(new Vector3(0.5f, 0.5f, 0.5f));
            var cizgi = Instantiate(cizgiPrefab, cizgi_Konum,
                Quaternion.Euler(0f, 0f, 0f));

            //Çizgi yönünü AtoB olarak ayarla.
            cizgi.transform.rotation = rotationFromAToB;

            //Çizgiyi aradaki mesafeye göre ölçekle.
            cizgi.transform.localScale = new Vector3(0.01f, 0.01f, iki_Nokta_Arasi_Fark.magnitude);

            //Anchorun çocuğu yap.
            Anchor anchor = Session.CreateAnchor(new Pose(new Vector3(cizgi.transform.position.x, cizgi.transform.position.y,
                cizgi.transform.position.z), cizgi.transform.rotation));
            cizgi.transform.parent = anchor.transform;
        }
    }
}
