//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Technikmarkt
{
    using System;
    using System.Collections.Generic;
    
    public partial class hp_haendlerkauftprodukt
    {
        public string hp_h_haendlername { get; set; }
        public decimal hp_p_gtin { get; set; }
        public string hp_p_anbietername { get; set; }
    
        public virtual h_haendler h_haendler { get; set; }
        public virtual p_produkt p_produkt { get; set; }
    }
}