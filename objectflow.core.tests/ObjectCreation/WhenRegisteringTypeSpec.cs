﻿using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using NUnit.Framework;
using Moq;
using Objectflow.core.tests.TestOperations;
using Objectflow.tests.TestDomain;
using Rainbow.ObjectFlow.Container;
using Rainbow.ObjectFlow.Engine;
using Rainbow.ObjectFlow.Framework;
using Rainbow.ObjectFlow.Helpers;
using Rainbow.ObjectFlow.Language;

namespace Objectflow.core.tests.ObjectCreation
{
    public class WhenRegisteringTypeSpec:Specification
    {
        private IKernel _container;
        private Mock<SequentialBuilder<Colour>> _builder;
        private IDefine<Colour> _flow;

        [Scenario]
        public void Given()
        {
            _builder = new Mock<SequentialBuilder<Colour>>(new []{new TaskList<Colour>()});
            _container = new DefaultKernel();
            _container.Register(Component.For<SequentialBuilder<Colour>>().Instance(_builder.Object));
            _container.Register(Component.For<Dispatcher<Colour>>().Instance(new Dispatcher<Colour>()));

            ServiceLocator<Colour>.SetInstance(null);
            ServiceLocator<Colour>.SetInstance(_container);
            _flow = Workflow<Colour>.Definition();
        }

        [Observation]
        public void ShouldCreateInstanceOfSpecifiedOperation()
        {
            _builder.Setup(s => s.AddOperation<DuplicateName>());

            When();
            
            _builder.VerifyAll();
        }

        public void When()
        {            
            _flow.Do<DuplicateName>();
        }

        [Observation]
        public void ShouldCreateInstance()
        {
            var builder = new SequentialBuilder<Colour>(new TaskList<Colour>());
            builder.AddOperation<DuplicateName>();
            
            Assert.That(builder.TaskList.Tasks.Count, Is.EqualTo(1));
        }

        [Observation]
        public void ShouldCreateInstanceAndConstraints()
        {
            var builder = new SequentialBuilder<Colour>(new TaskList<Colour>());
            builder.AddOperation<DuplicateName>(If.IsTrue(true));

            Assert.That(builder.TaskList.Tasks.Count, Is.EqualTo(1));
        }
    }   
}
