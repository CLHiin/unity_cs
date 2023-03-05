using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 小怪骷髏3 : MonoBehaviour{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cannonball;
    [SerializeField] GameObject state_HP;
    [SerializeField] Text state_text;
    float attact_show = 0;
    float movespeed = 2.5f;
    bool attact =true;
    int HP = 50;

    void Update(){
        keydown();
        transform.Translate(0,0.01f*Time.deltaTime,0);
        if ( HP <= 0 ) Destroy(gameObject);
        if (attact_show > 0) attact_show -= Time.deltaTime;
        else GetComponent<Animator>().SetBool("攻擊",false);
        if(attact_show > 0 && attact_show < Time.deltaTime ) product(attact);

    }

    void keydown(){
        if( attact_show <= 0 ){
            if ( player.transform.position.x > transform.position.x ) GetComponent<SpriteRenderer>().flipX = true;
            else GetComponent<SpriteRenderer>().flipX = false;
            if(System.Math.Pow(player.transform.position.y - transform.position.y , 2 ) >= 0.25f ||
                System.Math.Pow(player.transform.position.x - transform.position.x , 2 ) >= 25){
                if ( player.transform.position.x > transform.position.x ) transform.Translate( movespeed*Time.deltaTime,0,0);
                if ( player.transform.position.x < transform.position.x ) transform.Translate(-movespeed*Time.deltaTime,0,0);
                if ( player.transform.position.y > transform.position.y ) transform.Translate(0, movespeed*Time.deltaTime,0);
                if ( player.transform.position.y < transform.position.y ) transform.Translate(0,-movespeed*Time.deltaTime,0);

            }
            else{
                attact=GetComponent<SpriteRenderer>().flipX;
                attact_show=1.42f;
                GetComponent<Animator>().SetBool("攻擊",true);
            }
        }
    }
    void product(bool i){
        GameObject m = Instantiate(transform.GetChild(0).gameObject,cannonball.transform);
        m.transform.localScale = new Vector3(1.5f*0.5f,  1.5f*0.5f, 1);
        if(i){
            m.GetComponent<箭矢>().movespeed = 8;
            m.transform.position=new Vector3( transform.position.x +0.21f , transform.position.y -0.12f, 0 );
        } 
        else {
            m.GetComponent<箭矢>().movespeed = -8;
            m.transform.position=new Vector3( transform.position.x -0.21f , transform.position.y -0.12f, 0 );
        } 
        m.SetActive(true);
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
