
using DocumentManagementSystem.Core.Entities;

namespace DocumentManagementSystem.Web.Helpers
{
    /// <summary>
    /// Session da Tutulan Bilgilere erişim sağlar.
    /// </summary>
    public static class SessionManagement
    {
        public static bool IsAdmin;

        public static bool IsStudent;

        public static bool IsJuryMember;

        public static bool IsAssistant;

        public static bool IsInstructor;

        public static bool IsChair;

        public static bool IsCoordinator;

        public static bool IsExternalJuryMember;

        public static int ActiveUserId;

        public static string ActiveUserNameSurname;

        public static string ActiveUserPictureUrl;

        

    }
}
