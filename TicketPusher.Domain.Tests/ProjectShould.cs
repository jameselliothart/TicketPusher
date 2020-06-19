using System;
using FluentAssertions;
using TicketPusher.Domain.Projects;
using Xunit;

namespace TicketPusher.Domain.Tests
{
    public class ProjectShould
    {
        [Fact]
        public void AllowNoParentProject()
        {
            var sutProject = new Project("None", null);

            sutProject.ParentProject.HasNoValue.Should().BeTrue();
        }

        [Fact]
        public void UpdateItsParentProject()
        {
            var sutProject = new Project("None", null);
            var parent = new Project("Parent");

            sutProject.UpdateParentProject(parent);

            sutProject.ParentProject.Should().Be(parent);
        }

        [Fact]
        public void NotAllowSelfReferencingParentProject()
        {
            var sutProject = new Project("None");

            Action act = () => sutProject.UpdateParentProject(sutProject);

            act.Should().Throw<InvalidOperationException>();
        }
    }
}