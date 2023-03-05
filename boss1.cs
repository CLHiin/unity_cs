using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss1 : MonoBehaviour{
    [SerializeField] GameObject player;
    [SerializeField] GameObject state_HP;
    [SerializeField] Text state_text;
    int HP = 100;
    float animtor_show = 0;
    float attact_show = 0;
    float attact_CD = 0;
    float movespeed = 3;
    float x;
    float y;
    void Update(){

        if( HP <= 0 ) Destroy(gameObject);
        if(!keydown()) GetComponent<Animator>().SetBool("移動",false);
        if(attact_CD > 0 ) attact_CD -= Time.deltaTime;
        if(attact_show <=0) GetComponent<Animator>().SetBool("攻擊",false);
        else attact_show -= Time.deltaTime;
        transform.Translate(0,0.01f*Time.deltaTime,0);
        if     (attact_show > 1.6 && attact_show <=1.6+5*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(true);
        else if(attact_show > 0.9 && attact_show <=0.9+5*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(true);
        else if(attact_show > 0.2 && attact_show <=0.2+5*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(true);
        else if(attact_show > 0.0 && attact_show <=0.0+5*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(true);

        if     (attact_show < 1.6 && attact_show >=1.6-5*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(false);
        else if(attact_show < 0.9 && attact_show >=0.9-5*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(false);
        else if(attact_show < 0.2 && attact_show >=0.2-5*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(false);
        else if(attact_show < 0.0 && attact_show >=0.0-5*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(false);
        x=transform.position.x;
        y=transform.position.y;

    }

    bool keydown(){
        bool move =false;
        if(animtor_show <= 0 && attact_show <= 0 && 100 >=
            System.Math.Pow(player.transform.position.x - x , 2 ) + 
            System.Math.Pow(player.transform.position.y - y , 2 ) ){
            if(System.Math.Pow(player.transform.position.x - x , 2 ) + 
               System.Math.Pow(player.transform.position.y - y , 2 ) >= 1){
                move=true;
                if ( player.transform.position.x > x ){
                    transform.Translate(movespeed*Time.deltaTime,0,0);
                    GetComponent<SpriteRenderer>().flipX = false;
                    transform.GetChild(0).transform.position= new Vector3( x + 0.5f , y , 0 );
                }
                if ( player.transform.position.x < x ){
                    transform.Translate(-movespeed*Time.deltaTime,0,0);
                    GetComponent<SpriteRenderer>().flipX = true;
                    transform.GetChild(0).transform.position= new Vector3( x - 0.5f , y , 0 );
                }
                if ( player.transform.position.y > y ) transform.Translate(0,movespeed*Time.deltaTime,0);
                if ( player.transform.position.y < y ) transform.Translate(0,-movespeed*Time.deltaTime,0);
                GetComponent<Animator>().SetBool("移動",true);
            }
            else if (attact_CD <= 0) {
                GetComponent<Animator>().SetBool("攻擊",true);
                attact_show=2f;
                attact_CD=3f;
            }
        }
        return move;
    }
    void OnTriggerEnter2D(Collider2D other) {
        hurt(player.GetComponent<player>().HP_hurt(other.gameObject.tag));
    }
    void hurt(int i){
        HP-=i;
        state_text.text = HP +"/100";
        state_HP.GetComponent<Image>().fillAmount =0.01f*HP;

    }
}
