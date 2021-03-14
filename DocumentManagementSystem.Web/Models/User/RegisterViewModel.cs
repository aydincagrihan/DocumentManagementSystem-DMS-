namespace DocumentManagementSystem.Web.Models.User
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ReTypePassword { get; set; }

        public string Gender { get; set; }
    }
}