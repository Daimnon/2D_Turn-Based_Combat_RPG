using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CombatStates { Waiting, Attacking, Resolving }

public class Character : Role, ICharacter
{
    private delegate void CombatState();
    private CombatState _combatState;

    private PlayerControls _playerControls;
    private InputAction _interact;

    [SerializeField] private CharacterData _data;
    public CharacterData Data => _data;

    //[SerializeField] private CharacterAnimations _animations;
    //[SerializeField] private SpriteRenderer _characterSpriteRenderer;
    
    [SerializeField] private Character _lastCharacterClickedOn;
    [SerializeField] private bool _isMyTurn = false, _isInCombat = false, _isAlive = true;
    private int _skillSlotToActivateNum;

    public Character LastCharacterClickedOn => _lastCharacterClickedOn;
    public bool MyTurn { get => _isMyTurn; set => _ = value; }
    public bool IsInCombat { get => _isInCombat; set => _ = value; }
    public bool IsAlive { get => _isAlive; set => _ = value; }
    public int SkillSlotToActivateNum { get => _skillSlotToActivateNum; set => _ = value; }

    private Camera _camera;
    private Mouse _cursor;
    private Vector2 _cursorPos;
    private bool _isSubscribedToCombatEvents, _isUnsubscribedFromCombatEvents;

