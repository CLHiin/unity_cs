using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map : MonoBehaviour{
    [SerializeField] GameObject[] monster;
    [SerializeField] GameObject Level;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject player;
    [SerializeField] Text timer_text;
    float CD = 3;
    int wave = 0;
    int level = 0;
    void Start() {Time.timeScale = 0;}
    void Update(){
        if(!player.GetComponent<player>().timesleep){
            if(enemy.transform.childCount < monster.Length+1 && CD > 3 ) CD = 0;
            if(level==4&& enemy.transform.childCount < monster.Length + 1 ) win();
            else if(level!=4){
                CD-=Time.deltaTime;
                if(CD <= 0 && wave < 5){
                    CD = 10*wave;
                    wave +=1;
                    if(level==1){
                        for(int i=0; i<4*wave ;i++) product_monster(1);
                        if( wave % 5 == 0 ) for(int i=0; i<wave/5 ;i++) product_monster(0);
                    }
                    else if(level==2){
                        for(int i=0; i<5*wave ;i++) product_monster(1);
                        if( wave % 5 == 0 ) for(int i=0; i<wave/5 ;i++) product_monster(2);
                    }
                    else if(level==3){
                        for(int i=0; i<3+wave ;i++) product_monster(Random.Range(3,8));
                    }
                }
                if(wave >= 5 && CD <= 0 ){
                    player.GetComponent<player>().timesleep=true;
                    Level.transform.GetChild(level).gameObject.SetActive(true);
                }
            }
            
        }
        timer_text.text = "第" + wave  + "波\n" + System.Math.Round(CD, 1) + "s";
    }
    public void next_level(){
        player.GetComponent<player>().HP=100;
        player.GetComponent<player>().MP=100;
        player.GetComponent<player>().timesleep=false;
        Level.transform.GetChild(level).gameObject.SetActive(false);
        wave = 1;
        level += 1;
        Time.timeScale = 1;
        transform.GetChild(level-1).gameObject.SetActive(false);
        transform.GetChild(level).gameObject.SetActive(true);
        if(level==4){
            timer.SetActive(false);
            product_monster(8);
            product_monster(9);
        }
    }
    public void product_monster(int i){
        GameObject m = Instantiate(monster[i],enemy.transform);
        m.transform.position=new Vector3( Random.Range(-12f,12f) , Random.Range(-2.5f,-1f) , 0 );
        m.SetActive(true);
    }
    public void Start_botton(){
        timer.SetActive(true);
        Level.transform.GetChild(level).gameObject.SetActive(true);
    }
    void win(){
        Win.SetActive(true);
        player.GetComponent<player>().timesleep=true;
    }
}