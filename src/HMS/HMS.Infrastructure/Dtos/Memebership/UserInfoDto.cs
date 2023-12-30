namespace HMS.Infrastructure.Dtos.Memebership
{
    public class UserInfoDto
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}