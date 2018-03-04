﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Polly.Specs.Helpers;
using Xunit;

namespace Polly.Specs
{
    public class ContextSpecs
    {
        [Fact]
        public void Should_assign_ExecutionKey_from_constructor()
        {
            Context context = new Context("SomeKey");

            context.ExecutionKey.Should().Be("SomeKey");

            context.Keys.Count.Should().Be(0);
        }

        [Fact]
        public void Should_assign_ExecutionKey_and_context_data_from_constructor()
        {
            Context context = new Context("SomeKey", new { key1 = "value1", key2 = "value2" }.AsDictionary());

            context.ExecutionKey.Should().Be("SomeKey");
            context["key1"].Should().Be("value1");
            context["key2"].Should().Be("value2");
        }

        [Fact]
        public void Should_assign_CorrelationId_when_accessed()
        {
            Context context = new Context("SomeKey");

            context.CorrelationId.Should().NotBeEmpty();
        }

        [Fact]
        public void Should_return_consistent_CorrelationId()
        {
            Context context = new Context("SomeKey");

            Guid retrieved1 = context.CorrelationId;
            Guid retrieved2 = context.CorrelationId;

            retrieved1.ShouldBeEquivalentTo(retrieved2);
        }
    }
}
