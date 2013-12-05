using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Soft.Exceptions
{
    public class SoftException
    {
        public static void Control(Exception Ex, MessageBoxIcon Icon)
        {
            FrmMessageError FrmError = new FrmMessageError();
            int code = Ex.GetHashCode(); 
            FrmError.ShowError(Ex.Message.ToString(), Ex.InnerException + Ex.Source + Ex.StackTrace , SystemIcons.Error.ToBitmap());
        }
    }
}
