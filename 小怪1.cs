using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 小怪1 : MonoBehaviour{
    [SerializeField] GameObject player;
    [SerializeField] GameObject state_HP;
    [SerializeField] Text state_text;
    float attact_show = 0;
    float movespeed = 3f;
    int HP = 20;
    void Update(){
        if( HP <= 0 ) Destroy(gameObject);
        if(!keydown()) {
            GetComponent<Animator>().SetBool("移動",false);
            if (attact_show<0.1f) HP=0;
            else if(attact_show<=0.5f) transform.GetChild(0).gameObject.SetActive(true);
        }
        if (attact_show>0) attact_show -= Time.deltaTime;
    }

    bool keydown(){
        bool move =false;
        if( attact_show <= 0 ){
            if(System.Math.Pow(player.transform.position.x - transform.position.x , 2 ) + 
               System.Math.Pow(player.transform.position.y - transform.position.y , 2 ) >= 1){
                move=true;
                if ( player.transform.position.x > transform.position.x ){
                    transform.Translate(movespeed*Time.deltaTime,0,0);
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                if ( player.transform.position.x < transform.position.x ){
                    transform.Translate(-movespeed*Time.deltaTime,0,0);
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                if ( player.transform.position.y > transform.position.y ) transform.Translate(0,movespeed*Time.deltaTime,0);
                if ( player.transform.position.y < transform.position.y ) transform.Translate(0,-movespeed*Time.deltaTime,0);
            }
            else attact_show=1f;
        }
        return move;
    }
    void OnTriggerEnter2D(Collider2D other) {
        hurt(player.GetComponent<player>().HP_hurt(other.gameObject.tag));
    }
    void hurt(int i){
        HP-=i;
        state_text.text = HP +"/20";
        state_HP.GetComponent<Image>().fillAmount =0.05f*HP;
    }
}
