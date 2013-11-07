using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;

namespace Soft.Win
{
    public class RefreshView: ControllerApp 
    {
        public override void Start()
        {
            FrmMain.RefreshView();
            base.m_ResultProcess = EnumResult.SUCESS;
            base.Start();
        }
    }
}
