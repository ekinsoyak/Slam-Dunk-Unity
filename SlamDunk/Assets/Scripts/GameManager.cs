using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    [Header("--LEVEL TEMEL OBJELERİ--")]
    [SerializeField]private GameObject Platform;
    [SerializeField]private GameObject Pota;
    [SerializeField]private GameObject PotaBuyume;
    [SerializeField]private GameObject[] BuyumeNoktalari;
    [SerializeField]private AudioSource[] Sesler;
    [SerializeField]private ParticleSystem[] Efektler;

    [Header("--UI OBJELERİ--")]
    [SerializeField]private Image[] GorevGorselleri;
    [SerializeField]private GameObject[] Paneller;
    [SerializeField]private Sprite GorevTamamlandi;
    [SerializeField]private int AtilmasiGerekenTop;
    [SerializeField]private TextMeshProUGUI LevelAd;
    int BasketSayisi;

    private void Start() {

        LevelAd.text="LEVEL :  "+ SceneManager.GetActiveScene().name;
        
        for (int i = 0; i < AtilmasiGerekenTop; i++)
        {
            GorevGorselleri[i].gameObject.SetActive(true);
        }

    }

    void BuyumeOlussun(){
        int RandomSayi=Random.Range(0,BuyumeNoktalari.Length-1);
        PotaBuyume.transform.position=BuyumeNoktalari[RandomSayi].transform.position;
        PotaBuyume.SetActive(true);
    }
    
    void Update()
    {
        if(Time.timeScale!=0){

            if(Input.GetKey(KeyCode.LeftArrow)){

            if(Platform.transform.position.x > -1.71)//Kenar sınırlandırması
            Platform.transform.position=Vector3.Lerp(Platform.transform.position,new Vector3(Platform.transform.position.x-.6f,
            Platform.transform.position.y,Platform.transform.position.z),.030f);

        }else if(Input.GetKey(KeyCode.RightArrow)){

            if(Platform.transform.position.x < 1.71)//Kenar sınırlandırması
            Platform.transform.position=Vector3.Lerp(Platform.transform.position,new Vector3(Platform.transform.position.x+.6f,
            Platform.transform.position.y,Platform.transform.position.z),.030f);
        }
        }

        
    }

    public void Basket(Vector3 Poz){
        Sesler[2].Play();
        BasketSayisi++;
        GorevGorselleri[BasketSayisi-1].sprite=GorevTamamlandi;
        Efektler[0].transform.position=Poz;
        Efektler[0].gameObject.SetActive(true);
        if(BasketSayisi==AtilmasiGerekenTop){
            Kazandin();
        }
        if(BasketSayisi==1){
            BuyumeOlussun();
        }
        
    }

    public void Kaybettin(){
        Sesler[0].Play();
        Paneller[2].SetActive(true);
        Time.timeScale=0;
    }

    void Kazandin(){
        Paneller[1].SetActive(true);
        Sesler[1].Play();
        PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level")+1);
        Time.timeScale=0;
    }

    public void PotaBuyut(Vector3 Poz){
        Efektler[1].transform.position=Poz;
        Efektler[1].gameObject.SetActive(true);
        Sesler[3].Play();
        Pota.transform.localScale=new Vector3(55f,55f,55f);
    }

    public void Butonislemleri(string Deger){
        switch(Deger){

            case "Durdur":
                Time.timeScale=0;
                Paneller[0].SetActive(true);
                break;
            
            case "DevamEt":
                Time.timeScale=1;
                Paneller[0].SetActive(false);
                break;
            
            case "Tekrar":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale=1;
                break;

            case "SonrakiLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
                Time.timeScale=1;
                break;

            case "Cikis":
                Application.Quit();
                break;
            
        }
    }
}


