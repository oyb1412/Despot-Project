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
                    //적 체크.
                    case RoomType.Combat:
                        ChangeType(BattleStateType.BattleWait);
                        break;
                    //현재 맵이 상점인지 체크
                    case RoomType.Shop:
                        ChangeType(BattleStateType.Shop);
                        break;
                        //보상맵 체크
                    case RoomType.Reward:
                        ChangeType(BattleStateType.SelectItem);
                        break;
                }
                break;
            case BattleStateType.BattleWait:
                UIManager.Instance.SetGameLog("검 버튼을 눌러 전투를 시작하세요!");
                UIManager.Instance.ToggleUI(UIType.FightToggle);
                //버튼 활성화, 버튼 누르면 battle로 변경
            break;
                case BattleStateType.Battle:
                UIManager.Instance.SetGameLog("전투 진행 중...");

                var playerList = PlayerCretureManager.Instance.UnitSlotManager.GetAllUnit();
                var enemyList = EnemyCretureManager.Instance.UnitSlotManager.GetAllUnit();

                foreach(var item in playerList) {
                    item.GetComponent<PlayerCretureBase>().StartBattle();
                }

                foreach (var item in enemyList) {
                    item.GetComponent<EnemyCretureBase>().StartBattle();
                }

                //모든 크리쳐 순회하며 배틀 시작
                break;
                case BattleStateType.CanChangeMap:
                UIManager.Instance.SetGameLog("이동할 맵을 선택하세요!");

                //맵 이동 기능 활성화, 버튼 누르면 크리쳐 이동
                break;
                case BattleStateType.Shop:
                UIManager.Instance.SetGameLog("상점에서 당신을 위한 물픔을 구매하세요!");

                //상점 배치
                break;
            case BattleStateType.Lose:
                UIManager.Instance.SetGameLog("전투에서 패배했습니다...");

                //게임 종료
                break;
            case BattleStateType.SelectItem:
                UIManager.Instance.SetGameLog("보상 중 하나를 선택하세요!");

                //아이템 고르면, canchangemap으로 변경
                break;

        }
    }
}
