using System;

namespace AspNetCoreAnatomySamples.Infrasructure.Services
{
    public interface IMetricRecorder
    {
        public void RecordRequest(int statusCode, long milliseconds);

        public void RecordException(Exception ex);
    }
}
