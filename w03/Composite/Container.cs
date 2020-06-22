namespace arch.Composite
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Container Component: it can be Window, Frame, Panel ...
    /// </summary>
    public class Container : Component
    {
        private IList<Component> children = new List<Component>();

        public Container(string name) : base(name)
        { }

        public override void Add(Component c)
        {
            this.children.Add(c);
        }

        public override void Print()
        {
            Console.WriteLine("print {0}", this.name);
            foreach (var item in this.children)
            {
                item.Print();
            }
        }

        public override void Remove(Component c)
        {
            this.children.Remove(c);
        }
    }
}