//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TexasHoldem.Database.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class GameRoomPreferance
    {
        public int room_id { get; set; }
        public Nullable<bool> is_Spectetor { get; set; }
        public Nullable<int> Min_player_in_room { get; set; }
        public Nullable<int> max_player_in_room { get; set; }
        public Nullable<int> enter_paying_money { get; set; }
        public Nullable<int> starting_chip { get; set; }
        public Nullable<int> Bb { get; set; }
        public Nullable<int> Sb { get; set; }
        public Nullable<int> League_name { get; set; }
        public Nullable<int> Game_Mode { get; set; }
        public int Game_Id { get; set; }
    
        public virtual GameMode GameMode { get; set; }
        public virtual LeagueName LeagueName { get; set; }
    }
}
