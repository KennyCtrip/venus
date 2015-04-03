using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Venus.Tests
{
    interface IFoo
    {
        string Name { get; }
    }

    class Foo1 : IFoo
    {
        public string Name
        {
            get { return "Foo1"; }
        }
    }

    class Foo2 : IFoo
    {
        public string Name
        {
            get { return "Foo2"; }
        }
    }

    interface IBar
    {
        string Name { get; }
    }

    class Bar1 : IBar
    {
        public string Name
        {
            get { return "Bar1"; }
        }
    }

    class Bar2 : IBar
    {
        public string Name
        {
            get { return "Bar2"; }
        }
    }

    interface IFooBar
    {
        IFoo Foo { get; set; }
        IBar Bar { get; set; }
    }

    class FooBar1 : IFooBar
    {
        public FooBar1(IFoo foo)
        {
            throw new InvalidOperationException("This constructor should not be called by container.");
        }

        public FooBar1(IFoo foo, IBar bar)
        {
            Foo = foo;
            Bar = bar;
        }

        public IFoo Foo
        {
            get;
            set;
        }

        public IBar Bar
        {
            get;
            set;
        }
    }

    class FooBar2 : IFooBar
    {
        [InjectConstructor]
        public FooBar2(IFoo foo)
        {
            Foo = foo;
        }

        public FooBar2(IFoo foo, IBar bar)
        {
            throw new InvalidOperationException("This constructor should not be called by container.");
        }

        public IFoo Foo
        {
            get;
            set;
        }

        [Inject()]
        public IBar Bar
        {
            get;
            set;
        }
    }

    class FooBar3 : IFooBar
    {
        [InjectConstructor]
        public FooBar3([Inject("foo2")]IFoo foo)
        {
            Foo = foo;
        }

        public FooBar3(IFoo foo, IBar bar)
        {
            throw new InvalidOperationException("This constructor should not be called by container.");
        }

        public IFoo Foo
        {
            get;
            set;
        }

        [Inject("bar2")]
        public IBar Bar
        {
            get;
            set;
        }
    }
}
