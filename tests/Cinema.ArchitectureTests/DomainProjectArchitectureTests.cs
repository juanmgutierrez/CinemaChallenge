using Cinema.Domain.Common;
using FluentAssertions;
using NetArchTest.Rules;

namespace Cinema.ArchitectureTests;

public class DomainProjectArchitectureTests
{
    [Theory]
    [InlineData(ProjectsNamespaces.Application)]
    [InlineData(ProjectsNamespaces.Infrastructure)]
    [InlineData(ProjectsNamespaces.Contracts)]
    [InlineData(ProjectsNamespaces.API)]
    public void Domain_ShouldNotDependOn_OtherProjects(string projectNamespace)
    {
        var domainAssembly = typeof(DomainAssemblyReference).Assembly;

        var testDependencies = Types
            .InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOn(projectNamespace)
            .GetResult();

        testDependencies.IsSuccessful.Should().BeTrue();
    }
}
