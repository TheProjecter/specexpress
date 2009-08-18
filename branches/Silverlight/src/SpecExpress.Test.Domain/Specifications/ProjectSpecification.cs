using SpecExpress.Test.Domain.Entities;
namespace SpecExpress.Test.Domain.Specifications
{
    public class ProjectSpecification : SpecificationBase<Project>
    {
        public ProjectSpecification()
        {
            Check(p => p.ProjectName).Required().And.LengthBetween(0, 30);
        }
    }
}