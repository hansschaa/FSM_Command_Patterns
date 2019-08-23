using UnityEngine;
using System.Collections;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigidbody;
    [HideInInspector] public Animator animator;

    [Header("States Parameters")] 
    public float idleTime;
    public float walkTime;
    public float walkSpeed;

    private Transform textMeshTransform;
    public TextMeshProUGUI textMeshPro;
    
    private FSM _fsm;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        textMeshTransform = textMeshPro.gameObject.transform;
        
        _fsm = new FSM(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeFSM();
        
    }

    private void MakeFSM()
    {
        IdleState idleState = new IdleState(this, StateID.IdleState);
        WalkState walkState = new WalkState(this, StateID.PatrolState, walkSpeed);
        
        //IdleState
        idleState.AddTransition(new TimeTransition(walkState,idleTime));
        
        //WalkState
        walkState.AddTransition(new TimeTransition(idleState,walkTime));
        
        _fsm.AddState(idleState);
        _fsm.AddState(walkState);
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.FSMUpdate();
        
         Vector2 cameraToWorld = Camera.main.WorldToScreenPoint(transform.position);
         textMeshTransform.position = new Vector2(cameraToWorld.x, cameraToWorld.y + 40);
    }

    

    public void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
        
    }

    public void UpdateStateView(FSMState currentState) => textMeshPro.text = currentState.StateId.ToString();
    
}
