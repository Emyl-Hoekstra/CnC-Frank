
namespace CnC.Base
{
    public enum enObjectState
    {
        unchanged,
        updated,
        added,
        deleted
    }
    public enum enCommand
    {
        JoinGame,
        LeaveGame,
        AddUnit,
        MoveForward,
        MoveBackward,
        FireFixedGun,
        RotateCCW, //45-90'
        RotateCW, //45-90'
        RotateTurretCCW, //45-90'
        RotateTurretCW //45-90'
    }

    public enum enCommandStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public enum enUnitStatus
    {
        Building,
        Build,
        Moving,
        Rotating,
        Stopped,
        Repairing,
        Damaged,
        Destroyed
    }

    public enum enWeaponStatus
    {
        Repairing,
        Operational,
        Overheated,
        Damaged,
        Destroyed
    }

    public enum enHeading
    {
        South,
        SouthWest,
        West,
        NorthWest,
        North,
        NorthEast,
        East,
        SouthEast
    }

    public enum enRotationDirection
    {
        Clockwise,
        CounterClockwise
    }

}
