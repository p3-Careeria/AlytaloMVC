using System.Collections.Generic;

namespace AlytaloMVC.Models
{
    public class OminaisuusViewModel
    {
        public  IEnumerable<Sauna> Saunat { get; set; }
        public  IEnumerable<Termostaatti> Termostaatit { get; set; }
        public  IEnumerable<Valot> Valot { get; set; }
    }
}