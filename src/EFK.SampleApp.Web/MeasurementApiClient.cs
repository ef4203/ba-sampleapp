// Copyright (c) Elias Frank. All rights reserved.

namespace EFK.SampleApp.Web;

using EFK.SampleApp.Common;

public class MeasurementApiClient(HttpClient httpClient)
{
    public async Task<Measurement[]> GetWeatherAsync()
    {
        // Use the httpClient to send an HTTP GET request to the specified API endpoint.
        // If the response is null (no data received), return an empty array of Measurement objects.
        return await httpClient.GetFromJsonAsync<Measurement[]>("/api/measurements")
                .ConfigureAwait(false) ?? [];
    }
}
