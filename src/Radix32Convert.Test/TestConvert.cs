using Xunit;
using Xunit.Abstractions;
using System.Numerics;
using System.Linq;
using System;
namespace Radix32Convert.Test
{
    public class TestConvert : IDisposable
    {
        ITestOutputHelper m_OutputHelper;
        public TestConvert(ITestOutputHelper outputHelper)
        {
            m_OutputHelper = outputHelper;
            m_OutputHelper.WriteLine("mogemoge");
        }
        [Theory]
        [InlineData(100L)]
        [InlineData(-100L)]
        [InlineData(long.MaxValue)]
        [InlineData(long.MinValue)]
        [InlineData(0L)]
        public void ConvertTest(long value)
        {
            m_OutputHelper.WriteLine($"test value is {value}");
            var expected = new BigInteger(value);
            var s = Radix32Converter.ConvertToString(expected);
            var b = Radix32Converter.ConvertToInteger(s);
            Assert.Equal(expected, b);
            m_OutputHelper.WriteLine($"convert result is {value} => {s}");
        }
        [Theory]
        [InlineData(128/8)]
        [InlineData(192/8)]
        public void ConvertBinaryTest(int datalength)
        {
            var rnd = new Random();
            var b = Enumerable.Range(0, datalength).Select(i => (byte)rnd.Next(1,254))
                .Concat(new byte[1] { 0 }).ToArray()
                ;
            var bstr = string.Join("", b.Select(x => x.ToString("x2")));
            m_OutputHelper.WriteLine($"{bstr}");
            var expected = new BigInteger(b);
            var s = Radix32Converter.ConvertToString(expected);
            Assert.Equal(expected, Radix32Converter.ConvertToInteger(s));
            m_OutputHelper.WriteLine($"convert result is {bstr} => {s}");
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    m_OutputHelper.WriteLine("disposing");
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TestConvert() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}