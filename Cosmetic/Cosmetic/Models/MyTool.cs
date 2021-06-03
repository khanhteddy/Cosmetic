using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cosmetic.Models
{
    public class MyTool
    {
        public static string UploadHinh(IFormFile fHinh, string folder)
        {
            string fileNameReturn = string.Empty;
            if (fHinh != null)
            {
                fileNameReturn = $"_{DateTime.Now.Ticks}{fHinh.FileName}";
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, fileNameReturn);
                using (var file = new FileStream(fileName, FileMode.Create))
                {
                    fHinh.CopyTo(file);
                }
            }
            return fileNameReturn;
        }
    }

    public static class StaticClass
    {
        public static string ToVND(this double dongia)
        {
            return $"{dongia.ToString("#,##0")} đ";
        }

        public static string ToUrlFriendly(this string tieuDe)
        {
            if (tieuDe != null)
            {
                string str = tieuDe.ToLower().Trim();

                //thay thế tiếng Việt
                str = Regex.Replace(str, @"[áàảãạâấầẩẫậăắằẳẵặ]", "a");
                str = Regex.Replace(str, @"[éèẻẽẹêếềểễệ]", "e");
                str = Regex.Replace(str, @"[úùụủũưừứựửữ]", "u");
                str = Regex.Replace(str, @"[íìỉĩị]", "i");
                str = Regex.Replace(str, @"[óòỏõọôốồổỗộơớờởỡợ]", "o");
                str = Regex.Replace(str, @"[ýỳỷỹỵ]", "y");
                str = Regex.Replace(str, @"[đ]", "d");
                str = Regex.Replace(str, @"[-]", "");
                str = Regex.Replace(str, @"[+]", "");

                str = Regex.Replace(str, @"[^a-z0-9\s-]", " ");
                str = Regex.Replace(str, @"\s+", "-").Trim();
                str = Regex.Replace(str, @"\s", "-");
                return str;
            }

            return tieuDe;
        }
    }


}
