using Cinema.Contracts.Common;
using FluentAssertions;
using NetArchTest.Rules;

namespace Cinema.ArchitectureTests;

public class ContractsProjectArchitectureTests
{
    [Theory]
    [InlineData(ProjectsNamespaces.Infrastructure)]
    [InlineData(ProjectsNamespaces.API)]
    public void Contracts_ShouldNotDependOn_InfrastructureOrAPIProject(string projectNamespace)
    {
        var contractsAssembly = typeof(ContractsAssemblyReference).Assembly;

        var testDependencies = Types
            .InAssembly(contractsAssembly)
            .ShouldNot()
            .HaveDependencyOn(projectNamespace)
            .GetResult();

        testDependencies.IsSuccessful.Should().BeTrue();
    }
}
