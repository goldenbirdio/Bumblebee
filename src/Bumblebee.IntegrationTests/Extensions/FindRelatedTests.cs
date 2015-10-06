using System;

using Bumblebee.Extensions;
using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;

using FluentAssertions;

using NSubstitute;

using NUnit.Framework;

using OpenQA.Selenium;

// ReSharper disable InconsistentNaming

namespace Bumblebee.IntegrationTests.Extensions
{
	[TestFixture]
	public class FindRelatedTests
	{
		[Test]
		public void When_null_IElement_Then_throws_ArgumentNullException()
		{
			IElement element = null;

			Action fn = () => element.FindRelated<GrandparentBlock>();

			fn.ShouldThrow<ArgumentNullException>()
				.Which.ParamName
				.Should().Be("element");
		}

		[Test]
		public void When_IElement_with_null_Parent_Then_throws_ArgumentNullException()
		{
			IBlock parent = null;
			IElement element = Substitute.For<IElement>();

			element.Parent.Returns(parent);

			Action fn = () => element.FindRelated<GrandparentBlock>();

			fn.ShouldThrow<ArgumentNullException>()
				.Which.ParamName
				.Should().Be("block");
		}

		[Test]
		public void When_null_IBlock_Then_throws_ArgumentNullException()
		{
			IBlock block = null;

			Action fn = () => block.FindRelated<GrandparentBlock>();

			fn.ShouldThrow<ArgumentNullException>()
				.Which.ParamName
				.Should().Be("block");
		}

		[Test]
		public void When_TestBlock_with_no_parent_Then_TestBlock_is_returned()
		{
			var environment = Substitute.For<IDriverEnvironment>();
			var session = Substitute.For<Session>(environment);

			var @by = Substitute.For<By>();

			var block = new GrandparentBlock(session, @by);

			var result = block.FindRelated<GrandparentBlock>();

			result.InstanceId
				.Should().Be(block.InstanceId);
		}

		[Test]
		public void When_TestBlock_with_parent_Then_parent_is_not_returned()
		{
			var environment = Substitute.For<IDriverEnvironment>();
			var session = Substitute.For<Session>(environment);

			var @by = Substitute.For<By>();

			var parent = new GrandparentBlock(session, @by);

			var block = new GrandparentBlock(parent, @by);

			var result = block.FindRelated<GrandparentBlock>();

			result.InstanceId
				.Should().Be(block.InstanceId);
		}

		[Test]
		public void When_TestBlock_with_parent_Then_child_is_returned()
		{
			var environment = Substitute.For<IDriverEnvironment>();
			var session = Substitute.For<Session>(environment);

			var @by = Substitute.For<By>();

			var parent = new GrandparentBlock(session, @by);

			var block = new GrandparentBlock(parent, @by);

			var result = block.FindRelated<GrandparentBlock>();

			result.InstanceId
				.Should().Be(block.InstanceId);
		}

		[Test]
		public void When_ChildBlock1_Then_parent_is_findable()
		{
			var environment = Substitute.For<IDriverEnvironment>();
			var session = Substitute.For<Session>(environment);

			var @by = Substitute.For<By>();

			var block = new GrandparentBlock(session, @by);

			var result = block.ParentBlockA.ChildBlock1.FindRelated<ParentBlockA>();

			result.Should().NotBeNull();
		}

		[Test]
		public void When_ChildBlock1_Then_grandparent_is_findable()
		{
			var environment = Substitute.For<IDriverEnvironment>();
			var session = Substitute.For<Session>(environment);

			var @by = Substitute.For<By>();

			var block = new GrandparentBlock(session, @by);

			var result = block.ParentBlockA.ChildBlock1.FindRelated<GrandparentBlock>();

			result.Should().NotBeNull();
		}

		[Test]
		public void When_ChildBlock1_Then_sibling_is_findable()
		{
			var environment = Substitute.For<IDriverEnvironment>();
			var session = Substitute.For<Session>(environment);

			var @by = Substitute.For<By>();

			var block = new GrandparentBlock(session, @by);

			var result = block.ParentBlockA.ChildBlock1.FindRelated<ChildBlock3>();

			result.Should().NotBeNull();
		}

