namespace arch.Composite
{
    /// <summary>
    /// Form Component: it can be Button, Label, TextBox, CheckBox ...
    /// </summary>
    public class FormComponent : Component
    {
        public FormComponent(string name) : base(name)
        { }

        public override void Add(Component c)
        {
            throw new System.NotImplementedException();
        }

        public override void Remove(Component c)
        {
            throw new System.NotImplementedException();
        }
    }
}