﻿using System.Collections.Generic;

namespace TexasHoldemShared.CommMessages.ServerToClient
{
    public class LeaderboardResponseCommMessage : ResponeCommMessage
    {
        public List<LeaderboardLineData> Results;

        public LeaderboardResponseCommMessage() : base(-1) { }

        public LeaderboardResponseCommMessage(int id, long sid, bool success, CommunicationMessage originalMsg,
            List<LeaderboardLineData> results) : base(id, sid, success, originalMsg)
        {
            Results = results;
        }

        public override ResponeCommMessage Handle(IEventHandler handler)
        {
            return handler.HandleEvent(this);
        }

        //public override void Notify(IResponseNotifier notifier, ResponeCommMessage msg)
        //{
        //    notifier.Notify(OriginalMsg, this);
        //}

        public override bool Equals(CommunicationMessage other)
        {
            bool ans = false;
            if (other.GetType() == typeof(LeaderboardResponseCommMessage))
            {
                var afterCast = (LeaderboardResponseCommMessage)other;
                ans = UserId == afterCast.UserId && Results.TrueForAll(line =>
                {
                    //check if all items of Results exist in afterCast.Results
                    return afterCast.Results.Find(elem => elem.Equals(line)) != null;
                });
            }
            return ans;
        }
    }
}
