using FourtitudeAsiaTest.BLL;
using Microsoft.Extensions.DependencyInjection;

namespace FourtitudeAsiaTest.Tests;

public class ItemServiceTests
{
    private readonly IItemBLL _itemBLL;

    public ItemServiceTests()
    {
        var services = new ServiceCollection();

        // Register Dependencies
        services.AddTransient<IItemBLL, ItemBLL>();

        _itemBLL = services.BuildServiceProvider().GetRequiredService<IItemBLL>();
    }

    [Fact]
    public void CalculateFinalAmount_Returns_No_Discount()
    {
        // Act
        var result = _itemBLL.CalculateFinalAmount(19900);

        // Assert
        Assert.Equal(0, result.TotalDiscount);
        Assert.Equal(19900, result.FinalAmount);
    }

    [Fact]
    public void CalculateFinalAmount_Returns_Discount_5Percent()
    {
        // Act
        var result = _itemBLL.CalculateFinalAmount(20000);

        // Assert
        Assert.Equal(1000, result.TotalDiscount);
        Assert.Equal(19000, result.FinalAmount);
    }

    [Fact]
    public void CalculateFinalAmount_Returns_Discount_7Percent()
    {
        // Act
        var result = _itemBLL.CalculateFinalAmount(60000);

        // Assert
        Assert.Equal(4200, result.TotalDiscount);
        Assert.Equal(55800, result.FinalAmount);
    }

    [Fact]
    public void CalculateFinalAmount_Returns_Discount_7Percent_Prime_8Percent()
    {
        // Act
        var result = _itemBLL.CalculateFinalAmount(61500);

        // Assert
        Assert.Equal(9225, result.TotalDiscount);
        Assert.Equal(52275, result.FinalAmount);
    }

    [Fact]
    public void CalculateFinalAmount_Returns_Discount_10Percent()
    {
        // Act
        var result = _itemBLL.CalculateFinalAmount(90000);

        // Assert
        Assert.Equal(9000, result.TotalDiscount);
        Assert.Equal(81000, result.FinalAmount);
    }

    [Fact]
    public void CalculateFinalAmount_Returns_Discount_10Percent_Digit5_10Percent()
    {
        // Act
        var result = _itemBLL.CalculateFinalAmount(90500);

        // Assert
        Assert.Equal(18100, result.TotalDiscount);
        Assert.Equal(72400, result.FinalAmount);
    }

    [Fact]
    public void CalculateFinalAmount_Returns_Discount_15Percent()
    {
        // Act
        var result = _itemBLL.CalculateFinalAmount(125000);

        // Assert
        Assert.Equal(18750, result.TotalDiscount);
        Assert.Equal(106250, result.FinalAmount);
    }

    [Fact]
    public void CalculateFinalAmount_Returns_Discount_15Percent_Prime_8Percent_NotOver20()
    {
        // Act
        var result = _itemBLL.CalculateFinalAmount(125100);

        // Assert
        Assert.Equal(25020, result.TotalDiscount);
        Assert.Equal(100080, result.FinalAmount);
    }

    [Fact]
    public void CalculateFinalAmount_Returns_Discount_15Percent_Digit5_10Percent_NotOver20()
    {
        // Act
        var result = _itemBLL.CalculateFinalAmount(125500);

        // Assert
        Assert.Equal(25100, result.TotalDiscount);
        Assert.Equal(100400, result.FinalAmount);
    }


}
