using CnC.Base;

namespace CnC.Model
{

    public class GameCommand : FireObject, IFireObject
    {
        public GameCommand(string userId, string objectId, enCommand command, string unitId = null, int turn = 0, int? rotation = null, int? distance = null)
        {
            this.Path = this.GetType().Name;

            this.UserId = userId;
            this.ObjectId = objectId;
            this.Command = command;
            this.UnitId = unitId;
            this.Rotation = rotation;
            this.Distance = distance;
            this.Turn = turn;
            this.CommandStatus = enCommandStatus.Pending;
        }
        public string ObjectId { get; set; }

        public string UnitId { get; set; }
        public int Turn { get; set; }
        public enCommand Command { get; set; }

        public int? Rotation { get; set; }
        public int? Distance { get; set; }

        public enCommandStatus CommandStatus { get; set; }
        public string CommandStatusMessage { get; set; }
    }
}
