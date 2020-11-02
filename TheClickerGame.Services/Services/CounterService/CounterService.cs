using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TheClickerGame.Common.ClickUpgrades;
using TheClickerGame.Common.CountGenerators;

namespace TheClickerGame.Services.Services.CounterService
{
    public class CounterService : ICounterService
    {
        #region Private Properties

        private decimal _counter = 0;

        private static object locker = new object();

        private static object countGeneratorsLocker = new object();

        private static object clickUpgradesLocker = new object();
        
        private static object countGeneratorBuyingQueueLocker = new object();

        #endregion

        #region Public Properties

        public List<CountGenerator> CountGenerators { get; set; } = new List<CountGenerator>();

        public List<ClickUpgrade> ClickUpgrades { get; set; } = new List<ClickUpgrade>();
        
        public List<CountGenerator> CountGeneratorBuyingQueue { get; set; } = new List<CountGenerator>();

        public decimal Counter => _counter;

        public decimal GetTotalMultiplier()
        {
            lock (countGeneratorsLocker)
            {
                return CountGenerators.Sum(x => x.Quantity * x.Multiplier);
            }
        }

        public async Task<decimal> GetTotalMultiplierAsync()
        {
            return await Task.Run(
                       () =>
                       {
                           lock (countGeneratorsLocker)
                           {
                               return CountGenerators.Sum(x => x.Quantity * x.Multiplier);
                           }
                       });
        }

        public async Task<decimal> GetTotalClickMultiplierAsync()
        {
            return await Task.Run(
                       () =>
                       {
                           decimal value = 1;

                           lock (clickUpgradesLocker)
                           {
                               ClickUpgrades.ForEach(x => value = value * x.Multiplier);
                           }

                           return value;
                       }
                   );
        }

        public decimal GetTotalClickMultiplier()
        {
            decimal value = 1;

            lock (clickUpgradesLocker)
            {
                ClickUpgrades.ForEach(x => value = value * x.Multiplier);
            }

            return value;
        }

        #endregion

        #region Methods

        public async Task<decimal> GetGeneratorPriceAsync<T>(int amount = 1) where T : CountGenerator
        {
            return await Task.Run(
                       () =>
                       {
                           lock (countGeneratorsLocker)
                           {
                               var generator = CountGenerators.OfType<T>().FirstOrDefault();
                               var generatorCount = generator?.Quantity ?? 0;

                               var prevPrice = Activator.CreateInstance<T>().Price;
                               var price = prevPrice;

                               for (var i = 0; i < (generatorCount + amount); i++)
                               {
                                   if (i < generatorCount)
                                   {
                                       price = decimal.Add(prevPrice, prevPrice * 0.05M);
                                       prevPrice = price;
                                   }
                                   else if (amount == 1)
                                   {
                                       continue;
                                   }
                                   else
                                   {
                                       price += decimal.Add(prevPrice, prevPrice * 0.05M);
                                       prevPrice = decimal.Add(prevPrice, prevPrice * 0.05M);
                                   }
                               }

                               return Math.Ceiling(price);
                           }
                       }
                   );
        }

        public decimal GetGeneratorPrice<T>(int amount = 1) where T : CountGenerator
        {
            lock (countGeneratorsLocker)
            {
                var generator = CountGenerators.OfType<T>().FirstOrDefault();
                var generatorCount = generator?.Quantity ?? 0;

                var prevPrice = Activator.CreateInstance<T>().Price;
                var price = prevPrice;

                for (var i = 0; i < (generatorCount + amount); i++)
                {
                    if (i < generatorCount)
                    {
                        price = decimal.Add(prevPrice, prevPrice * 0.05M);
                        prevPrice = price;
                    }
                    else if (amount == 1)
                    {
                        continue;
                    }
                    else
                    {
                        price += decimal.Add(prevPrice, prevPrice * 0.05M);
                        prevPrice = decimal.Add(prevPrice, prevPrice * 0.05M);
                    }
                }

                return Math.Ceiling(price);
            }
        }

        public async Task<int> GetGeneratorCountAsync<T>() where T : CountGenerator
        {
            return await Task.Run(
                       () =>
                       {
                           lock (countGeneratorsLocker)
                           {
                               return CountGenerators.OfType<T>().FirstOrDefault()?.Quantity ?? 0;
                           }
                       });
        }

        public int GetGeneratorCount<T>() where T : CountGenerator
        {
            lock (countGeneratorsLocker)
            {
                return CountGenerators.OfType<T>().FirstOrDefault()?.Quantity ?? 0;
            }
        }

        public async Task<bool> HasGeneratorAsync<T>() where T : CountGenerator
        {
            return await Task.Run(
                       () =>
                       {
                           lock (countGeneratorsLocker)
                           {
                               return CountGenerators.Any(x => x.GetType() == typeof(T));
                           }
                       }
                   );
        }

