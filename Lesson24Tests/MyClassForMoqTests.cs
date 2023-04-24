using Lesson24;
using Moq;

namespace Lesson24Tests;

public class MyClassForMoqTests
{
    private readonly MyClassForMoq _classObject;
    
    public MyClassForMoqTests()
    {
        var myDependencyMock = new Mock<IMyDependency>();
        myDependencyMock.Setup(x => x.GetNumber()).Returns(5);
        myDependencyMock.Setup(x => x.GetString()).Returns("test");
        _classObject = new MyClassForMoq(myDependencyMock.Object);
    }
    
    [Fact]
    public void MyMethod_ReturnsExpectedResult()
    {
        // Arrange
        // var myDependencyMock = new Mock<IMyDependency>();
        // myDependencyMock.Setup(x => x.GetNumber()).Returns(5);
        // myDependencyMock.Setup(x => x.GetString()).Returns("test");
        // var myClass = new MyClassForMoq(myDependencyMock.Object);
        // Act
        string result = _classObject.MyMethod();
        // Assert
        Assert.Equal("Number: 5, String: test", result);
    }

    [Fact]
    public void MyOtherMethod_ReturnsExpectedResult()
    {
        // Arrange
        // var myDependencyMock = new Mock<IMyDependency>();
        // myDependencyMock.Setup(x => x.GetNumber()).Returns(10);
        // var myClass = new MyClassForMoq(myDependencyMock.Object);
        // Act
        int result = _classObject.MyOtherMethod();
        // Assert
        Assert.Equal(5, result);
    }
}