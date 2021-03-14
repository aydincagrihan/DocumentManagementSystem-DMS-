using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Core.Enums
{
    public static class Constants
    {
        public struct Gender
        {
            public const string Male = "Male";
            public const string Female = "Female";            
        }

        public struct SemesterType
        {
            public const string Fall = "Fall";
            public const string Spring = "Spring";
        }

        public struct DefaultPictureUrl
        {
            public const string DefaultPictureUrlMale = "~/img/users/avatar5.png";
            public const string DefaultPictureUrlFemale = "~/img/users/avatar3.png";
        }

        public struct OperationType
        {
            public const string Update = "Update";
            public const string Insert = "Insert";
        }
    }
}
