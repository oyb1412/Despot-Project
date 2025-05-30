using UnityEngine;

public enum BattleStateType {
    Idle,
    BattleWait,
    Battle,
    CanChangeMap,
    Shop,
    Lose,
    SelectItem,
    Count,
}

public class BattleStateManager : MonoSingleton<BattleStateManager>
{
    public BattleStateType CurrentType;

    void Start()
    {
        CurrentType = BattleStateType.Count;
    }

    public void ChangeType(BattleStateType type) {
        CurrentType = type;

        switch (CurrentType) {
            case BattleStateType.Idle:
                UIManager.Instance.SetGameLog("");

                switch (MapManager.Instance.CurrentMapType) {
                    //�� üũ.
                    case RoomType.Combat:
                        ChangeType(BattleStateType.BattleWait);
                        break;
                    //���� ���� �������� üũ
                    case RoomType.Shop:
                        ChangeType(BattleStateType.Shop);
                        break;
                        //����� üũ
                    case RoomType.Reward:
                        ChangeType(BattleStateType.SelectItem);
                        break;
                }
                break;
            case BattleStateType.BattleWait:
                UIManager.Instance.SetGameLog("�� ��ư�� ���� ������ �����ϼ���!");
                UIManager.Instance.ToggleUI(UIType.FightToggle);
                //��ư Ȱ��ȭ, ��ư ������ battle�� ����
            break;
                case BattleStateType.Battle:
                UIManager.Instance.SetGameLog("���� ���� ��...");

                var playerList = PlayerCretureManager.Instance.UnitSlotManager.GetAllUnit();
                var enemyList = EnemyCretureManager.Instance.UnitSlotManager.GetAllUnit();

                foreach(var item in playerList) {
                    item.GetComponent<PlayerCretureBase>().StartBattle();
                }

                foreach (var item in enemyList) {
                    item.GetComponent<EnemyCretureBase>().StartBattle();
                }

                //��� ũ���� ��ȸ�ϸ� ��Ʋ ����
                break;
                case BattleStateType.CanChangeMap:
                UIManager.Instance.SetGameLog("�̵��� ���� �����ϼ���!");

                //�� �̵� ��� Ȱ��ȭ, ��ư ������ ũ���� �̵�
                break;
                case BattleStateType.Shop:
                UIManager.Instance.SetGameLog("�������� ����� ���� ������ �����ϼ���!");

                //���� ��ġ
                break;
            case BattleStateType.Lose:
                UIManager.Instance.SetGameLog("�������� �й��߽��ϴ�...");

                //���� ����
                break;
            case BattleStateType.SelectItem:
                UIManager.Instance.SetGameLog("���� �� �ϳ��� �����ϼ���!");

                //������ ����, canchangemap���� ����
                break;

        }
    }
}
