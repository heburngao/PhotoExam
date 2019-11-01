namespace Common
{
    public enum SubCode
    {
        GetRole = 1,
        AddRole = 2,
        SelectRole = 3,
        UpdateRole = 4,
        AddTaskDB = 5,
        UpdateTaskDB = 6,
        GetTaskDB = 7,
        GetInventoryItemDB = 8,
        AddInventoryItemDB = 9,
        UpdateInventoryItemDB = 10,
        UpdateInventoryItemDBList = 11,
        UpgradeEqup = 12,
        Add = 13,
        Update = 14,
        Get = 15,
        Upgrade = 16,
        SendTeam = 17,//req create team
        CancelTeam = 18,//req cancel create team
        GetTeam = 19,//team create success
        SyncPositionAndRotation = 20,
        SyncMoveAnimation = 21,
        CreateEnemy = 22,// spawn enemy
        SyncAnimation = 23,
        SendGameState = 24,
        SyncBossAnimation = 25,
        
    }
}