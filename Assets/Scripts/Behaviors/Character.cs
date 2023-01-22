using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CombatStates { Waiting, Attacking, Resolving }

public class Character : Role
{
    protected delegate void CombatState();
    protected CombatState _combatState;

    [SerializeField] protected CharacterData _data;
    public CharacterData Data { get => _data; set => _ = value; }

    [SerializeField] protected float _originalPosX;
    public float OriginalPosX => _originalPosX;

    [SerializeField] protected Transform _attackerPosTr;
    public Transform AttackerPosTr => _attackerPosTr;

    //[SerializeField] private CharacterAnimations _animations;
    //[SerializeField] private SpriteRenderer _characterSpriteRenderer;

    [Header("Balancing")]
    [SerializeField] protected float _slideSpeed;

    [Header("Conditions")]
    [SerializeField] protected bool _isInCombat = false, _isMyTurn = false, _isAttackMelee = true, _didWeaponMadeContactWithOpponent = false, _isAlive = true;

    protected int _skillSlotToActivateNum;

    public bool IsInCombat { get => _isInCombat; set => _ = value; }
    public bool MyTurn { get => _isMyTurn; set => _ = value; }
    public bool IsAttackMelee { get => _isAttackMelee; set => _ = value; }
    public bool IsAlive { get => _isAlive; set => _ = value; }
    public int SkillSlotToActivateNum { get => _skillSlotToActivateNum; set => _ = value; }

    protected bool _isSubscribedToCombatEvents, _isUnsubscribedFromCombatEvents;

