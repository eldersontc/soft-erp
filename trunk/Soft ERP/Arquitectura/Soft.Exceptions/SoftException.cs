using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using NHibernate;
using System.Data.SqlClient;

namespace Soft.Exceptions
{
    public class SoftException
    {
        public static void Control(Exception ex)
        {
            //if (ex.InnerException == null)
            {
                switch (ex.GetType().Name)
                {
                    case "SqlException":
                        ShowSqlException((SqlException)ex);
                        break;
                    default:
                        ShowException(ex);
                        break;
                }
            }
            //else { Control(ex.InnerException); }
        }

        public static void ShowException(Exception ex) {
            FrmMessageError FrmError = new FrmMessageError();
            Exception exFinal = ex;
            while (exFinal.InnerException != null) { exFinal = ex.InnerException; }
            FrmError.ShowError(exFinal.Message, exFinal.Source, SystemIcons.Warning.ToBitmap());
        }

        public static void ShowSqlException(SqlException ex) {
            FrmMessageError FrmError = new FrmMessageError();
            String Message = "";
            String Details = "";
            Bitmap Icon = null;
            switch (ex.Number)
            {
                case 547:
                    Int32 StartIndex = ex.Message.IndexOf("FK_");
                    Int32 EndIndex = ex.Message.IndexOf(".");
                    String Me = ex.Message.Substring(StartIndex, EndIndex - StartIndex - 1);
                    String[] Tables = Me.Split('_');
                    Message = "No es posible la eliminación.";
                    Details = String.Format("La entidad {0} se encuentra relacionada con 1 o más {1}(s)", Tables[2], Tables[1]);
                    Icon = SystemIcons.Warning.ToBitmap();
                    break;
                default:
                    Message = ex.Message;
                    Icon = SystemIcons.Warning.ToBitmap();
                    break;
            }
            FrmError.ShowError(Message, Details, Icon);
        }

        public static void Control(Exception ex, Bitmap Icon)
        {
            FrmMessageError FrmError = new FrmMessageError();
            FrmError.ShowError(ex.Message.ToString(), "" , Icon);
        }

    }
}
