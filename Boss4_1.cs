using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss4_1 : MonoBehaviour{
    [SerializeField] GameObject player;
    [SerializeField] GameObject map;
    [SerializeField] GameObject state_HP;
    [SerializeField] Text state_text;
    float attact_show = 0;
    float attact_CD = 0;
    float movespeed = 1f;
    float flipX , flipY ;
    int HP = 400;

    void Update(){
        keydown();
        transform.Translate(0,0.01f*Time.deltaTime,0);
        if ( HP <= 0 ) Destroy(gameObject);
        if (attact_show > 0 && attact_show <=Time.deltaTime ) map.GetComponent<map>().product_monster(Random.Range(3,8));
        if (attact_show > 0) attact_show -= Time.deltaTime;
        else GetComponent<Animator>().SetBool("攻擊",false);
        if (attact_CD > 0) attact_CD -= Time.deltaTime;
    }

    void keydown(){
        if( attact_show <= 0 ){
            if(transform.position.x>11) flipX = -System.Math.Abs(flipX);
            else if(transform.position.x<-11)flipX = System.Math.Abs(flipX);
            if(transform.position.y>1) flipY = -System.Math.Abs(flipY);
            else if(transform.position.y<-2) flipY = System.Math.Abs(flipY);

            if(attact_CD <= 0){
                attact_show=2;
                attact_CD=6;
                flipX=Random.Range( -3.0f , 3.0f );
                flipY= 2;
                GetComponent<Animator>().SetBool("攻擊",true);
            }
            else {
                transform.Translate( flipX*movespeed*Time.deltaTime , flipY*movespeed*Time.deltaTime , 0 );
                if ( player.transform.position.x > transform.position.x ) GetComponent<SpriteRenderer>().flipX = true;
                else GetComponent<SpriteRenderer>().flipX = false;
                     if( transform.position.y >1.5f ) flipY*=-1;
                else if( transform.position.y < -2f ) flipY*=-1;
                     if( transform.position.x > 11f ) flipX*=-1;
                else if( transform.position.x <-11f ) flipX*=-1;
            }
            
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        hurt(player.GetComponent<player>().HP_hurt(other.gameObject.tag));
    }
    void hurt(int i){
        HP-=i;
        state_text.text = HP +"/400";
        state_HP.GetComponent<Image>().fillAmount =0.0025f*HP;
    }
}
