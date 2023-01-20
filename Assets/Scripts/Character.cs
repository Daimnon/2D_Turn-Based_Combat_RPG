using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : Role, ICharacter
{
    private delegate void State();
    private State _state;

    private PlayerControls _playerControls;
    private InputAction _interact;

    [SerializeField] private CharacterData _data;
    public CharacterData Data => _data;

    [SerializeField] private Character _lastCharacterClickedOn;
    [SerializeField] private bool _startCombat = false, _finishedCombat = false, _startWaiting = false, _finishedWaiting = false, _startAttacking = false,
                                  _finishedAttacking = false, _startResolving = false, _finishedResolving = false, _isAlive = true;

    public Character LastCharacterClickedOn => _lastCharacterClickedOn;
    public bool IsAlive { get => _isAlive; set => _ = value; }

    private Camera _camera;
    private Mouse _cursor;
    private Vector2 _cursorPos;

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        this.Initialize();
    }
    private void OnEnable()
    {
        if (_characterType == CharacterType.Player)
        {
            _interact.Enable();
            _interact.performed += Interact;
        }
    }
    private void Update()
    {
        if (_characterType == CharacterType.Player)
        {
            _cursorPos = _cursor.position.ReadValue();
        }

        _state.Invoke();
        Debug.Log($"Current player state: {_state.Method.Name}");
    }
    private void OnDisable()
    {
        if (_characterType == CharacterType.Player)
        {
            _interact.Disable();
        }
    }
    #endregion

    #region States
    private void OutsideOfCombat() // while situation where combat do not take place
    {
        // happens before the loop of the first frame where the condition is met
        if (_finishedCombat)
            _state = Waiting;
        // ---------------------------------------------------------------------





        // happens after the loop of the first frame where the condition is met
        if (_startCombat)
            _startCombat = false;
    }
    private void Waiting() // while waiting for this character's turn
    {
        // happens before the loop of the first frame where the condition is met
        if (_finishedCombat)
            _state = OutsideOfCombat;

        if (_finishedWaiting)
            _state = Attacking;
        // ---------------------------------------------------------------------





        // happens after the loop of the first frame where the condition is met
        if (_startWaiting)
            _startWaiting = false;
    }
    private void Attacking() // while this character's attacks
    {
        // happens before the loop of the first frame where the condition is met
        if (_finishedCombat)
            _state = OutsideOfCombat;

        if (_finishedAttacking)
            _state = Waiting;
        // ---------------------------------------------------------------------





        // happens after the loop of the first frame where the condition is met
        if (_startAttacking)
            _startAttacking = false;
    }
    private void Resolving() // after this character's has being attacked
    {
        // happens before the loop of the first frame where the condition is met
        if (_data.CurrentHealth <= 0)
            Die();

        if (_finishedCombat)
            _state = OutsideOfCombat;

        if (_finishedResolving)
            _state = Waiting;
        // ---------------------------------------------------------------------






        // happens after the loop of the first frame where the condition is met
        if (_startResolving)
            _startResolving = false;
    }
    #endregion

    private void Initialize()
    {
        if (_characterType == CharacterType.Player)
        {
            _playerControls = new PlayerControls();
            _camera = Camera.main;
            _cursor = Mouse.current;
            _interact = _playerControls.Player.Interact;
        }
        
        _state = OutsideOfCombat;
    }
    public void Interact(InputAction.CallbackContext interactContext)
    {
        if (_characterType != CharacterType.Player)
            return;

        Ray ray = _camera.ScreenPointToRay(_cursorPos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider && hit.transform.root.TryGetComponent(out Character targetCharacter))
        {
            _lastCharacterClickedOn = targetCharacter;
            Debug.Log($"Clicked on {_lastCharacterClickedOn.name}");
        }
        else
        {
            _lastCharacterClickedOn = null;
            return;
        }

        // to remove
        OpenSkillMenu();
    }

    #region ICharacter
    public void OpenSkillMenu()
    {
        if (!_lastCharacterClickedOn)
            return;

        UIManager.Instance.CombatSkillMenu.gameObject.SetActive(true);
        UIManager.Instance.CombatSkillMenu.SkillsParent.SetActive(true);
        UIManager.Instance.RefreshCombatSkillMenuDisplay(_lastCharacterClickedOn, _camera.WorldToScreenPoint(_lastCharacterClickedOn.transform.position), _data.ActiveSkills, _data.CurrentLevel);
        // open skill menu
    }
    public void DoSkill(int slotNum)
    {
        // do skill
    }
    public void Die()
    {
        // die
    }
    #endregion

    #region overrides
    public override string ToString()
    {
        return $"Name: {_data.name}, Role: {_role}, Lvl: {_data.CurrentLevel}";
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
