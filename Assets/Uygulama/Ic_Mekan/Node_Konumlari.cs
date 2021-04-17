using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Konumlari : MonoBehaviour
{
    //Offset dunyaya yerleşecek nesneelerin konumlarını hesaplamakta kullanılacak.
    public float dunya_Offset_X;
    public float dunya_Offset_Z;

    //En üstteki node resm1, altındaki resim2..... şeklinde indexlenmiştir.

    //QR kodun buluduğu nodelerdeki metre hesabını yaklaşık 0.3m kadar geriden almayı unutma.
    //çünkü kamera olduğu gibi o QR kodun üzerinde değil, yakınında.

    public readonly int[,] resimIndex_to_Node_Tut = new int[2, 2]
    {
        { 0, 0},//Resim 0 - Node 1 - PC
        { 1, 5} //Resim 1 - Node 6 - TV
    };

    //bunlar ankara için bursa hesaplanmalı
    public readonly float[,] node_Coordinates = new float[6, 2]
    {
        { 0f, 0f},    //PC - QR nodesi - 
        { -1.76f, 0.86f},//kesişim - 
        { -1.76f, 6.56f},//dönüş Nok. - 
        { -0.56f, 6.56f},//kesişim2 - 
        { -0.56f, 5.00f},//salon - 
        { 3.04f, 5.6f} //TV - QR nodesi
    };

    //online distance calculatordan yararlanıldı.
    public readonly float[,] graph = new float[6, 6]
    {
        { 0, 1.96f, 0, 0, 0, 0},
        { 0, 0, 5.7f, 0, 0, 0},
        { 0, 0, 0, 1.2f, 0, 0},
        { 0, 0, 0, 0, 1.56f, 0},
        { 0, 0, 0, 0, 0, 3.65f},
        { 0, 0, 0, 0, 0, 0}
    };

    public void Dunyayi_Kaydir(GameObject Ic_Mekan_Controller)
    {
        float newX = Ic_Mekan_Controller.transform.position.x - node_Coordinates[0, 0];
        float newZ = Ic_Mekan_Controller.transform.position.z - node_Coordinates[0, 1];
        Ic_Mekan_Controller.transform.position = new Vector3(newX, 0f, newZ);
        dunya_Offset_X = newX;
        dunya_Offset_Z = newZ;
        Debug.Log("Offset: " + dunya_Offset_X + "\n" + dunya_Offset_Z);
    }

    //Alınan resim indexinin hangi nodeye ait olduğunu bulur.
    //Yani hangi QR kodun okutulduğu bulunur.
    public int ResimIndex_to_Node(int resimIndex)
    {
        for (int i = 0; i < resimIndex_to_Node_Tut.Length; i++)
        {
            if (resimIndex_to_Node_Tut[i, 0] == resimIndex)
                return resimIndex_to_Node_Tut[i, 1];
        }

        //Buraya gelirse sıkıntı.
        //Resim - Node eşleştirmesi (resimIndex_to_Node_Tut) kontrol edilmeli
        //ve doğru bir şekilde eklenildiğinden emin olunmalı.
        Debug.Log("ResimIndex bulunamadı.");
        return -1;
    }
}
