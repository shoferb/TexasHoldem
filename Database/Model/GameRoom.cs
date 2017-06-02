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
    
    public partial class GameRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GameRoom()
        {
            this.Decks = new HashSet<Deck>();
            this.Players = new HashSet<Player>();
            this.SystemLogs = new HashSet<SystemLog>();
            this.UserTables = new HashSet<UserTable>();
            this.UserTables1 = new HashSet<UserTable>();
            this.UserTables2 = new HashSet<UserTable>();
            this.UserTables3 = new HashSet<UserTable>();
        }
    
        public int room_Id { get; set; }
        public int game_id { get; set; }
        public int Dealer_position { get; set; }
        public int Max_Bet_In_Round { get; set; }
        public int Pot_count { get; set; }
        public int Bb { get; set; }
        public int Sb { get; set; }
        public bool is_Active_Game { get; set; }
        public int curr_Player { get; set; }
        public int Dealer_Player { get; set; }
        public int Bb_Player { get; set; }
        public int SB_player { get; set; }
        public int hand_step { get; set; }
        public int First_Player_In_round { get; set; }
        public int curr_player_position { get; set; }
        public int first_player_in_round_position { get; set; }
        public int last_rise_in_round { get; set; }
        public int league_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deck> Decks { get; set; }
        public virtual GameReplay GameReplay { get; set; }
        public virtual HandStep HandStep { get; set; }
        public virtual LeagueName LeagueName { get; set; }
        public virtual UserTable UserTable { get; set; }
        public virtual UserTable UserTable1 { get; set; }
        public virtual UserTable UserTable2 { get; set; }
        public virtual UserTable UserTable3 { get; set; }
        public virtual UserTable UserTable4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Players { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SystemLog> SystemLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTable> UserTables { get; set; }
        public virtual Card Card { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTable> UserTables1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTable> UserTables2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTable> UserTables3 { get; set; }
        public virtual UserTable UserTable5 { get; set; }
    }
}
