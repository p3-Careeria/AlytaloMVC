//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlytaloMVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sauna
    {
        public int Id { get; set; }
        public int Talon_Id { get; set; }
        public Nullable<bool> Kaynnissa { get; set; }
        public Nullable<int> Lampo { get; set; }
        public Nullable<System.DateTime> Ajakohta { get; set; }
    
        public virtual Talo Talo { get; set; }
    }
}
