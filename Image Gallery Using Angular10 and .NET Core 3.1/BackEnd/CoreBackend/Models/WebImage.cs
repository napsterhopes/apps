using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBackend.Models
{
    public partial class WebImage
    {
        public int WebImgId { get; set; }
        public string WebImgName { get; set; }
        public byte[] Picture { get; set; }

        [NotMapped]
        public string WebImgString { get; set; }
        public DateTime DateTimeAdded { get; set; }
    }
}
