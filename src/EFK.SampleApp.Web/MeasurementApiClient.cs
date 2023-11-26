// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.Web;

using EFK.SampleApp.Common;

public class MeasurementApiClient(HttpClient httpClient)
{
    public async Task<Measurement[]> GetWeatherAsync()
    {
        return await httpClient.GetFromJsonAsync<Measurement[]>("/api/measurements")
                .ConfigureAwait(false)
            ?? [];
    }
}
