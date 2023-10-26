namespace EFK.SampleApp.TelemetryService.Filter;

using Hangfire.Dashboard;

public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}
