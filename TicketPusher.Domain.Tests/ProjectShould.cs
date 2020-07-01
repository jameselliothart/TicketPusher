using System;
using System.Collections.Generic;
using FluentAssertions;
using TicketPusher.Domain.Projects;
using Xunit;

namespace TicketPusher.Domain.Tests
{
    public class ProjectShould
    {
        [Fact]
        public void SetNullParentToNone()
        {
            var sutProject = new Project("Test", null);

            sutProject.ParentProject.Should().Be(Project.None);
        }

        [Fact]
        public void DefaultToNoneParent()
        {
            var sutProject = new Project("Test");

            sutProject.ParentProject.Should().Be(Project.None);
        }

        [Fact]
        public void UpdateItsParentProject()
        {
            var sutProject = new Project("Test", null);
            var parent = new Project("Parent");

            sutProject.SetParentProject(parent);

            sutProject.ParentProject.Should().Be(parent);
        }

        [Fact]
        public void NotAllowSelfReferencingParentProject()
        {
            var sutProject = new Project("Test");

            Action act = () => sutProject.SetParentProject(sutProject);

            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ReturnHierarchy_GivenNoParent()
        {
            var sutProject = Project.None;

            sutProject.GetHierarchy().Should().Be("/");
        }

        [Fact]
        public void ReturnHierarchy_GivenOneParent()
        {
            var sutProject = new Project("SUT", Project.None);

            sutProject.GetHierarchy().Should().Be("/SUT");
        }

        [Fact]
        public void ReturnHierarchy_GivenMultipleNesting()
        {
            var grandParentProject = new Project("Grand Parent", Project.None);
            var parentProject = new Project("Parent", grandParentProject);
            var sutProject = new Project("SUT", parentProject);

            sutProject.GetHierarchy().Should().Be("/Grand Parent/Parent/SUT");
        }
    }
}