using System.Collections.Generic;

namespace DSmartQB.CORE.DTOs
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserTokenDTO
    {
        public string Role { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Prefix { get; set; }
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Datebirth { get; set; }
        public string Gender { get; set; }
        public string NationalId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string RoleId { get; set; }
    }

    public class UserBinder
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public bool Deleted { get; set; }
        public string Prefix { get; set; }
    }

    public class UserPagination
    {
        public List<UserBinder> Users { get; set; }
        public int TotalRows { get; set; }
    }


    public class UserProfile
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DrawMenu
    {
        public string Id { get; set; }
        public string[] Views { get; set; }
    }

    public class MenuBinder
    {
        public string View { get; set; }
        public string Icon { get; set; }
    }

    public class Views
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
