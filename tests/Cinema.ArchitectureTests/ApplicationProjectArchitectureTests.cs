using Cinema.Application.Common;
using FluentAssertions;
using NetArchTest.Rules;

namespace Cinema.ArchitectureTests;

public class ApplicationProjectArchitectureTests
{
    [Theory]
    [InlineData(ProjectsNamespaces.Infrastructure)]
    [InlineData(ProjectsNamespaces.Contracts)]
    [InlineData(ProjectsNamespaces.API)]
    public void Application_ShouldNotDependOn_ProjectsDifferentToDomain(string projectNamespace)
    {
        var applicationAssembly = typeof(ApplicationAssemblyReference).Assembly;

        var testDependencies = Types
            .InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(projectNamespace)
            .GetResult();

        testDependencies.IsSuccessful.Should().BeTrue();
    }
}
