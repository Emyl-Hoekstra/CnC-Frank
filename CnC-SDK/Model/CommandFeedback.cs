using CnC.Base;
using FirebaseSharp.Portable;

namespace CnC.Model
{
    public class CommandFeedback : FireObject, IFireObject
    {
        public CommandFeedback()
        {
            this.Path = this.GetType().Name;
        }

        public CommandFeedback(GameCommand command, enCommandStatus status, string feedback)
        {
            this.Path = this.GetType().Name;
            this.CommandId = command.Id;
            this.CommandStatus = status;
            this.CommandType = command.Command;
            this.UserId = command.UserId;
            this.Feedback = feedback;
        }

        public string CommandId { get; set; }
        public enCommand CommandType { get; set; }
        public enCommandStatus CommandStatus { get; set; }

        public string Feedback { get; set; }

        public override string ToString()
        {
            return this.Feedback;
        }
    }
}
