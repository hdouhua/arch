
namespace arch.Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Container("WinForm(WINDOW窗口)");
            root.Add(new FormComponent("Picture(LOGO图片)"));
            root.Add(new FormComponent("Button(登录)"));
            root.Add(new FormComponent("Button(注册)"));

            var frame = new Container("Frame(FRAME1)");
            frame.Add(new FormComponent("Label(用户名)"));
            frame.Add(new FormComponent("TextBox(文本框)"));
            frame.Add(new FormComponent("Label(密码)"));
            frame.Add(new FormComponent("PasswordBox(密码框)"));
            frame.Add(new FormComponent("CheckBox(复选框)"));
            frame.Add(new FormComponent("TextBox(记住用户名)"));
            frame.Add(new FormComponent("LinkLabel(忘记密码)"));
            root.Add(frame);

            root.Print();
        }
    }
}
