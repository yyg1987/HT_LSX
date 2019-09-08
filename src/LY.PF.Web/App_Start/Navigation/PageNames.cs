namespace LY.PF.Web.Navigation
{
    public static class PageNames
    {
        public static class App
        {
            public static class Common
            {
                public const string Administration = "Administration";
                public const string Roles = "Administration.Roles";
                public const string Users = "Administration.Users";
                public const string AuditLogs = "Administration.AuditLogs";
                public const string OrganizationUnits = "Administration.OrganizationUnits";
                public const string Languages = "Administration.Languages";
            }

            public static class Host
            {
                public const string Tenants = "Tenants";
                public const string Editions = "Editions";
                public const string Maintenance = "Administration.Maintenance";
                public const string Settings = "Administration.Settings.Host";
            }

            public static class Tenant
            {
                public const string Dashboard = "Dashboard.Tenant";
                public const string Settings = "Administration.Settings.Tenant";
            }

            public static class Chart
            {
                public const string ChartData = "ChartData";
                // public const string SaleProgram = "ChartData.SaleProgram";
                public const string HistogramByMonth = "ChartData.HistogramByMonth";
                public const string HistogramByArea = "ChartData.HistogramByArea";
                public const string HistogramByType = "ChartData.HistogramByType";
                public const string Pie = "ChartData.Pie";
                public const string AreaPie = "ChartData.AreaPie";
                public const string ProductTypePie = "ChartData.ProductTypePie";

            }

        }

        public static class Frontend
        {
            public const string Home = "Frontend.Home";
            public const string About = "Frontend.About";
        }
    }
}