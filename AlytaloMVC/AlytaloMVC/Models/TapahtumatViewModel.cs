using System;

namespace AlytaloMVC.Models
{
    public class TapahtumatViewModel
    {
        public int Id { get; set; }
        public int OminaisuudenId { get; set; }
        public string Tapahtuma { get; set; }
        public DateTime Ajankohta { get; set; }
    }
}