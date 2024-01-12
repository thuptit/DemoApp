using DemoApp;

var container = new Container();
container.Register<IA, A>().AsPerScope();
container.Register<IB, B>().AsPerScope();

using(var scope = container.CreateScope())
{
    var a1 = (IA)scope.GetService(typeof(IA));
    a1.GetInstanceId();
}

var a = container.Resolve<IA>();
a.GetInstanceId();

interface IA
{
    void GetInstanceId();
}
class A : IA
{
    private readonly IB _b;
    public A(IB b)
    {
        _b = b;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public void GetInstanceId()
    {
        Console.WriteLine("Class A: " + Id);
        _b.GetInstanceId();
    }
}

interface IB
{
    void GetInstanceId();
}
class B : IB
{
    public B()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public void GetInstanceId()
    {
        Console.WriteLine("Class B: " + Id);
    }
}