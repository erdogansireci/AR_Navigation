using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Main : MonoBehaviour
{
    // İç ve dış mekan varış noktalarının listesi
    private List<string> itemsIcMekan = new List<string> { "Oda1", "Oda2", "Çıkış" };
    private List<int> icMekanReturnValue = new List<int> { 5, 6, 7 };
    private List<string> itemsDisMekan = new List<string> { "Otopark", "Bina Önü", "Çıkış" };
    private List<int> disMekanReturnValue = new List<int> { 4, 8, 5 };

    public GameObject yonlendirme_sayfasi;
    public GameObject anasayfa;

    private bool icMekanFlag;

    // Dış mekan konumlarını dropbox'a yerleştir.
    public void nameinserterDısMekan()
    {
        this.GetComponent<Dropdown>().ClearOptions();
        this.GetComponent<Dropdown>().AddOptions(itemsDisMekan);
        yonlendirme_sayfasi.SetActive(true);
        anasayfa.SetActive(false);
        icMekanFlag = false;
    }

    // İç mekan konumlarını dropbox'a yerleştir.
    public void nameinserterIcMekan()
    {
        this.GetComponent<Dropdown>().ClearOptions();
        this.GetComponent<Dropdown>().AddOptions(itemsIcMekan);
        yonlendirme_sayfasi.SetActive(true);
        anasayfa.SetActive(false);
        icMekanFlag = true;
    }

    public void returnMainPage()
    {
        yonlendirme_sayfasi.SetActive(false);
        anasayfa.SetActive(true);
    }

    //Seçilen navigasyon ve konuma göre navigasyon sistemlerini başlatır.
    public void callScene()
    {
        int chosenItem = this.GetComponent<Dropdown>().value;
        if(icMekanFlag == true)
        {
            Ic_Controller.HedefNoktasi = icMekanReturnValue[chosenItem];
            SceneManager.LoadScene(1);
        }
        else
        {
            Controller.HedefNoktasi = disMekanReturnValue[chosenItem];
            SceneManager.LoadScene(2);
        }

    }
}
