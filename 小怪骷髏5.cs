using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 小怪骷髏5 : MonoBehaviour{
    [SerializeField] GameObject player;
    [SerializeField] GameObject state_HP;
    [SerializeField] Text state_text;
    float attact_show = 0;
    float movespeed = 3f;
    int HP = 50;

    void Update(){
        keydown();
        transform.Translate(0,0.01f*Time.deltaTime,0);
        if ( HP <= 0 ) Destroy(gameObject);
        if (attact_show > 0) attact_show -= Time.deltaTime;
        else GetComponent<Animator>().SetBool("攻擊",false);
        if(attact_show > 0 && attact_show <=0.0+5*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(true);
        else if (attact_show<=0) transform.GetChild(0).gameObject.SetActive(false);

    }

    void keydown(){
        if( attact_show <= 0 ){
            if(System.Math.Pow(player.transform.position.x - transform.position.x , 2 ) + 
               System.Math.Pow(player.transform.position.y - transform.position.y , 2 ) >= 1){
                if ( player.transform.position.x > transform.position.x ){
                    transform.GetChild(0).transform.position = new Vector3( transform.position.x+0.75f, transform.position.y, 0);
                    transform.Translate(movespeed*Time.deltaTime,0,0);
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                if ( player.transform.position.x < transform.position.x ){
                    transform.GetChild(0).transform.position = new Vector3( transform.position.x-0.75f, transform.position.y, 0);
                    transform.Translate(-movespeed*Time.deltaTime,0,0);
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                if ( player.transform.position.y > transform.position.y ) transform.Translate(0,movespeed*Time.deltaTime,0);
                if ( player.transform.position.y < transform.position.y ) transform.Translate(0,-movespeed*Time.deltaTime,0);
            }
            else{
                attact_show=1.1f;
                GetComponent<Animator>().SetBool("攻擊",true);
                
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        hurt(player.GetComponent<player>().HP_hurt(other.gameObject.tag));
    }
    void hurt(int i){
        HP-=i;
        state_text.text = HP +"/50";
        state_HP.GetComponent<Image>().fillAmount =0.02f*HP;
    }
}
