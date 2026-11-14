using Teambase.Assessment.Application.Services;
using Teambase.Assessment.Domain.Enums;

namespace Teambase.Assessment.Tests;

public class PremiumCalculatorTests
{
    private readonly PremiumCalculator _calculator = new();

    [Theory]
    [InlineData(5, 500)]
    [InlineData(15, 3000)]
    [InlineData(25, 7500)]
    public void AgeRated_uses_decade_multiplier(int age, decimal expectedFull)
    {
        var result = PremiumCalculator.CalculateFullPremium(age, Gender.Male, PricingModel.AgeRated);
        Assert.Equal(expectedFull, result);
    }

    [Fact]
    public void FlatRate_is_1000()
    {
        var premium = PremiumCalculator.CalculateFullPremium(40, Gender.Female, PricingModel.FlatRate);
        Assert.Equal(1000m, premium);
    }

    [Fact]
    public void Female_under_18_matches_age_rated()
    {
        var ageRated = PremiumCalculator.CalculateFullPremium(17, Gender.Female, PricingModel.AgeRated);
        var genderRated = PremiumCalculator.CalculateFullPremium(17, Gender.Female, PricingModel.GenderAgeRated);
        Assert.Equal(ageRated, genderRated);
    }

    [Fact]
    public void Female_from_18_applies_multiplier()
    {
        var ageRated = PremiumCalculator.CalculateFullPremium(18, Gender.Female, PricingModel.AgeRated);
        var genderRated = PremiumCalculator.CalculateFullPremium(18, Gender.Female, PricingModel.GenderAgeRated);
        Assert.Equal(ageRated * 1.5m, genderRated);
    }

    [Fact]
    public void ProrateByDays_march1_to_dec31_example()
    {
        var policyStart = new DateOnly(2025, 1, 1);
        var policyEnd = new DateOnly(2025, 12, 31);
        var addition = new DateOnly(2025, 3, 1);
        var dob = new DateOnly(1990, 1, 1);

        var result = _calculator.Calculate(
            dob, Gender.Male, addition, policyStart, policyEnd,
            PricingModel.FlatRate, ProrataMethod.ByDays);

        Assert.Equal(306, result.RemainingDays);
        Assert.Equal(838.36m, result.ProratedPremium);
    }

    [Fact]
    public void ProrateByMonth_march15_to_dec31_example()
    {
        var policyStart = new DateOnly(2025, 1, 1);
        var policyEnd = new DateOnly(2025, 12, 31);
        var addition = new DateOnly(2025, 3, 15);
        var dob = new DateOnly(1990, 1, 1);

        var result = _calculator.Calculate(
            dob, Gender.Male, addition, policyStart, policyEnd,
            PricingModel.FlatRate, ProrataMethod.ByMonth);

        Assert.Equal(10, result.RemainingMonths);
        Assert.Equal(833.33m, result.ProratedPremium);
    }
}