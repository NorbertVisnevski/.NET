using APILibrary;
using Moq;
using NUnit.Framework;
using System;
using Tests.TestClasses;
using CsharpProject;

namespace Tests
{
    public class CacheTests
    {
        Cache cache;
        TestForecastProvider provider;
        [SetUp]
        public void Setup()
        {
            ForecastProvider.APIKey = "76d856611fmsha35ddf8e5adef10p1d1297jsn381734c61c69";
            provider = new TestForecastProvider(cache = new Cache());
        }

        [Test]
        public void webCacheTest1()
        {
            var request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Imperial)
                .setForecastType(ForecastType.Weekly)
                .buildRequest();
            provider.getForecast(request);
            provider.getForecast(request);
            Assert.AreEqual(provider.counter, 1);
        }
        [Test]
        public void webCacheTest2()
        {
            var request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Imperial)
                .setForecastType(ForecastType.Weekly)
                .buildRequest();
            provider.getForecast(request);
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Weekly)
                .buildRequest();
            provider.getForecast(request);
            Assert.AreEqual(provider.counter, 1);
        }
        [Test]
        public void webCacheTest3()
        {
            var request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Imperial)
                .setForecastType(ForecastType.Weekly)
                .buildRequest();
            provider.getForecast(request);
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Today)
                .buildRequest();
            provider.getForecast(request);
            Assert.AreEqual(provider.counter, 1);
        }
        [Test]
        public void webCacheTest4()
        {
            var request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Imperial)
                .setForecastType(ForecastType.Weekly)
                .buildRequest();
            provider.getForecast(request);
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Current)
                .buildRequest();
            provider.getForecast(request);
            Assert.AreEqual(provider.counter, 2);
        }
        [Test]
        public void webCacheTest5()
        {
            var request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Imperial)
                .setForecastType(ForecastType.Weekly)
                .buildRequest();
            provider.getForecast(request);
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Today)
                .buildRequest();
            provider.getForecast(request);
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("warsaw")
                .setCountry("pl")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Today)
                .buildRequest();
            provider.getForecast(request);
            Assert.AreEqual(provider.counter, 2);
        }
        [Test]
        public void localCacheTest1()
        {
            var request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("Berlin")
                .setCountry("de")
                .setUnityType(UnitType.Imperial)
                .setForecastType(ForecastType.Weekly)
                .buildRequest();
            cache.add(request, new Newtonsoft.Json.Linq.JObject());
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("Berlin")
                .setCountry("de")
                .setUnityType(UnitType.Imperial)
                .setForecastType(ForecastType.Tomorrow)
                .buildRequest();
            cache.add(request, new Newtonsoft.Json.Linq.JObject());
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Current)
                .buildRequest();
            cache.add(request, new Newtonsoft.Json.Linq.JObject());
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("vilnius")
                .setCountry("lt")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Today)
                .buildRequest();
            cache.add(request, new Newtonsoft.Json.Linq.JObject());
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("Berlin")
                .setCountry("de")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Weekly)
                .buildRequest();
            try
            {
                provider.getForecast(request);
            }
            catch (Exception) { }
            Assert.AreEqual(provider.counter, 0);
        }
        [Test]
        public void localCacheTest2()
        {
            var request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("Berlin")
                .setCountry("de")
                .setUnityType(UnitType.Imperial)
                .setForecastType(ForecastType.Tomorrow)
                .buildRequest();
            cache.add(request, new Newtonsoft.Json.Linq.JObject());
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("Berlin")
                .setCountry("de")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Current)
                .buildRequest();
            try
            {
                provider.getForecast(request);
            }
            catch (Exception) { }
            Assert.AreEqual(provider.counter, 1);
        }
        [Test]
        public void localCacheTest3()
        {
            var request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("Berlin")
                .setCountry("de")
                .setUnityType(UnitType.Imperial)
                .setForecastType(ForecastType.Tomorrow)
                .buildRequest();
            cache.add(request, new Newtonsoft.Json.Linq.JObject());
            request = new ForecastRequest.ForecastRequestBuilder()
                .setCity("Berlin")
                .setCountry("de")
                .setUnityType(UnitType.Metric)
                .setForecastType(ForecastType.Today)
                .buildRequest();
            try
            {
                provider.getForecast(request);
            }
            catch (Exception) { }
            Assert.AreEqual(provider.counter, 0);
        }
        [Test]
        public void localCacheTes4()
        {
            var request = new ForecastRequest.ForecastRequestBuilder().buildRequest();
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            cache.add(request, null);
            Assert.AreEqual(cache.Size, 10);
        }
    }

    public class StringTests
    {
        [Test]
        public void stringTest1()
        {
            string str= null;

            Assert.Throws<ArgumentNullException>(() => str.FirstCharToUpper());
        }
        [Test]
        public void stringTest2()
        {
            string str = "";

            Assert.Throws<ArgumentException>(() => str.FirstCharToUpper());
        }
        [Test]
        public void stringTest3()
        {
            string str = "vilnius";

            Assert.AreEqual(str.FirstCharToUpper(),"Vilnius");
        }
    }
}