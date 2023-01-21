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

    //[SerializeField] private CharacterAnimations _animations;
    //[SerializeField] private SpriteRenderer _characterSpriteRenderer;
    [SerializeField] private Character _lastCharacterClickedOn;
    [SerializeField] private bool _startCombat = false, _inCombat = false, _finishedCombat = false, _myTurn = false, _startWaiting = false, _finishedWaiting = false,
                                  _startAttacking = false, _finishedAttacking = false, _startResolving = false, _finishedResolving = false, _isAlive = true;

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

        _state.Invoke();
        Debug.Log($"Current player state: {_state.Method.Name}");
    }
    private void OnDisable()
    {
        if (this is Player)
        {
            _interact.Disable();
        }
    }
    #endregion

    #region States
    private void OutsideOfCombat() // while situation where combat do not take place
    {
        // happens before the loop of the first frame where the condition is met
        if (_inCombat)
            _inCombat = false;

        if (_myTurn)
            _myTurn = false;

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

        if (_myTurn && _finishedWaiting)
            _state = Attacking;
        // ---------------------------------------------------------------------





        // happens after the loop of the first frame where the condition is met
        if (_startWaiting)
            _startWaiting = false;
    }
    private void Attacking() // while this character's attacks
    {
        // happens before the loop of the first frame where the condition is met
        if (!_myTurn)
            return;

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
        if (_myTurn)
            return;

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
        if (this is Player)
        {
            _playerControls = new PlayerControls();
            _camera = Camera.main;
            _cursor = Mouse.current;
            _interact = _playerControls.Player.Interact;
        }
        else if (this is Ally)
        {

        }
        else
        {

        }

        //_characterSpriteRenderer.material.mainTexture = _data.SpriteSheet;
        //_characterSpriteRenderer.sprite = _data.SpriteSheet.
        _state = OutsideOfCombat;

        _state = Attacking;
    }
    public void Interact(InputAction.CallbackContext interactContext)
    {
        // if not player ignore
        if (!(this is Player))
            return;

        // get character on click --------------------------------------------------------------
        Ray ray = _camera.ScreenPointToRay(_cursorPos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider && hit.transform.root.TryGetComponent(out Character targetCharacter))
        {
            _lastCharacterClickedOn = targetCharacter;
            Debug.Log($"Clicked on {_lastCharacterClickedOn}");
        }
        else
        {
            _lastCharacterClickedOn = null;
            return;
        }
        // -------------------------------------------------------------------------------------

        if (_myTurn && _state == Attacking)
            OpenSkillMenu();

        if (_state == OutsideOfCombat)
            Debug.Log("interact outside of combat");
    }

    #region ICharacter
    public void OpenSkillMenu()
    {
        if (!_lastCharacterClickedOn)
            return;

        UIManager.Instance.RefreshCombatSkillMenuDisplay(_lastCharacterClickedOn, _camera.WorldToScreenPoint(_lastCharacterClickedOn.transform.position), _data.ActiveSkills, _lastCharacterClickedOn._data.CurrentLevel);

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
