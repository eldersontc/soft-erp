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
        public static void Control(Exception ex, MessageBoxIcon Icon)
        {
            FrmMessageError FrmError = new FrmMessageError();
            FrmError.ShowError(ex.ToString(), "", SystemIcons.Error.ToBitmap());
        }
    }
}
