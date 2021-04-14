using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //YARDIMCI FOKSİYON VE ÇİZDİRME ÖRNEKLERİ

    ////Prefab nesneyi istenilen koordinatlarda çizdir.
    ////1 ---- 39.993495, 32.848014
    ////2 ---- 39.993476, 32.848003
    //var dunya = Instantiate(yerlestirilecek_Nesne,
    //    new Vector3(lat2x(39.993476f), 0f, long2z(32.848003f)),
    //    Quaternion.Euler(10f, -17f, -19f));

    ////39.993487, 32.847967
    //var cube = Instantiate(yerlestirilecek_Nesne2,
    //    new Vector3(lat2x(39.993487f), 0f, long2z(32.847967f)),
    //    Quaternion.Euler(10f, -17f, -19f));

    ////39.993499, 32.847931
    //var cube2 = Instantiate(yerlestirilecek_Nesne2,
    //    new Vector3(lat2x(39.993499f), 0f, long2z(32.847931f)),
    //    Quaternion.Euler(10f, -17f, -19f));

    ////Add an Anchor and a renderable in front of the camera       
    //Anchor anchor = Session.CreateAnchor(new Pose(new Vector3(dunya.transform.position.x, 0f, dunya.transform.position.z),
    //    dunya.transform.rotation));
    //Anchor anchor2 = Session.CreateAnchor(new Pose(new Vector3(cube.transform.position.x, 0f, cube.transform.position.z),
    //    cube.transform.rotation));
    //Anchor anchor3 = Session.CreateAnchor(new Pose(new Vector3(cube2.transform.position.x, 0f, cube2.transform.position.z),
    //    cube2.transform.rotation));

    //dunya.transform.parent = anchor.transform;
    //            cube.transform.parent = anchor2.transform;
    //            cube2.transform.parent = anchor3.transform;

    //            //Anchoru kaydırılacak objenin çocuğu yap.
    //            anchor.transform.parent = mapController.transform;
    //            anchor2.transform.parent = mapController.transform;
    //            anchor3.transform.parent = mapController.transform;
}