        public bool HasGenerator<T>() where T : CountGenerator
        {
            lock (countGeneratorsLocker)
            {
                return CountGenerators.Any(x => x.GetType() == typeof(T));
            }
        }

        public async Task<bool> CanBuyGeneratorAsync<T>(int amount = 1) where T : CountGenerator
        {
            var price = await GetGeneratorPriceAsync<T>(amount);

            return decimal.Compare(price, Counter) <= 0;
        }

        public bool CanBuyGenerator<T>(int amount = 1) where T : CountGenerator
        {
            var price = GetGeneratorPrice<T>(amount);

            return decimal.Compare(price, Counter) <= 0;
        }

        public async Task BuyGeneratorAsync<T>(int amount = 1) where T : CountGenerator
        {
            try
            {
                var canBuyGenerator = await CanBuyGeneratorAsync<T>(amount);

                if (canBuyGenerator)
                {
                    var price = await GetGeneratorPriceAsync<T>(amount);
                    var hasGenerator = await HasGeneratorAsync<T>();

                    if (hasGenerator)
                    {
                        lock (countGeneratorsLocker)
                        {
                            var generatorIndex = CountGenerators.FindIndex(x => x.GetType() == typeof(T));
                            CountGenerators[generatorIndex].Quantity += amount;
                        }
                    }
                    else
                    {
                        var generator = Activator.CreateInstance<T>();
                        generator.Quantity = amount;

                        lock (countGeneratorsLocker)
                        {
                            CountGenerators.Add(generator);
                        }
                    }

                    DecrementCounter(price);
                }
            }
            catch { }
        }

        public void BuyGenerator<T>(int amount = 1) where T : CountGenerator
        {
            try
            {
                var canBuyGenerator = CanBuyGenerator<T>(amount);

                if (canBuyGenerator)
                {
                    var price = GetGeneratorPrice<T>(amount);
                    var hasGenerator = HasGenerator<T>();

                    if (hasGenerator)
                    {
                        lock (countGeneratorsLocker)
                        {
                            var generatorIndex = CountGenerators.FindIndex(x => x.GetType() == typeof(T));
                            CountGenerators[generatorIndex].Quantity += amount;
                        }
                    }
                    else
                    {
                        var generator = Activator.CreateInstance<T>();
                        generator.Quantity = amount;

                        lock (countGeneratorsLocker)
                        {
                            CountGenerators.Add(generator);
                        }
                    }

                    DecrementCounter(price);
                }
            }
            catch { }
        }

        public async Task QueueGeneratorAsync<T>(int amount = 1) where T : CountGenerator
        {
            await Task.Run(() =>
            {
                lock (countGeneratorBuyingQueueLocker)
                {
                    var generator = CountGeneratorBuyingQueue.FirstOrDefault(x => x.GetType() == typeof(T));

                    if (generator != null)
                    {
                        generator.Quantity += amount;
                    }
                    else
                    {
                        generator = Activator.CreateInstance<T>();
                        generator.Quantity = amount;
                        CountGeneratorBuyingQueue.Add(generator);
                    }
                }
            });
        }

        public void QueueGenerator<T>(int amount = 1) where T : CountGenerator
        {
            lock (countGeneratorBuyingQueueLocker)
            {
                var generator = CountGeneratorBuyingQueue.FirstOrDefault(x => x.GetType() == typeof(T));

                if (generator != null)
                {
                    generator.Quantity += amount;
                }
                else
                {
                    generator = Activator.CreateInstance<T>();
                    generator.Quantity = amount;
                    CountGeneratorBuyingQueue.Add(generator);
                }
            }
        }

        public async Task CheckGeneratorQueueAsync()
        {
            await Task.Run(() =>
            {
                lock (countGeneratorBuyingQueueLocker)
                {
                    if (!CountGeneratorBuyingQueue.Any()) return;
                    
                    var ordered = CountGeneratorBuyingQueue.OrderBy(x => GetType().GetMethod("GetGeneratorPrice").MakeGenericMethod(x.GetType()).Invoke(this, null));
                    var canBuy = (bool) GetType().GetMethod("CanBuyGenerator").MakeGenericMethod(ordered.First().GetType()).Invoke(this, null);

                    if (canBuy)
                    {
                        GetType().GetMethod("BuyGenerator").MakeGenericMethod(ordered.First().GetType()).Invoke(this, null);

                        if (ordered.First().Quantity > 1)
                        {
                            ordered.First().Quantity -= 1;
                        }
                        else
                        {
                            CountGeneratorBuyingQueue.Remove(ordered.First());
                        }
                    }
                }
            });
        }

