using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character, IPlayer
{
    private PlayerControls _playerControls;
    private InputAction _interact;

    [SerializeField] private Character _lastCharacterClickedOn;
    public Character LastCharacterClickedOn => _lastCharacterClickedOn;

    private Camera _camera;
    private Mouse _cursor;
    private Vector2 _cursorPos;

    private void Awake()
    {
        _data = _data as PlayerData;
        Initialize();
    }
    private void OnEnable()
    {
        _interact.Enable();
        _interact.performed += Interact;
    }
    private void Update()
    {
        _cursorPos = _cursor.position.ReadValue();
    }
    private void OnDisable()
    {
        _interact.Disable();
    }

    #region Combat States
    protected override void Waiting() // while waiting for this character's turn
    {
        // happens before the loop of the first frame where the condition is met

        // ---------------------------------------------------------------------

        // logic here

        // happens after the loop of the first frame where the condition is met

    }
    protected override void Attacking() // while this character's is attacking
    {
        // happens before the loop of the first frame where the condition is met
        if (!_isMyTurn)
            return;

        if (!LastCharacterClickedOn)
            return;
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
        else if (transform.position == LastCharacterClickedOn.AttackerPosTr.position && CheckWeaponCollisionWithOpponent())
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
        }

        // happens after the loop of the first frame where the condition is met

    }
    protected override void Resolving() // after this character's has being attacked
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
    public override void OnStartTurn(Character invokerC) // occurs when this character's turn has started.
    {
        if (invokerC == this)
        {
            ChangeCombatState(CombatStates.Waiting);
        }
    }
    public override void OnAttack(Character invokerC) // occurs before the ability strikes. Usually reserved for reaction effects that modify the ability attributes.
    {
        if (invokerC == this)
        {
            _combatState = Attacking;
        }
    }
    public override void OnAttackHit(Character invokerC) // occurs when the ability scores a hit.
    {
        if (invokerC == this)
        {

        }
    }
    public override void OnAttackMiss(Character invokerC) // occurs when the ability doesn't score a hit (i.e. a "Miss"). Likely not used by the game at all.
    {
        if (invokerC == this)
        {

        }
    }
    public override void OnAttackHitCrit(Character invokerC) // occurs when the ability scores a crit. The ability has to score a hit first before the game checks for critical hits.
    {
        if (invokerC == this)
        {

        }
    }
    public override void OnAttackKill(Character invokerC) // occurs when the ability kills an enemy. The ability has to score a hit first before the game checks for killing blows.
    {
        if (invokerC == this)
        {
            // add exp to combat conclusion
        }
    }
    public override void OnAttackResolve(Character invokerC) // occurs after the ability made it's last strike. Unlike the other phases, this one will occur exactly once per ability execution, regardless of how many times the ability strikes.
    {
        if (invokerC == this)
        {
            ChangeCombatState(CombatStates.Resolving);
        }
    }
    public override void OnDeath(Character invokerC) // occurs when current hp reach 0.
    {
        if (invokerC == this)
        {
            Die();
        }
    }
    public override void OnEndTurn(Character invokerC) // occurs when this character's turn has ended.
    {
        if (invokerC == this)
        {
            ChangeCombatState(CombatStates.Waiting);
        }
    }
    public override void OnPlayerVictory(Character invokerC) // occurs if player survived the combat and all enemies are dealt with.
    {
        if ((invokerC is Player || invokerC is Ally) && invokerC == this)
        {
            // win
        }
    }
    #endregion

    protected override void Initialize()
    {
        _playerControls = new PlayerControls();
        _camera = Camera.main;
        _cursor = Mouse.current;
        _interact = _playerControls.Player.Interact;

        for (int i = 0; i < _data.ActiveSkills.Length; i++)
        {
            if (_data.ActiveSkills[i])
                _data.ActiveSkills[i].InvokerC = this;
        }
        //foreach (Skill s in _data.ActiveSkills)
        //{
        //    if (s)
        //        s.InvokerC = this;
        //}

        _combatState = Attacking;
    }

    protected override void SlideTowardsOpponentAttackerPos()
    {
        transform.position += _slideSpeed * Time.deltaTime * (Vector3)CombatManager.Instance.GetAttackDirection(this, LastCharacterClickedOn);
    }
    protected override void SlideTowardOriginalPos()
    {
        transform.position += _slideSpeed * Time.deltaTime * _lastCharacterClickedOn.AttackerPosTr.position - new Vector3(_originalPosX, transform.position.y, transform.position.z);
    }

    public void Interact(InputAction.CallbackContext interactContext)
    {
        // get character on click --------------------------------------------------------------
        Ray ray = _camera.ScreenPointToRay(_cursorPos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider && hit.transform.root.TryGetComponent(out Character targetCharacter))
        {

            _lastCharacterClickedOn = targetCharacter;

            if (_isMyTurn && _combatState == Attacking)
                OpenSkillMenu();
            else if (!_isMyTurn && _combatState == Waiting) ;

            Debug.Log($"Clicked on {_lastCharacterClickedOn}");
        }
        else
        {
            _lastCharacterClickedOn = null;
        }
        // -------------------------------------------------------------------------------------

    }

    #region IPlayer
    public void OpenSkillMenu()
    {
        if (!_lastCharacterClickedOn)
            return;

        CombatUIManager.Instance.RefreshCombatSkillMenuDisplay(this, _lastCharacterClickedOn, _camera.WorldToScreenPoint(_lastCharacterClickedOn.transform.position), _data.ActiveSkills, _lastCharacterClickedOn.Data.CurrentLevel);

        switch (CombatUIManager.Instance.CombatSkillMenu.gameObject.activeInHierarchy)
        {
            case true:
                CombatUIManager.Instance.CombatSkillMenu.gameObject.SetActive(false);
                CombatUIManager.Instance.CombatSkillMenu.SkillsParent.SetActive(false);
                break;
            case false:
                CombatUIManager.Instance.CombatSkillMenu.gameObject.SetActive(true);
                CombatUIManager.Instance.CombatSkillMenu.SkillsParent.SetActive(true);
                break;
        }
    }
    #endregion

    #region ICharacter
    public override void ActivateSkill()
    {
        _data.ActiveSkills[SkillSlotToActivateNum].Activate();
        //(_data as CharacterData).ActiveSkills[SkillSlotToActivateNum].Activate();
    }
    public override void Die()
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
