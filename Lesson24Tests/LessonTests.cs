using Lesson24;
using Moq;

namespace Lesson24Tests;

public class LessonTests
{
    [Theory]
    [InlineData(6, 1, 2, 3)]
    public void TestMultiplication2(int expected, params int[] args)
    {
        var realClass = new MyClass();
        Assert.Equal(realClass.MulMethod(args), expected);
    }

    [Theory]
    [InlineData(new[] {1, 2, 3}, 6)]
    public void TestMultiplication(int[] args, int expected)
    {
        var realClass = new MyClass();
        Assert.Equal(realClass.MulMethod(args), expected);
    }

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 3, 5)]
    [InlineData(10, 5, 15)]
    [InlineData(100, 5, 105)]
    public void TestAddition(int a, int b, int expected)
    {
        // Act
        int result = a + b;
        // Assert
        Assert.Equal(expected, result);
    }


    [Fact]
    public void MyTestMethod()
    {
        // Arrange
        var myInterfaceMock = new Mock<IMyInterface>();
        myInterfaceMock.Setup(x => x.MyMethod(5, 10)).Returns(15);

        var myClass = new MyClass(myInterfaceMock.Object);

        // Act
        int result = myClass.MyMethod(5, 10);

        // Assert
        Assert.Equal(15, result);
    }
}