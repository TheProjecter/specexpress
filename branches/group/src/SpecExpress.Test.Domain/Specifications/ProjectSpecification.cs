using SpecExpress.Test.Domain.Entities;
namespace SpecExpress.Test.Domain.Specifications
{
    public class ProjectSpecification : Validates<Project>
    {
        public ProjectSpecification()
        {
            Check(p => p.ProjectName).Required().And.LengthBetween(0, 30);
            Check(p => p.StartDate).Required().And.LessThan(p => p.EndDate);
            Check(p => p.EndDate).Required().And.GreaterThan(p => p.StartDate);
        }
    }
}