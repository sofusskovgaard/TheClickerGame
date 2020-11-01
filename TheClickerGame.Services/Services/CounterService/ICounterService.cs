using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using TheClickerGame.Common.ClickUpgrades;
using TheClickerGame.Common.CountGenerators;

namespace TheClickerGame.Services.Services.CounterService
{
    public interface ICounterService
    {
        event EventHandler CounterChanged;

        List<CountGenerator> CountGenerators { get; set; }

        List<ClickUpgrade> ClickUpgrades { get; set; }

        decimal Counter { get; }


        Task<decimal> GetTotalMultiplierAsync();

        decimal GetTotalMultiplier();

        Task<decimal> GetTotalClickMultiplierAsync();

        decimal GetTotalClickMultiplier();


        Task<decimal> GetGeneratorPriceAsync<T>(int amount = 1) where T : CountGenerator;

        decimal GetGeneratorPrice<T>(int amount = 1) where T : CountGenerator;


        Task<int> GetGeneratorCountAsync<T>() where T : CountGenerator;

        int GetGeneratorCount<T>() where T : CountGenerator;

        Task<bool> HasGeneratorAsync<T>() where T : CountGenerator;

        bool HasGenerator<T>() where T : CountGenerator;


        Task<bool> CanBuyGeneratorAsync<T>(int amount = 1) where T : CountGenerator;

        bool CanBuyGenerator<T>(int amount = 1) where T : CountGenerator;


        Task BuyGeneratorAsync<T>(int amount = 1) where T : CountGenerator;

        void BuyGenerator<T>(int amount = 1) where T : CountGenerator;

        Task<bool> HasUpgradeAsync<T>() where T : ClickUpgrade;

        bool HasUpgrade<T>() where T : ClickUpgrade;

        Task<bool> CanBuyUpgradeAsync<T>() where T : ClickUpgrade;

        bool CanBuyUpgrade<T>() where T : ClickUpgrade;


        Task BuyUpgradeAsync<T>() where T : ClickUpgrade;

        void BuyUpgrade<T>() where T : ClickUpgrade;


        void DecrementCounter(decimal amount);

        Task IncrementCounterAsync(decimal? amount = null);
        void IncrementCounter(decimal? amount = null);

        Task IncrementCounterWithoutEventAsync(decimal? amount = null);
        void IncrementCounterWithoutEvent(decimal? amount = null);
    }
}