    #region Debug
    private int _stateDebugCounter = 0;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        InitializeCharacter();
    }
    private void OnEnable()
    {
        if (this is Player)
        {
            _interact.Enable();
            _interact.performed += Interact;
        }
    }
    private void Update()
    {
        if (this is Player)
        {
            _cursorPos = _cursor.position.ReadValue();
        }

        if (_isInCombat)
        {
            _combatState.Invoke();

            if (!_isSubscribedToCombatEvents)
            {
                CombatManager.Instance.OnStartCombatByCharacter -= OnStartCombat;
                _isSubscribedToCombatEvents = true;
                _isUnsubscribedFromCombatEvents = false;
            }

            if (_stateDebugCounter == 0)
            {
                Debug.Log($"{_data.Name}'s combat state is {_combatState.Method.Name}");
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
    private void OnDisable()
    {
        if (this is Player)
        {
            _interact.Disable();
        }

        
    }
    #endregion

    #region Combat States
    //private void OutsideOfCombat() // while situation where combat do not take place
    //{
    //    // happens before the loop of the first frame where the condition is met
    //    if (_inCombat)
    //        _inCombat = false;
    //
    //    if (_myTurn)
    //        _myTurn = false;
    //
    //    if (_finishedCombat)
    //        _state = Waiting;
    //    // ---------------------------------------------------------------------
    //
    //
    //
    //
    //
    //    // happens after the loop of the first frame where the condition is met
    //    if (_startCombat)
    //        _startCombat = false;
    //}
    //private void InitializeCombat()
    //{
    //    _startCombat = true;
    //    _inCombat = true;
    //
    //    if (!_myTurn)
    //        _state = Waiting;
    //    else
    //        _state = Attacking;
    //}
    private void Waiting() // while waiting for this character's turn
    {
        // happens before the loop of the first frame where the condition is met

        // ---------------------------------------------------------------------

        // logic here
    
        // happens after the loop of the first frame where the condition is met

    }
    private void Attacking() // while this character's attacks
    {
        // happens before the loop of the first frame where the condition is met

        if (!_isMyTurn)
            return;

        // ---------------------------------------------------------------------

        // logic here

        // happens after the loop of the first frame where the condition is met

    }
    private void Resolving() // after this character's has being attacked
    {
        // happens before the loop of the first frame where the condition is met
        if (_isMyTurn)
            return;
    
        if (_data.CurrentHealth <= 0)
            Die();

        //if (_finishedResolving)
        //    _state = Waiting;
        // ---------------------------------------------------------------------

        // logic here

        // happens after the loop of the first frame where the condition is met

    }
    #endregion

    #region Combat Events
    public void OnStartCombat(Character invokerC) // occurs when entering combat.
    {   
        if (invokerC == this)
        {
            InitializeCombat();
        }
    }
    public void OnStartTurn(Character invokerC) // occurs when this character's turn has started.
    {   
        if (invokerC == this)
        {
            ChangeCombatState(CombatStates.Attacking);
        }
    }
    public void OnAttack(Character invokerC) // occurs before the ability strikes. Usually reserved for reaction effects that modify the ability attributes.
    {
        if (invokerC == this)
        {

        }  
    }
    public void OnAttackHit(Character invokerC) // occurs when the ability scores a hit.
    {
        if (invokerC == this)
        {

        }
    }
    public void OnAttackMiss(Character invokerC) // occurs when the ability doesn't score a hit (i.e. a "Miss"). Likely not used by the game at all.
    {
        if (invokerC == this)
        {

        } 
    }
    public void OnAttackHitCrit(Character invokerC) // occurs when the ability scores a crit. The ability has to score a hit first before the game checks for critical hits.
    {
        if (invokerC == this)
        {

        }  
    }
    public void OnAttackKill(Character invokerC) // occurs when the ability kills an enemy. The ability has to score a hit first before the game checks for killing blows.
    {
        if (invokerC == this)
        {
            // add exp to combat conclusion
        }
    }
    public void OnAttackResolve(Character invokerC) // occurs after the ability made it's last strike. Unlike the other phases, this one will occur exactly once per ability execution, regardless of how many times the ability strikes.
    {
        if (invokerC == this)
        {
            ChangeCombatState(CombatStates.Resolving);
        }
    }
    public void OnDeath(Character invokerC) // occurs when current hp reach 0.
    {
        if (invokerC == this)
        {
            Die();
        }
    }
    public void OnEndTurn(Character invokerC) // occurs when this character's turn has ended.
    {
        if (invokerC == this)
        {
            ChangeCombatState(CombatStates.Waiting);
        }
    }
    public void OnEndCombat(Character invokerC) // occurs if player survived the combat and all enemies are dealt with.
    {
        if (invokerC == this)
        {
            _isInCombat = false;
        }
    }

    private void SubscribeCombatEventsExceptOnStartCombat()
    {
        CombatManager.Instance.OnStartTurnByCharacter += OnStartTurn;
        CombatManager.Instance.OnAttackByCharacter += OnAttack;
        CombatManager.Instance.OnAttackHitByCharacter += OnAttackHit;
        CombatManager.Instance.OnAttackMissByCharacter += OnAttackMiss;
        CombatManager.Instance.OnAttackHitCritByCharacter += OnAttackHitCrit;
        CombatManager.Instance.OnAttackKillByCharacter += OnAttackKill;
        CombatManager.Instance.OnAttackResolveByCharacter += OnAttackResolve;
        CombatManager.Instance.OnDeathByCharacter += OnDeath;
        CombatManager.Instance.OnEndTurnByCharacter += OnEndTurn;
        CombatManager.Instance.OnEndCombatByCharacter += OnEndCombat;
    }
    private void UnSubscribeAllCombatEvents()
    {
        CombatManager.Instance.OnStartCombatByCharacter -= OnStartCombat;
        CombatManager.Instance.OnStartTurnByCharacter -= OnStartTurn;
        CombatManager.Instance.OnAttackByCharacter -= OnAttack;
        CombatManager.Instance.OnAttackHitByCharacter -= OnAttackHit;
        CombatManager.Instance.OnAttackMissByCharacter -= OnAttackMiss;
        CombatManager.Instance.OnAttackHitCritByCharacter -= OnAttackHitCrit;
        CombatManager.Instance.OnAttackKillByCharacter -= OnAttackKill;
        CombatManager.Instance.OnAttackResolveByCharacter -= OnAttackResolve;
        CombatManager.Instance.OnDeathByCharacter -= OnDeath;
        CombatManager.Instance.OnEndTurnByCharacter -= OnEndTurn;
        CombatManager.Instance.OnEndCombatByCharacter -= OnEndCombat;
    }
    #endregion

    private void InitializeCharacter()
    {
        if (this is Player)
        {
            _playerControls = new PlayerControls();
            _camera = Camera.main;
            _cursor = Mouse.current;
            _interact = _playerControls.Player.Interact;

            // to remove
            _combatState = Attacking;
        }
        else if (this is Ally)
        {

        }
        else
        {

        }

        //_characterSpriteRenderer.material.mainTexture = _data.SpriteSheet;
        //_characterSpriteRenderer.sprite = _data.SpriteSheet.

        /* temp comment -----------
        _state = OutsideOfCombat;

        _state = Attacking;
        ------------------------- */
    }
    private void InitializeCombat()
    {
        SubscribeCombatEventsExceptOnStartCombat();

        if (!_isMyTurn)
            _combatState = Waiting;
        else
            _combatState = Attacking;
    }
    public void Interact(InputAction.CallbackContext interactContext)
    {
        // if not player ignore
        if (!(this is Player))
            return;

        // get character on click --------------------------------------------------------------
        Ray ray = _camera.ScreenPointToRay(_cursorPos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (_isInCombat)
        {
            if (hit.collider && hit.transform.root.TryGetComponent(out Character targetCharacter))
            {
                _lastCharacterClickedOn = targetCharacter;

                if (_isInCombat && _isMyTurn && _combatState == Attacking)
                    OpenSkillMenu();
                else if (_isInCombat && !_isMyTurn && _combatState == Waiting) ;

                Debug.Log($"Clicked on {_lastCharacterClickedOn}");
            }
        }
        else
        {
            _lastCharacterClickedOn = null;
        }
        // -------------------------------------------------------------------------------------

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

    #region ICharacter
    public void OpenSkillMenu()
    {
        if (!_lastCharacterClickedOn)
            return;

        UIManager.Instance.RefreshCombatSkillMenuDisplay(this, _lastCharacterClickedOn, _camera.WorldToScreenPoint(_lastCharacterClickedOn.transform.position), _data.ActiveSkills, _lastCharacterClickedOn._data.CurrentLevel);

        switch (UIManager.Instance.CombatSkillMenu.gameObject.activeInHierarchy)
        {
            case true:
                UIManager.Instance.CombatSkillMenu.gameObject.SetActive(false);
                UIManager.Instance.CombatSkillMenu.SkillsParent.SetActive(false);
                break;
            case false:
                UIManager.Instance.CombatSkillMenu.gameObject.SetActive(true);
                UIManager.Instance.CombatSkillMenu.SkillsParent.SetActive(true);
                break;
        }
    }
    public void ActivateSkill()
    {
        _data.ActiveSkills[SkillSlotToActivateNum].Activate();
    }
    public void Die()
    {
        
    }
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
