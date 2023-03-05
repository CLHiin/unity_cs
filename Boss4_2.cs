using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss4_2 : MonoBehaviour{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cannonball;
    [SerializeField] GameObject state_HP;
    [SerializeField] Text state_text;
    float attact_show = 0;
    float attact_CD = 0;
    float movespeed = 1f;
    float flipX , flipY ;
    bool attact =true;
    int HP = 400;

    void Update(){
        state_text.text = HP +"/400";
        keydown();
        transform.Translate(0,0.01f*Time.deltaTime,0);
        if ( HP <= 0 ) Destroy(gameObject);
        if (attact_show > 0.0f && attact_show <= 0.0f + Time.deltaTime ) for(int i=0; i<5;i++) product(i,attact);
        if (attact_show > 0.5f && attact_show <= 0.5f + Time.deltaTime ) for(int i=0; i<5;i++) product(i,attact);
        if (attact_show > 1.0f && attact_show <= 1.0f + Time.deltaTime ) for(int i=0; i<5;i++) product(i,attact);
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
                flipY=2;
                attact=GetComponent<SpriteRenderer>().flipX;
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
    void product(int i,bool j){
        GameObject m = Instantiate(transform.GetChild(0).gameObject , cannonball.transform);
        if(j){
            m.GetComponent<砲彈2>().movespeed = 5;
            m.transform.position=new Vector3( transform.position.x + 1 , transform.position.y -0.6f, 0 );
        } 
        else {
            m.GetComponent<砲彈2>().movespeed = -5;
            m.transform.position=new Vector3( transform.position.x - 1 , transform.position.y -0.6f, 0 );
        }
             if(i==0) m.GetComponent<砲彈2>().flipY = 0.5f;
        else if(i==1) m.GetComponent<砲彈2>().flipY = 0.25f;
        else if(i==2) m.GetComponent<砲彈2>().flipY = 0;
        else if(i==3) m.GetComponent<砲彈2>().flipY =-0.25f;
        else if(i==4) m.GetComponent<砲彈2>().flipY =-0.5f;

        m.SetActive(true);
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
