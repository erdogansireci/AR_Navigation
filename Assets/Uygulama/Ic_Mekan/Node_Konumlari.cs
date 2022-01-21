using UnityEngine;

public class Node_Konumlari : MonoBehaviour
{
    //Offset dunyaya yerleşecek nesnelerin konumlarını hesaplamakta kullanılacak.
    public float dunya_Offset_X;
    public float dunya_Offset_Z;

    //Hedef noktası olabilecek noktaları tutar.
    public readonly int[,] resimIndex_to_Node_Tut = new int[2, 2]
    {
        { 0, 0},//Resim 0 - Node 1 - PC
        { 1, 5} //Resim 1 - Node 6 - TV
    };

    //Bulunulan konumun modeli
    public readonly float[,] node_Coordinates = new float[8, 2]
    {
        {  0f, 0f},    //PC - QR nodesi - 1
        { -1.76f, 0.86f},//kesişim - 2
        { -1.76f, 6.56f},//dönüş Nok. - 3
        { -0.56f, 6.56f},//kesişim2 - 4
        { -0.56f, 4.80f},//salon - 5
        {  1.60f, 4.80f}, //Oda1 - QR nodesi 6
        {  1.76f, 0.86f},//Oda2 7
        { -1.76f, 8.00f}//Çıkış 8
    };

    //Nodeler arası bağlantılar
    public readonly float[,] graph = new float[8, 8]
    {
        { 0, 1.96f, 0, 0, 0, 0, 0, 0},
        { 0, 0, 5.7f, 0, 0, 0, 2.00f, 0},
        { 0, 0, 0, 1.2f, 0, 0, 0, 1.44f},
        { 0, 0, 0, 0, 1.76f, 0, 0, 0},
        { 0, 0, 0, 0, 0, 2.16f, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0, 0}
    };

    //Bulunulan konuma göre dünyayı hizala
    public void Dunyayi_Kaydir(GameObject Ic_Mekan_Controller)
    {
        float newX = Ic_Mekan_Controller.transform.position.x - node_Coordinates[0, 0];
        float newZ = Ic_Mekan_Controller.transform.position.z - node_Coordinates[0, 1];
        Ic_Mekan_Controller.transform.position = new Vector3(newX, 0f, newZ);
        dunya_Offset_X = newX;
        dunya_Offset_Z = newZ;
    }

    //Okutulan resme göre konumu gösteren indexi bul.
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
        return -1;
    }
}
