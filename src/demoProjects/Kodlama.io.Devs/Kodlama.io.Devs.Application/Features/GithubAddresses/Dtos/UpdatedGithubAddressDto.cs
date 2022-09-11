namespace Kodlama.io.Devs.Application.Features.GithubAddresses.Dtos
{
    public class UpdatedGithubAddressDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GithubUrl { get; set; }
    }
}