//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TexasHoldem.Database.EntityFramework.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class HandStep
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HandStep()
        {
            this.GameRooms = new HashSet<GameRoom>();
        }
    
        public int hand_Step_value { get; set; }
        public string hand_Step_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameRoom> GameRooms { get; set; }
    }
}
