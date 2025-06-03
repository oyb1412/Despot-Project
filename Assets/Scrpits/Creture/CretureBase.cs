using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum CretureBundle {
    Melee,
    Archor,
    Mage,
    Count,
}

public enum CretureState {
    Idle,
    Search,
    Move,
    Attack,
    Die,
}


public abstract class CretureBase : MonoBehaviour
{
    public int AttackPower;
    public float AttackSpeed;
    public float MoveSpeed;
    public float AttackRange;
    public int MaxHp;
    public int CurrentHp;
    public Animator animator;
    public CretureState State;
    public AttackInterface AttackInterface;
    public CretureBundle Bundle;
    public bool IsPlayer;
    public CretureBase CurrentTarget;
    private Coroutine battleRoutine;

    protected virtual void Awake() {
        State = CretureState.Idle;
        MaxHp = 50;
        AttackPower = 3;
        AttackSpeed = 1;
        MoveSpeed = 2;
        AttackRange = 2;
        CurrentHp = MaxHp;
        animator = GetComponent<Animator>();
    }

    public void StartBattle() {
        battleRoutine = StartCoroutine(BattleLoop());
    }

    public void StopBattle() {
        animator.SetBool("Move", false);
        State = CretureState.Idle;

        if (IsPlayer)
            BattleStateManager.Instance.ChangeType(BattleStateType.SelectItem);
        else
            BattleStateManager.Instance.ChangeType(BattleStateType.Lose);

        StopCoroutine(battleRoutine);
    }

    private IEnumerator BattleLoop() {
        while (true) {
            if (CurrentHp < 0)
                yield break;

            yield return StartCoroutine(SearchTarget());

            if (CurrentTarget == null) {
                yield break;
            }

            yield return StartCoroutine(TraceTarget(CurrentTarget));

            if (CurrentTarget == null) continue;

            yield return StartCoroutine(AttackTarget(CurrentTarget));
        }
    }

    public IEnumerator SearchTarget() {
        State = CretureState.Search;

        List<GameObject> enemyList = IsPlayer
    ? EnemyCretureManager.Instance.UnitSlotManager.unitSlots
        .Values.SelectMany(array => array).Where(go => go != null).ToList()
    : PlayerCretureManager.Instance.UnitSlotManager.unitSlots
        .Values.SelectMany(array => array).Where(go => go != null).ToList();

        float minDist = float.MaxValue;
        CurrentTarget = null;

        foreach (var unit in enemyList) {
            if (unit == null) continue;
            float dist = Vector3.Distance(transform.position, unit.transform.position);
            if (dist < minDist) {
                minDist = dist;
                CurrentTarget = unit.GetComponent<CretureBase>();
            }
        }

        yield return null;
    }

    public IEnumerator TraceTarget(CretureBase target) {
        State = CretureState.Move;
        animator.SetBool("Move", true);
        while (true) {
            if (target == null) yield break;

            float dist = Vector3.Distance(transform.position, target.transform.position);

            if (dist <= AttackRange) {
                animator.SetBool("Move", false);
                yield break;
            }

            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.position += dir * MoveSpeed * Time.deltaTime;

            yield return null;
        }
    }

    public IEnumerator AttackTarget(CretureBase target) {
        State= CretureState.Attack;
        while (true) {
            if (target == null || Vector3.Distance(transform.position, target.transform.position) > AttackRange
                || target.State == CretureState.Die || target.CurrentHp <= 0) {
                animator.SetBool("Move", false);
                State = CretureState.Idle;
                yield break; 
            }

            //공격
            yield return new WaitForSeconds(AttackSpeed);
            animator.SetTrigger("Attack");
            Attack(this, target);
            Debug.Log("공격");
        }
    }

    public abstract void Attack(CretureBase my, CretureBase targe);

    public void TakeDamage(int amount) {
        CurrentHp -= amount;
        if(CurrentHp <= 0 && State != CretureState.Die) {
            if(IsPlayer) {
                PlayerCretureManager.Instance.UnitSlotManager.RemoveUnit(Bundle, gameObject);
                if(PlayerCretureManager.Instance.UnitSlotManager.GetTotalUnitCount() <= 0) {
                    var enemyList = EnemyCretureManager.Instance.UnitSlotManager.GetAllUnit();
                    foreach (var item in enemyList) {
                        item.GetComponent<EnemyCretureBase>().StopBattle();
                    }
                }
            }
            else {
                EnemyCretureManager.Instance.UnitSlotManager.RemoveUnit(Bundle, gameObject);
                if (EnemyCretureManager.Instance.UnitSlotManager.GetTotalUnitCount() <= 0) {
                    var playerList = PlayerCretureManager.Instance.UnitSlotManager.GetAllUnit();
                    foreach (var item in playerList) {
                        item.GetComponent<PlayerCretureBase>().StopBattle();
                    }
                }
            }

            
            State = CretureState.Die;
            animator.SetBool("Move", false);
            animator.SetTrigger("Die");
            StopCoroutine(battleRoutine);
        }
    }

    public void SetDieEvent() {
        Destroy(gameObject);
    }
}
