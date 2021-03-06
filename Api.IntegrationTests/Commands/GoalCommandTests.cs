﻿using Api.Domain.Entities;
using Api.IntegrationTests.AutoData;
using Api.Repositories.Commands;
using Api.Repositories.DbConnection;
using AutoFixture.Idioms;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests.Commands
{
    [Collection("DatabaseSetup")]
    public class GoalCommandTests
    {
        [Theory, AutoDataNSubstitute]
        public void GuardClausesTest(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(GoalCommand).GetConstructors());
        }

        [Theory, AutoDataNSubstitute]
        public async Task CreateAsync_ShouldPerformCorrectly(
            Goal goal,
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalCommand(sqlConnectionFactory);

            // Act
            var result = await sut.CreateAsync(goal);

            // Asserts
            result.Should().BeTrue();
        }

        [Theory, AutoDataNSubstitute]
        public async Task UpdateAsync_ShouldPerformCorrectly(
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalCommand(sqlConnectionFactory);
            var goal = new Goal
            {
                Id = new Guid("f4f25a21-6a88-4623-89cc-7d0ed349e0ea"),
                Title = "Study Investment",
                Count = 40
            };

            // Act
            var result = await sut.UpdateAsync(goal);

            // Asserts
            result.Should().BeTrue();
        }

        // TODO: test for update fails because id doesnt exist

        [Theory, AutoDataNSubstitute]
        public async Task DeleteAsync_ShouldPerformCorrectly(
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalCommand(sqlConnectionFactory);
            var goalId = new Guid("74e2948a-37a4-457d-9254-2cbed39ae27f");

            // Act
            var result = await sut.DeleteAsync(goalId);

            // Asserts
            result.Should().BeTrue();
        }

        [Theory, AutoDataNSubstitute]
        public async Task DeleteAsync_WhenIdDoesntExist_ShouldPerformCorrectly(
           SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalCommand(sqlConnectionFactory);
            var goalId = new Guid("74e2948a-0000-0000-0000-2cbed39ae27f");

            // Act
            var result = await sut.DeleteAsync(goalId);

            // Asserts
            result.Should().BeFalse();
        }
    }
}
