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
    public bool MyTurn { get => _isMyTurn; set => _isMyTurn = value; }
    public bool IsInCombat { get => _isInCombat; set => _isInCombat = value; }
    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    public int SkillSlotToActivateNum { get => _skillSlotToActivateNum; set => _skillSlotToActivateNum = value; }

    private Camera _camera;
    private Mouse _cursor;
    private Vector2 _cursorPos;

    #region Debug
    private int _stateDebugCounter = 0;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        this.Initialize();
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

            if (_stateDebugCounter == 0)
            {
                Debug.Log($"{_data.Name}'s combat state is {_combatState.Method.Name}");
                _stateDebugCounter++;
            }
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

        //if (_finishedCombat)
        //    _state = OutsideOfCombat;
    
        //if (_myTurn && _finishedWaiting)
        //    _state = Attacking;
        // ---------------------------------------------------------------------
    
    
    
    
    
        // happens after the loop of the first frame where the condition is met
        //if (_startWaiting)
        //    _startWaiting = false;
    }
    private void Attacking() // while this character's attacks
    {
        // happens before the loop of the first frame where the condition is met
        if (!_isMyTurn)
            return;
    
        //if (_finishedCombat)
        //    _state = OutsideOfCombat;
    
        //if (_finishedAttacking)
        //    _state = Waiting;
        // ---------------------------------------------------------------------
    
    
    
    
    
        // happens after the loop of the first frame where the condition is met
        //if (_startAttacking)
        //    _startAttacking = false;
    }
    private void Resolving() // after this character's has being attacked
    {
        // happens before the loop of the first frame where the condition is met
        if (_isMyTurn)
            return;
    
        if (_data.CurrentHealth <= 0)
            Die();
    
        //if (_finishedCombat)
        //    _state = OutsideOfCombat;
    
        //if (_finishedResolving)
        //    _state = Waiting;
        // ---------------------------------------------------------------------
    
    
    
    
    
    
        // happens after the loop of the first frame where the condition is met
        //if (_startResolving)
        //    _startResolving = false;
    }
    #endregion

    #region Combat Events
    public void OnStartCombat()
    {
        // occurs when entering combat.
    }
    public void OnStartTurn()
    {
        // occurs when this character's turn has started.
    }
    public void OnAttack()
    {
        // occurs before the ability strikes. Usually reserved for reaction effects that modify the ability attributes.
    }
    public void OnAttackHit()
    {
        // occurs when the ability scores a hit.
    }
    public void OnAttackMiss()
    {
        // occurs when the ability doesn't score a hit (i.e. a "Miss"). Likely not used by the game at all.
    }
    public void OnAttackHitCrit()
    {
        // occurs when the ability scores a crit. The ability has to score a hit first before the game checks for critical hits.
    }
    public void OnAttackKill()
    {
        // occurs when the ability kills an enemy. The ability has to score a hit first before the game checks for killing blows.
    }
    public void OnAttackResolve()
    {
        // occurs after the ability made it's last strike. Unlike the other phases, this one will occur exactly once per ability execution, regardless of how many times the ability strikes.
    }
    public void OnDeath()
    {
        // occurs when current hp reach 0.
    }
    public void OnEndTurn()
    {
        // occurs when this character's turn has ended.
    }
    public void OnEndCombat()
    {
        // occurs if player survived the combat and all enemies are dealt with.
    }
    #endregion

    private void Initialize()
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
                _combatState = Waiting;
                break;
            case CombatStates.Attacking:
                _combatState = Attacking;
                break;
            case CombatStates.Resolving:
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
        // die
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
