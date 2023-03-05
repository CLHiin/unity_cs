using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2 : MonoBehaviour{
    [SerializeField] GameObject player;
    [SerializeField] GameObject state_HP;
    [SerializeField] Text state_text;
    int HP = 200;
    float animtor_show = 0;
    float attact_show = 0;
    float attact_CD = 0;
    float movespeed = 4;
    float play_x=-10;
    float play_y=-10;
    float x;
    float y;
    void Update(){
        play_x=player.transform.position.x;
        play_y=player.transform.position.y;
        x=transform.position.x;
        y=transform.position.y;

        if( HP <= 0 ) Destroy(gameObject);
        keydown();
        if(attact_CD > 0 ) attact_CD -= Time.deltaTime;
        if(attact_show <=0) GetComponent<Animator>().SetBool("攻擊",false);
        else attact_show -= Time.deltaTime;
        transform.Translate(0,0.01f*Time.deltaTime,0);
        if     (attact_show > 1.5 && attact_show <=1.5+2*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(true);
        else if(attact_show > 0.7 && attact_show <=0.7+2*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(true);
        else if(attact_show > 0.5 && attact_show <=0.5+2*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(true);

        if     (attact_show < 1.8 && attact_show >=1.8-2*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(false);
        else if(attact_show < 0.7 && attact_show >=0.7-2*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(false);
        else if(attact_show < 0.5 && attact_show >=0.5-2*Time.deltaTime ) transform.GetChild(0).gameObject.SetActive(false);

        if ( attact_show < 0.7 && attact_show > 0 ) transform.GetChild(1).gameObject.SetActive(true);
        else {
            transform.GetChild(1).gameObject.SetActive(false);
            player.transform.rotation = Quaternion.Euler (0, 0, 0);
            player.GetComponent<player>().control = false;
        }

        transform.Translate(0,0.01f*Time.deltaTime,0);
        if( player.GetComponent<player>().control ){
            if ( GetComponent<SpriteRenderer>().flipX ){
                player.transform.position = new Vector3( x - 1.5f , y + 0.7f , 0 );
                player.transform.rotation = Quaternion.Euler (0, 0, 14f);
            }
            else{
                player.transform.position = new Vector3( x + 2 , y + 0.7f , 0 );
                player.transform.rotation = Quaternion.Euler (0, 0, -14f);
            }
        }
    }

    void keydown(){
        GetComponent<Animator>().SetBool("移動",false);
        if(animtor_show <= 0 && attact_show <= 0 && 100 >= ( System.Math.Pow(play_x - x , 2) + System.Math.Pow(play_y - y , 2) ) ){
            if( ( System.Math.Pow(play_x - x , 2 ) + System.Math.Pow(play_y - y , 2) ) >= 4){
                GetComponent<Animator>().SetBool("移動",true);
                if ( play_x > x ){
                    transform.Translate(movespeed*Time.deltaTime,0,0);
                    GetComponent<SpriteRenderer>().flipX = false;
                    transform.GetChild(0).transform.position= new Vector3( x + 0.5f , y , 0 );
                    transform.GetChild(1).transform.position= new Vector3( x + 0.5f , y , 0 );
                }
                else {
                    transform.Translate(-movespeed*Time.deltaTime,0,0);
                    GetComponent<SpriteRenderer>().flipX = true;
                    transform.GetChild(0).transform.position= new Vector3( x - 1f , y , 0 );
                    transform.GetChild(1).transform.position= new Vector3( x - 1f , y , 0 );
                }
                if ( play_y > y ) transform.Translate(0,movespeed*Time.deltaTime,0);
                else transform.Translate(0,-movespeed*Time.deltaTime,0);
            }
            else if (attact_CD <= 0) {
                GetComponent<Animator>().SetBool("攻擊",true);
                attact_show=1.833f;
                attact_CD=2.5f;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        hurt(player.GetComponent<player>().HP_hurt(other.gameObject.tag));
    }
    void hurt(int i){
        HP-=i;
        state_text.text = HP +"/200";
        state_HP.GetComponent<Image>().fillAmount =0.005f*HP;

    }
}
