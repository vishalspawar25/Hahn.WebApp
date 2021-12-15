namespace Hahn.ApplicatonProcess.Data.models
{
    public  class Applicant:BaseEntity
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Address { get; set; }
        public bool IsHired { get; set; }
    }
}
