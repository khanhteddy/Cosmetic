using System;
using System.Collections.Generic;

namespace Cosmetic.Models
{
    public partial class Blog
    {
        public int MaBlog { get; set; }
        public string TenBlog { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayDang { get; set; }
        public int? MaLoaiBlog { get; set; }

        public LoaiBlog MaLoaiBlogNavigation { get; set; }
    }
}
