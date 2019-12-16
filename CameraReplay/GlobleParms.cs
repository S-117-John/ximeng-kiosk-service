using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunRise.HOSP.Common;
using System.Drawing;

namespace CameraReplay
{
   public class GlobleParms
    {
       public static bool Init()
       {
           SystemConfig.DataGridViewCheckBoxNormal = Image.FromFile(@"Image\表格选择\选择区域.png");
           SystemConfig.DataGridViewCheckBoxChecked = Image.FromFile(@"Image\表格选择\选中状态.png");
           SunRise.HOSP.ExControl.TransButton.LayoutName = "Blue";
           SunRise.HOSP.ExControl.TransButton.IsUserLayout = false;
           SunRise.HOSP.ExControl.TransButton.AllDefaultFontColor = Color.Black;
           SunRise.HOSP.ExControl.TransButton.DisableFontColor = Color.Gray;
         
           SunRise.HOSP.ExControl.TransButton.DefaultImgNormal = Image.FromFile(@"Image\小按钮\按钮n.png");
           SunRise.HOSP.ExControl.TransButton.DefaultImgPress = Image.FromFile(@"Image\小按钮\按钮p.png");
           SunRise.HOSP.ExControl.TransButton.DefaultImgDisable = Image.FromFile(@"Image\小按钮\按钮n.png");
           return true;
       }
    }
}
