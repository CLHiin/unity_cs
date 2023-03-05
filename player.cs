using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour{
    [SerializeField] GameObject teach;
    [SerializeField] GameObject start;
    [SerializeField] GameObject stop;
    [SerializeField] GameObject state;
    [SerializeField] GameObject state2;
    [SerializeField] GameObject HPbar;
    [SerializeField] GameObject MPbar;
    [SerializeField] GameObject cannonball;
    [SerializeField] GameObject skill_1;
    [SerializeField] GameObject skill_2;
    [SerializeField] GameObject skill_3;
    [SerializeField] GameObject skill_4;
    [SerializeField] Text Hptext;
    [SerializeField] Text MPtext;
    [SerializeField] Text skill_1_text;
    [SerializeField] Text skill_2_text;
    [SerializeField] Text skill_3_text;
    [SerializeField] Text skill_4_text;
    GameObject m;
    public bool control = false;
    public bool timesleep = true;
    public int HP = 100;
    public int MP = 100;
    int attact_stage = 0;
    int teach_W = 0;
    float x;
    float y;

    float movespeed = 5f;
    float MP_up = 0;
    float HP_up = 0;

    float animtor_show = 0;
    float attact_show = 0;
    float attact_CD = 0;
    float skill_1_CD = 0;
    float skill_2_CD = 0;
    float skill_3_CD = 0;
    float skill_4_CD = 0;
    void Update(){
        if ( timesleep) Time.timeScale=0;
        else if ( Input.GetKey(KeyCode.P) ) Stop();
        else{
            Time.timeScale=1;
            x=transform.position.x;
            y=transform.position.y;
            if(MP_up>=1){
                MP+=1;
                MP_up=0;
            }
            if(HP<100) HP_up+=0.2f*Time.deltaTime;
            if(HP_up>=1){
                HP+=1;
                HP_up=0;
            }
            if(MP<100) MP_up+=0.5f*Time.deltaTime;
            if(!control) keydown();
            GetComponent<Animator>().SetInteger("attact",attact_stage);
            GetComponent<Animator>().SetFloat("show",attact_show);
            if(animtor_show >0) animtor_show -= Time.deltaTime;
            if(attact_show <=0) transform.GetChild(0).gameObject.SetActive(false);
            else attact_show -= Time.deltaTime;
            if(attact_CD <=0) attact_stage = 0;
            else attact_CD -= Time.deltaTime;

            if(skill_1_CD>0){
                if(skill_1_CD>1){
                    if     (!GetComponent<SpriteRenderer>().flipX && x < 13f)transform.Translate( 4*movespeed*Time.deltaTime,0,0);
                    else if( GetComponent<SpriteRenderer>().flipX && x >-13f)transform.Translate(-4*movespeed*Time.deltaTime,0,0);
                }
                else GetComponent<Animator>().SetBool("衝刺",false);
                skill_1_CD-=Time.deltaTime;
            }
            if(skill_2_CD>0){
                skill_2_CD -= Time.deltaTime;
                transform.GetChild(1).gameObject.SetActive(true);
            }
            else transform.GetChild(1).gameObject.SetActive(false);
            if(skill_3_CD>0){
                skill_3_CD-=Time.deltaTime;
                if( skill_3_CD < 15 - 0.5f   && skill_3_CD > 15 - 0.5f - 2*Time.deltaTime)GetComponent<Animator>().SetBool("技能3",false);
                else if( skill_3_CD < 15 - 1.6f  && skill_3_CD > 15 - 1.6f - 2*Time.deltaTime) Destroy(m);
                else if( skill_3_CD < 15 && skill_3_CD > 15 - 1.1f*Time.deltaTime){
                    m = Instantiate(transform.GetChild(2).gameObject , cannonball.transform);
                    m.transform.localScale = new Vector3(2.5f,  2.5f, 1);
                    if(GetComponent<SpriteRenderer>().flipX){
                        m.transform.position= new Vector3( x - 0.18f, y+0.5f , 0 );
                        m.GetComponent<SpriteRenderer>().flipX = false;
                    } 
                    else {
                        m.transform.position= new Vector3( x + 0.18f, y+0.5f , 0 );
                        m.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    m.SetActive(true);
                }
            }
            if(skill_4_CD>0){
                skill_4_CD-=Time.deltaTime;
                if( skill_4_CD < 60 - 1.33f   && skill_4_CD > 60 - 1.33f - 2*Time.deltaTime)GetComponent<Animator>().SetBool("技能4",false);
                else if( skill_4_CD < 60 - 0.17f   && skill_4_CD > 60 - 0.17f - 2*Time.deltaTime)transform.GetChild(3).gameObject.SetActive(true);
                else if( skill_4_CD < 60 - 1.00f   && skill_4_CD > 60 - 1.00f - 2*Time.deltaTime)transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        Hptext.text = HP +"/100";
        HPbar.GetComponent<Image>().fillAmount =0.01f*HP;
        MPtext.text = MP +"/100";
        MPbar.GetComponent<Image>().fillAmount =0.01f*MP;
        if(skill_1_CD<=0) skill_1_text.text = "L";
        else skill_1_text.text = System.Math.Round(skill_1_CD, 1)+"";
        skill_1.GetComponent<Image>().fillAmount =skill_1_CD/1.5f;
        if(skill_2_CD<=0) skill_2_text.text = "K";
        else skill_2_text.text = System.Math.Round(skill_2_CD, 1)+"";
        skill_2.GetComponent<Image>().fillAmount =skill_2_CD/10f;
        if(skill_3_CD<=0) skill_3_text.text = "U";
        else skill_3_text.text = System.Math.Round(skill_3_CD, 1)+"";
        skill_3.GetComponent<Image>().fillAmount =skill_3_CD/15f;
        if(skill_4_CD<=0) skill_4_text.text = "O";
        else skill_4_text.text = System.Math.Round(skill_4_CD, 1)+"";
        skill_4.GetComponent<Image>().fillAmount =skill_4_CD/60f;
    }
    void keydown(){
        GetComponent<Animator>().SetBool("run",false);
        if(animtor_show<=0){
            if ( Input.GetKey(KeyCode.D) ){
                if( x < 13) transform.Translate(movespeed*Time.deltaTime,0,0);
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<Animator>().SetBool("run",true);
                transform.GetChild(0).position= new Vector3( x + 0.2f , y , 0 );
                transform.GetChild(1).position= new Vector3( x - 0.18f , y-0.15f , 0 );
            }
            if ( Input.GetKey(KeyCode.A) ){
                if( x >-13f) transform.Translate(-movespeed*Time.deltaTime,0,0);
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<Animator>().SetBool("run",true);
                transform.GetChild(0).position= new Vector3( x - 0.2f , y , 0 );
                transform.GetChild(1).position= new Vector3( x + 0.18f , y-0.15f , 0 );
            }
            if ( Input.GetKey(KeyCode.W) ){
                if( y <1f) transform.Translate(0,movespeed*Time.deltaTime,0);
                GetComponent<Animator>().SetBool("run",true);
            }
            if ( Input.GetKey(KeyCode.S) ){
                if( y >-3.5f) transform.Translate(0,-movespeed*Time.deltaTime,0);
                GetComponent<Animator>().SetBool("run",true);
            }
            if ( Input.GetKey(KeyCode.L) && skill_1_CD<=0) {
                GetComponent<Animator>().SetBool("衝刺",true);
                animtor_show = 0.6f;
                MP-=5;
                skill_1_CD = 1.5f;
            }
            if ( Input.GetKey(KeyCode.K) && skill_2_CD<=0) {
                animtor_show = 0.1f;
                MP-=15;
                skill_2_CD = 10;
            }
            if ( Input.GetKey(KeyCode.U) && skill_3_CD<=0) {
                GetComponent<Animator>().SetBool("技能3",true);
                animtor_show = 0.5f;
                MP-=30;
                skill_3_CD = 15;
            }
            if ( Input.GetKey(KeyCode.O) &&  skill_4_CD<=0) {
                GetComponent<Animator>().SetBool("技能4",true);
                HP -= HP/10;
                animtor_show = 1.5f;
                MP-=60;
                skill_4_CD = 60;
            }
            if ( Input.GetKey(KeyCode.J)) {
                transform.Translate(0,0.01f*Time.deltaTime,0);
                transform.GetChild(0).gameObject.SetActive(true);
                if(attact_stage<=0){
                    attact_show=0.5f;
                    attact_stage=1;
                    animtor_show=0.5f;
                    attact_CD=1;
                }
                else if(attact_stage<=1){
                    attact_show=0.3f;
                    attact_stage=2;
                    animtor_show=0.3f;
                    attact_CD=0.5f;
                }
                else{
                    attact_show=0.3f;
                    attact_stage=3;
                    animtor_show=0.5f;
                    attact_CD=0.3f;
                }
            }
        }
    }
    public void Start_botton(){
        start.SetActive(false);//首頁消失
        gameObject.SetActive(true);//玩家顯示
        state.SetActive(true);
        state2.SetActive(true);
    }
    void Stop(){
        timesleep = true;
        stop.SetActive(true);//物件停止 顯示
    }
    public void keepgame(){
        timesleep = false;
        stop.SetActive(false);//物件停止 隱藏
    }
    public void teach_back() {
        teach.transform.GetChild(teach_W-1).gameObject.SetActive(false);
        teach_W = 0;
    }
    public void next_Teach(){
        if(teach_W < teach.transform.childCount){
            if(teach_W>0) teach.transform.GetChild(teach_W-1).gameObject.SetActive(false);
            teach.transform.GetChild(teach_W).gameObject.SetActive(true);
            teach_W+=1;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="hurt5"){
            HP_less(5);
        }
        if(other.gameObject.tag=="hurt10"){
            HP_less(10);
        }
        if(other.gameObject.tag=="控制") {
            control = true;
        }
        
    }
    void HP_less(int i){
        if(skill_1_CD > 1) HP -= 0;
        else if(skill_2_CD>0) HP -= i/2;
        else HP -= i;
    }
    public int HP_hurt(string i){
        if(i=="玩家攻擊10") return 10;
        else if(i=="玩家攻擊30") return 30;
        else if(i=="玩家攻擊100") return 100;
        else return 0;
    }
}