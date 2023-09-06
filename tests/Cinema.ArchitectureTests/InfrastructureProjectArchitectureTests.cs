using Cinema.Infrastructure.Common;
using FluentAssertions;
using NetArchTest.Rules;

namespace Cinema.ArchitectureTests;

public class InfrastructureProjectArchitectureTests
{
    [Theory]
    [InlineData(ProjectsNamespaces.API)]
    [InlineData(ProjectsNamespaces.Contracts)]
    public void Infrastructure_ShouldNotDependOn_ProjectsDifferentToDomainAndApplication(string projectNamespace)
    {
        var infrastructureAssembly = typeof(InfrastructureAssemblyReference).Assembly;

        var testDependencies = Types
            .InAssembly(infrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn(projectNamespace)
            .GetResult();

        testDependencies.IsSuccessful.Should().BeTrue();
    }
}
