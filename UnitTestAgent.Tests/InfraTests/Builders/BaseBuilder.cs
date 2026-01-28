using AutoFixture;
using AutoFixture.AutoMoq;

namespace UnitTestAgend.Tests.InfraTests.Builders;

public abstract class BaseBuilder<T>
{
    private readonly IFixture _fixture;
    protected readonly T _class;

    public BaseBuilder()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _class = _fixture.Create<T>();
    }

    public T Build(bool useDefaultData = false)
    {
        if (useDefaultData)
            return Activator.CreateInstance<T>();

        return _class;
    }
}
