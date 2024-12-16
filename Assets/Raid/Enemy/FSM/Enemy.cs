using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Skills")]
    public int skills; // how many skills
    public int currentSkillNumber;
    public float attackRate01;
    public float attackRange01;
    public float coolDown01;

    public float attackRate02;
    public float attackRange02;
    public float coolDown02;

    public float attackRate03;
    public float attackRange03;
    public float coolDown03;

    //public PlayerAgent playerAgent;
    public Transform[] playerTransform;
    public MeshCollider meshCollider;
    private NavMeshAgent agent;
    public Animator animator;
    private AudioSource audioSource;

    [Tooltip("HP")] public float enemyHealth;
    //public int TimesHitedByPlayer;
    [Tooltip("Hp slider")] public Slider slider;
    [Tooltip("Damage Text UI")] public TMP_Text getDamageText;
    [Tooltip("Death effect")] public GameObject deathEffect;

    public GameObject[] wayPointObj; //enemy patrol path
    public List<Vector3> wayPoints = new List<Vector3>(); //enemy patrol points for each path
    public int index;

    public int animeState; //0:idle; 1:run; 2: attack;
    public Transform targetPoint;

    public EnemyBaseState currentState; //enemy current state
    public PatrolState patrolState;
    public AttackState attackState;

    Vector3 targetPosition;

    public List<Transform> attackList = new List<Transform>();
    public float attackRate;
    private float nextAttack = 0;
    public float attackRange;
    public float distance;//distance to find attack object

    public bool isDead; //enemy is dead or not
    public bool isAttacking;
    public bool isAttackedByPlayer;

    public GameObject attackParticle01;
    public Transform attackParticle01Position;
    public AudioClip attackSound;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        patrolState = transform.gameObject.AddComponent<PatrolState>();
        attackState = transform.gameObject.AddComponent<AttackState>();
        meshCollider = GetComponent<MeshCollider>();
    }

    void Start()
    {
        currentSkillNumber = 0;
        isAttackedByPlayer = false;
        distance = 5f;
        
        isDead = false;
        slider.minValue = 0;
        slider.maxValue = enemyHealth;
        slider.value = enemyHealth;
        index = 0;
        //TimesHitedByPlayer = 0;
        //enemy will patrol at the begining
        TransitionToState(patrolState);
    }

    void Update()
    {
        if(isDead) return;
        currentState.OnUpdate(this);
        animator.SetInteger("state", animeState);
        AddAttackObj();
        //Debug.Log(currentSkillNumber);
    }

    public void MoveToTarget()
    {
        if(attackList.Count == 0) //no enemy to attack
        {
            targetPosition = Vector3.MoveTowards(transform.position, wayPoints[index], agent.speed * Time.deltaTime);
        }
        else //have enemy to chase
        {
            targetPosition = Vector3.MoveTowards(transform.position, attackList[0].transform.position, agent.speed * Time.deltaTime);
        }
       
        agent.destination = targetPosition;

    }

    public void LoadPath(GameObject go)
    {
        //clear list before loading path
        wayPoints.Clear();
        //add all waypoints to list
        foreach(Transform T in go.transform)
        {
            //Debug.Log("1");
            wayPoints.Add(T.position);
        }
    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnemyState(this);
    }

    public void Health(float damage)
    {
        //TimesHitedByPlayer++;
        if(isDead) return;
        isAttackedByPlayer = true;
        getDamageText.text = Mathf.Round(damage).ToString();
        enemyHealth -= damage;
        slider.value = enemyHealth;
        if(slider.value <= 0)
        {
            isDead = true;
            //animator.SetBool("isDead",true);
            //Destroy(this.gameObject, 5f);
        }
    }

    public void AttackAction()
    {
        if(this.gameObject.name == "Mutant")
        {
            MultipleAttackAction();
        }
        else if(Vector3.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if(Time.time > nextAttack)
            {
                //use attack animation
                animator.SetTrigger("attack01");
                //refresh next attack timer
                nextAttack = Time.time + attackRate;
            }
        }
    }

    public void MultipleAttackAction()
    {
        AnimatorStateInfo info =  animator.GetCurrentAnimatorStateInfo(1);
        var skillNumber = Random.Range(1, skills + 1);
        switch(skillNumber)
        {
            case 1:
                if(Vector3.Distance(transform.position, targetPoint.position) < attackRange01 && Time.time > coolDown01)
                {
                    currentSkillNumber = skillNumber;
                    //use attack animation
                    animator.SetTrigger("attack01");
                    //refresh next attack timer
                    coolDown01 = Time.time + attackRate01;
                    return;
                }
                else
                {
                    return;
                }
                
            case 2:
                if(Vector3.Distance(transform.position, targetPoint.position) < attackRange02 && Time.time > coolDown02)
                {
                    currentSkillNumber = skillNumber;
                    //use attack animation
                    animator.SetTrigger("attack02");
                    //refresh next attack timer
                    coolDown02 = Time.time + attackRate02;
                    return;
                }
                else
                {
                    return;
                }
            case 3:
                if(Vector3.Distance(transform.position, targetPoint.position) < attackRange03 && Time.time > coolDown03)
                {
                    currentSkillNumber = skillNumber;
                    //use attack animation
                    animator.SetTrigger("attack03");
                    //refresh next attack timer
                    coolDown03 = Time.time + attackRate03;
                    return;
                }
                else
                {
                    return;
                }
            default:
                return;
        }
        
        
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }

    public void PlayAttackEffect()
    {
        if(gameObject.name == "Mutant")
        {
            GameObject particle01 = Instantiate(attackParticle01, attackParticle01Position.position, attackParticle01Position.rotation);
            PlayAttackSound();
            Destroy(particle01, 3f);
        }
    }

    private void AddAttackObj()
    {
        if((Vector3.Distance(playerTransform.position, this.transform.position) <= distance || 
            isAttackedByPlayer)
            && attackList.Count == 0)
        {
            attackList.Add(playerTransform);
        }
        
        
    }

}
