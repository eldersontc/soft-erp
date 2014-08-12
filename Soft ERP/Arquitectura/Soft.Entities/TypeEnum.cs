using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    public class TypeEnum
    {
        public static class CEnumExportFormat 
        {
            public const string PDF = "PDF";
            public const string WORD = "WORD";
            public const string EXCEL = "EXCEL";
        }

        public static class CEnumCondition
        {
            public const string LIKE = "LIKE";
            public const string IN = "IN";
            public const string NOT_IN = "NOT IN";
        }
    }
}