        public void CheckGeneratorQueue()
        {
            lock (countGeneratorBuyingQueueLocker)
            {
                if (!CountGeneratorBuyingQueue.Any()) return;
                
                var ordered = CountGeneratorBuyingQueue.OrderBy(x => GetType().GetMethod("GetGeneratorPrice").MakeGenericMethod(x.GetType()).Invoke(this, null));
                var canBuy = (bool) GetType().GetMethod("CanBuyGenerator").MakeGenericMethod(ordered.First().GetType()).Invoke(this, null);

                if (canBuy)
                {
                    GetType().GetMethod("BuyGenerator").MakeGenericMethod(ordered.First().GetType()).Invoke(this, null);

                    if (ordered.First().Quantity > 1)
                    {
                        ordered.First().Quantity -= 1;
                    }
                    else
                    {
                        CountGeneratorBuyingQueue.Remove(ordered.First());
                    }
                }
            }
        }

        public async Task<bool> HasUpgradeAsync<T>() where T : ClickUpgrade
        {
            return await Task.Run(
                       () =>
                       {
                           lock (clickUpgradesLocker)
                           {
                               return ClickUpgrades.Any(x => x.GetType() == typeof(T));
                           }
                       });
        }

        public bool HasUpgrade<T>() where T : ClickUpgrade
        {
            lock (clickUpgradesLocker)
            {
                return ClickUpgrades.Any(x => x.GetType() == typeof(T));
            }
        }

        public async Task<bool> CanBuyUpgradeAsync<T>() where T : ClickUpgrade
        {
            return decimal.Compare(Activator.CreateInstance<T>().Price, Counter) <= 0 && !(await HasUpgradeAsync<T>());
        }

        public bool CanBuyUpgrade<T>() where T : ClickUpgrade
        {
            return decimal.Compare(Activator.CreateInstance<T>().Price, Counter) <= 0 && !(HasUpgrade<T>());
        }

        public async Task BuyUpgradeAsync<T>() where T : ClickUpgrade
        {
            var canBuyUpgrade = await CanBuyUpgradeAsync<T>();

            if (canBuyUpgrade)
            {
                var upgrade = Activator.CreateInstance<T>();

                lock (clickUpgradesLocker)
                {
                    ClickUpgrades.Add(upgrade);
                }
                
                DecrementCounter(upgrade.Price);
            }
        }

        public void BuyUpgrade<T>() where T : ClickUpgrade
        {
            var canBuyUpgrade = CanBuyUpgrade<T>();

            if (canBuyUpgrade)
            {
                var upgrade = Activator.CreateInstance<T>();

                lock (clickUpgradesLocker)
                {
                    ClickUpgrades.Add(upgrade);
                }

                DecrementCounter(upgrade.Price);
            }
        }

        public void DecrementCounter(decimal amount)
        {
            lock (locker)
            {
                var value = decimal.Subtract(_counter, amount);

                if (decimal.Compare(0, value) <= 0)
                    _counter = value;
                else
                    throw new ArgumentOutOfRangeException("Counter", Counter, $"Insufficient count");

                var handler = CounterChanged;
                handler?.Invoke(null, EventArgs.Empty);
            }
        }

        public async Task IncrementCounterAsync(decimal? amount = null)
        {
            await Task.Run(
                () =>
                {
                    lock (countGeneratorsLocker)
                    {
                        lock (locker)
                        {
                            _counter = decimal.Add(_counter, amount ?? CountGenerators.Sum(x => x.Quantity * x.Multiplier) * 0.25M);

                            // var handler = CounterChanged;
                            // handler?.Invoke(null, EventArgs.Empty);
                        }
                    }
                }
            );
        }

        public void IncrementCounter(decimal? amount = null)
        {
            lock (countGeneratorsLocker)
            {
                lock (locker)
                {
                    _counter = decimal.Add(_counter, amount ?? CountGenerators.Sum(x => x.Quantity * x.Multiplier) * 0.25M);

                    // var handler = CounterChanged;
                    // handler?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public async Task IncrementCounterWithoutEventAsync(decimal? amount = null)
        {
            var value = await GetTotalClickMultiplierAsync();

            lock (countGeneratorsLocker)
            {
                lock (locker)
                {
                    _counter = decimal.Add(_counter, amount ?? value);
                }
            }
        }

        public void IncrementCounterWithoutEvent(decimal? amount = null)
        {
            var value = GetTotalClickMultiplier();

            lock (countGeneratorsLocker)
            {
                lock (locker)
                {
                    _counter = decimal.Add(_counter, amount ?? value);
                }
            }
        }

        #endregion

        #region Events

        public event EventHandler CounterChanged;

        #endregion
    }
}