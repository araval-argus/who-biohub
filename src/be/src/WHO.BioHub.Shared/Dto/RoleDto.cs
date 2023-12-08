using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public RoleType RoleType { get; set; }
        public bool AddToRegistration { get; set; }
    }
}