    #region Debug
    protected int _stateDebugCounter = 0;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        InitializeCharacter();
    }
    private void Update()
    {
        if (_isInCombat)
        {
            _combatState.Invoke();

            if (!_isSubscribedToCombatEvents)
            {
                //_isUnsubscribedFromCombatEvents = false;
                //
                //if (this is Enemy && !CombatManager.Instance.EnemyParty[0])
                //    CombatManager.Instance.EnemyParty[0] = this;
                //else if (this is Enemy && !CombatManager.Instance.EnemyParty[1])
                //    CombatManager.Instance.EnemyParty[1] = this;
                //else if (this is Enemy && !CombatManager.Instance.EnemyParty[0])
                //    CombatManager.Instance.EnemyParty[2] = this;
                //else if (this is Player)
                //    CombatManager.Instance.PlayerParty[0] = this;
                //else if (!CombatManager.Instance.PlayerParty[1])
                //    CombatManager.Instance.PlayerParty[1] = this;
                //else if (!CombatManager.Instance.PlayerParty[0])
                //    CombatManager.Instance.PlayerParty[2] = this;
                //
                //_isSubscribedToCombatEvents = true;
                //CombatManager.Instance.OnStartCombatByCharacter -= OnStartCombat;
            }

            if (_stateDebugCounter == 0)
            {
                Debug.Log($"{name}'s combat state is {_combatState.Method.Name}");
                _stateDebugCounter++;
            }
        }
        else if (!_isUnsubscribedFromCombatEvents)
        {
            UnSubscribeAllCombatEvents();
            _isUnsubscribedFromCombatEvents = true;
            _isSubscribedToCombatEvents = false;
        }
    }
    private void OnDestroy()
    {
        if (!_isUnsubscribedFromCombatEvents)
            UnSubscribeAllCombatEvents();
    }
    #endregion

    #region Combat States
    protected virtual void Waiting() // while waiting for this character's turn
    {
        // happens before the loop of the first frame where the condition is met

        // ---------------------------------------------------------------------

        // logic here
    
        // happens after the loop of the first frame where the condition is met

    }
    protected virtual void Attacking() // while this character's is attacking
    {
        // happens before the loop of the first frame where the condition is met
        if (!_isMyTurn)
            return;

        /*if (!LastCharacterClickedOn)
            return;*/
        // ---------------------------------------------------------------------

        
        if (_didWeaponMadeContactWithOpponent && transform.position.x >= OriginalPosX)
        {
            transform.position = new(OriginalPosX, transform.position.y, transform.position.z);
            _didWeaponMadeContactWithOpponent = false;
            ChangeCombatState(CombatStates.Waiting);
        }
        if (_didWeaponMadeContactWithOpponent)
        {
            SlideTowardOriginalPos();
        }
        /*else if (transform.position == LastCharacterClickedOn.AttackerPosTr.position && CheckWeaponCollisionWithOpponent())
        {
            _didWeaponMadeContactWithOpponent = CheckWeaponCollisionWithOpponent() ? TryHitOpponent() : false;

            if (_didWeaponMadeContactWithOpponent)
                CombatManager.Instance.InvokeAttackResolveByOpponent(_lastCharacterClickedOn);
        }
        else if (transform.position == LastCharacterClickedOn.AttackerPosTr.position && !CheckWeaponCollisionWithOpponent()) // in attacker position and weapon did not contact opponent
        {
            return;

            // try to hit
            // invoke correct combat event
            //
        }
        else if (transform.position.x > LastCharacterClickedOn.AttackerPosTr.position.x)
        {
            transform.position = LastCharacterClickedOn.AttackerPosTr.position;
        }
        else if (!(transform.position.x >= LastCharacterClickedOn.AttackerPosTr.position.x))
        {
            if (_isAttackMelee)
            {
                SlideTowardsOpponentAttackerPos();
            }
            else
            {

            }
        }*/

        // happens after the loop of the first frame where the condition is met

    }
    protected virtual void Resolving() // after this character's has being attacked
    {
        // happens before the loop of the first frame where the condition is met
        if (_isMyTurn)
            return;
    
        /*if (_data.CurrentHealth <= 0)
            Die();*/

        //if (_finishedResolving)
        //    _state = Waiting;
        // ---------------------------------------------------------------------

        // logic here

        // happens after the loop of the first frame where the condition is met

    }
    #endregion

    #region Combat Events
    public virtual void OnStartTurn(Character invokerC) // occurs when this character's turn has started.
    {   
        if (invokerC == this)
        {
            ChangeCombatState(CombatStates.Waiting);
        }
    }
    public virtual void OnAttack(Character invokerC) // occurs before the ability strikes. Usually reserved for reaction effects that modify the ability attributes.
    {
        if (invokerC == this)
        {
            _combatState = Attacking;
        }  
    }
    public virtual void OnAttackHit(Character invokerC) // occurs when the ability scores a hit.
    {
        if (invokerC == this)
        {

        }
    }
    public virtual void OnAttackMiss(Character invokerC) // occurs when the ability doesn't score a hit (i.e. a "Miss"). Likely not used by the game at all.
    {
        if (invokerC == this)
        {

        } 
    }
    public virtual void OnAttackHitCrit(Character invokerC) // occurs when the ability scores a crit. The ability has to score a hit first before the game checks for critical hits.
    {
        if (invokerC == this)
        {

        }  
    }
    public virtual void OnAttackKill(Character invokerC) // occurs when the ability kills an enemy. The ability has to score a hit first before the game checks for killing blows.
    {
        if (invokerC == this)
        {
            // add exp to combat conclusion
        }
    }
    public virtual void OnAttackResolve(Character invokerC) // occurs after the ability made it's last strike. Unlike the other phases, this one will occur exactly once per ability execution, regardless of how many times the ability strikes.
    {
        if (invokerC == this)
        {
            ChangeCombatState(CombatStates.Resolving);
        }
    }
    public virtual void OnDeath(Character invokerC) // occurs when current hp reach 0.
    {
        if (invokerC == this)
        {
            //Die();
        }
    }
    public virtual void OnEndTurn(Character invokerC) // occurs when this character's turn has ended.
    {
        if (invokerC == this)
        {
            ChangeCombatState(CombatStates.Waiting);
        }
    }
    public virtual void OnEndCombat(Character invokerC) // occurs if player survived the combat and all enemies are dealt with.
    {
        if (invokerC == this)
        {
            _isInCombat = false;
        }
    }

    protected void SubscribeCombatEventsExceptOnStartCombat()
    {
        CombatManager.Instance.OnStartTurnByCharacter += OnStartTurn;
        CombatManager.Instance.OnAttackByCharacter += OnAttack;
        CombatManager.Instance.OnAttackHitByCharacter += OnAttackHit;
        CombatManager.Instance.OnAttackMissByCharacter += OnAttackMiss;
        CombatManager.Instance.OnAttackHitCritByCharacter += OnAttackHitCrit;
        CombatManager.Instance.OnAttackKillOpponent += OnAttackKill;
        CombatManager.Instance.OnAttackResolveByOpponent += OnAttackResolve;
        CombatManager.Instance.OnDeathByCharacter += OnDeath;
        CombatManager.Instance.OnEndTurnByCharacter += OnEndTurn;
        CombatManager.Instance.OnEndCombatByCharacter += OnEndCombat;
    }
    protected void UnSubscribeAllCombatEvents()
    {
        CombatManager.Instance.OnStartTurnByCharacter -= OnStartTurn;
        CombatManager.Instance.OnAttackByCharacter -= OnAttack;
        CombatManager.Instance.OnAttackHitByCharacter -= OnAttackHit;
        CombatManager.Instance.OnAttackMissByCharacter -= OnAttackMiss;
        CombatManager.Instance.OnAttackHitCritByCharacter -= OnAttackHitCrit;
        CombatManager.Instance.OnAttackKillOpponent -= OnAttackKill;
        CombatManager.Instance.OnAttackResolveByOpponent -= OnAttackResolve;
        CombatManager.Instance.OnDeathByCharacter -= OnDeath;
        CombatManager.Instance.OnEndTurnByCharacter -= OnEndTurn;
        CombatManager.Instance.OnEndCombatByCharacter -= OnEndCombat;
    }
    #endregion

    protected virtual void InitializeCharacter()
    {
        //_characterSpriteRenderer.material.mainTexture = _data.SpriteSheet;
        //_characterSpriteRenderer.sprite = _data.SpriteSheet.

        /* temp comment -----------
        _state = OutsideOfCombat;

        _state = Attacking;
        ------------------------- */
    }


    protected virtual void SlideTowardsOpponentAttackerPos()
    {
        //transform.position += _slideSpeed * Time.deltaTime * (Vector3)CombatManager.Instance.GetAttackDirection(this, LastCharacterClickedOn);
    }
    protected virtual void SlideTowardOriginalPos()
    {
        //transform.position += _slideSpeed * Time.deltaTime * _lastCharacterClickedOn.AttackerPosTr.position - new Vector3(_originalPosX, transform.position.y, transform.position.z);
    }
    protected virtual bool CheckWeaponCollisionWithOpponent()
    {
        bool didWeaponMadeContactWithOpponent = false; // didThisHappen ? yesItDid : NoItDidn't
        // if weapon didn't made contact with opponent condition should be true

        // check if weapon colided with opponent, if so break, if not continue

        return didWeaponMadeContactWithOpponent;
        
    }
    protected virtual bool TryHitOpponent()
    {
        return false;
    }

    
    public void ChangeCombatState(CombatStates desiredState)
    {
        if (!_isInCombat)
            return;

        switch (desiredState)
        {
            case CombatStates.Waiting:
                _stateDebugCounter = 0;
                _combatState = Waiting;
                break;
            case CombatStates.Attacking:
                _stateDebugCounter = 0;
                _combatState = Attacking;
                break;
            case CombatStates.Resolving:
                _stateDebugCounter = 0;
                _combatState = Resolving;
                break;
        }
    }

    #region Coroutines
    
    #endregion

    #region overrides
    public override string ToString()
    {
        return $"{_data.name}, Role: {_role}, Lvl: {_data.CurrentLevel}";
    }
    public override bool Equals(object other)
    {
        return base.Equals(other);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion
}
