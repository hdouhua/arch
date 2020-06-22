namespace arch.Composite
{
    using System;

    public abstract class Component
    {
        protected string name;

        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component c);

        public abstract void Remove(Component c);

        public virtual void Print()
        {
            Console.WriteLine("print {0}", this.name);
        }
    }
}