		[Test]
		public void When_ChildBlock1_Then_uncle_is_findable()
		{
			var environment = Substitute.For<IDriverEnvironment>();
			var session = Substitute.For<Session>(environment);

			var @by = Substitute.For<By>();

			var block = new GrandparentBlock(session, @by);

			var result = block.ParentBlockA.ChildBlock1.FindRelated<ParentBlockB>();

			result.Should().NotBeNull();
		}

		[Test]
		public void When_ChildBlock1_Then_cousin_is_findable()
		{
			var environment = Substitute.For<IDriverEnvironment>();
			var session = Substitute.For<Session>(environment);

			var @by = Substitute.For<By>();

			var block = new GrandparentBlock(session, @by);

			var result = block.ParentBlockA.ChildBlock1.FindRelated<ChildBlock2>();

			result.Should().NotBeNull();
		}
	}

	public abstract class InstanceGuidBlock : Block
	{
		private readonly Guid _instanceId = Guid.NewGuid();

		public Guid InstanceId { get { return _instanceId; } }

		protected InstanceGuidBlock(Session session, By @by) : base(session, @by)
		{
		}

		protected InstanceGuidBlock(IBlock parent, By @by) : base(parent, @by)
		{
		}
	}

	public class GrandparentBlock : InstanceGuidBlock
	{
		public GrandparentBlock(Session session, By @by) : base(session, @by)
		{
		}

		public GrandparentBlock(IBlock parent, By @by) : base(parent, @by)
		{
		}

		public ParentBlockA ParentBlockA
		{
			get
			{
				return new ParentBlockA(this, Substitute.For<By>());
			}
		}

		public ParentBlockB ParentBlockB
		{
			get
			{
				return new ParentBlockB(this, Substitute.For<By>());
			}
		}
	}

	public class ParentBlockA : InstanceGuidBlock
	{
		public ParentBlockA(Session session, By @by) : base(session, @by)
		{
		}

		public ParentBlockA(IBlock parent, By @by) : base(parent, @by)
		{
		}

		public ChildBlock1 ChildBlock1
		{
			get
			{
				return new ChildBlock1(this, Substitute.For<By>());
			}
		}

		public ChildBlock3 ChildBlock3
		{
			get
			{
				return new ChildBlock3(this, Substitute.For<By>());
			}
		}
	}

	public class ParentBlockB : InstanceGuidBlock
	{
		public ParentBlockB(Session session, By @by) : base(session, @by)
		{
		}

		public ParentBlockB(IBlock parent, By @by) : base(parent, @by)
		{
		}

		public ChildBlock2 ChildBlock2
		{
			get
			{
				return new ChildBlock2(this, Substitute.For<By>());
			}
		}

		public ChildBlock4 ChildBlock4
		{
			get
			{
				return new ChildBlock4(this, Substitute.For<By>());
			}
		}
	}

	public class ChildBlock1 : InstanceGuidBlock
	{
		public ChildBlock1(Session session, By @by) : base(session, @by)
		{
		}

		public ChildBlock1(IBlock parent, By @by) : base(parent, @by)
		{
		}
	}

	public class ChildBlock2 : InstanceGuidBlock
	{
		public ChildBlock2(Session session, By @by) : base(session, @by)
		{
		}

		public ChildBlock2(IBlock parent, By @by) : base(parent, @by)
		{
		}
	}

	public class ChildBlock3 : InstanceGuidBlock
	{
		public ChildBlock3(Session session, By @by) : base(session, @by)
		{
		}

		public ChildBlock3(IBlock parent, By @by) : base(parent, @by)
		{
		}
	}

	public class ChildBlock4 : InstanceGuidBlock
	{
		public ChildBlock4(Session session, By @by) : base(session, @by)
		{
		}

		public ChildBlock4(IBlock parent, By @by) : base(parent, @by)
		{
		}
	}
}