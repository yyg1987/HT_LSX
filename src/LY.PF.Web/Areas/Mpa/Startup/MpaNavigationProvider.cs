using Abp.Application.Navigation;
using Abp.Localization;
using LY.PF.Authorization;
using LY.PF.ClientTypes.Authorization;
using LY.PF.Districts.Authorization;
using LY.PF.Indents.Authorization;
using LY.PF.ProductTypes.Authorization;
using LY.PF.SaleFunnels.Authorization;
using LY.PF.SaleOrders.Authorization;
using LY.PF.Web.Navigation;

namespace LY.PF.Web.Areas.Mpa.Startup
{
    public class MpaNavigationProvider : NavigationProvider
    {
        public const string MenuName = "Mpa";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                .AddItem(new MenuItemDefinition(
                    PageNames.App.Host.Tenants,
                    L("Tenants"),
                    url: "Mpa/Tenants",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_Tenants
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Host.Editions,
                    L("Editions"),
                    url: "Mpa/Editions",
                    icon: "icon-grid",
                    requiredPermissionName: AppPermissions.Pages_Editions
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Dashboard,
                    L("Dashboard"),
                    url: "Mpa/Dashboard",
                    icon: "icon-home",
                    requiredPermissionName: AppPermissions.Pages_Tenant_Dashboard
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Common.Administration,
                    L("Administration"),
                    icon: "icon-wrench"
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.OrganizationUnits,
                        L("OrganizationUnits"),
                        url: "Mpa/OrganizationUnits",
                        icon: "icon-layers",
                        requiredPermissionName: AppPermissions.Pages_Administration_OrganizationUnits
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.Roles,
                        L("Roles"),
                        url: "Mpa/Roles",
                        icon: "icon-briefcase",
                        requiredPermissionName: AppPermissions.Pages_Administration_Roles
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.Users,
                        L("Users"),
                        url: "Mpa/Users",
                        icon: "icon-users",
                        requiredPermissionName: AppPermissions.Pages_Administration_Users
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.Languages,
                        L("Languages"),
                        url: "Mpa/Languages",
                        icon: "icon-flag",
                        requiredPermissionName: AppPermissions.Pages_Administration_Languages
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.AuditLogs,
                        L("AuditLogs"),
                        url: "Mpa/AuditLogs",
                        icon: "icon-lock",
                        requiredPermissionName: AppPermissions.Pages_Administration_AuditLogs
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Host.Maintenance,
                        L("Maintenance"),
                        url: "Mpa/Maintenance",
                        icon: "icon-wrench",
                        requiredPermissionName: AppPermissions.Pages_Administration_Host_Maintenance
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.Settings,
                        L("Settings"),
                        url: "Mpa/HostSettings",
                        icon: "icon-settings",
                        requiredPermissionName: AppPermissions.Pages_Administration_Host_Settings
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Tenant.Settings,
                        L("Settings"),
                        url: "Mpa/Settings",
                        icon: "icon-settings",
                        requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
                        )
                    )
                )
                .AddItem(new MenuItemDefinition(
                SaleFunnelAppPermissions.SaleFunnel,
                L("SaleFunnel"),
                icon: "icon-grid"
                ).AddItem(
                    new MenuItemDefinition(
                    ClientTypeAppPermissions.ClientType,
                    L("ClientType"),
                    "icon-star",
                    url: "Mpa/ClientTypeManage",
                    requiredPermissionName: ClientTypeAppPermissions.ClientType))
                    .AddItem(
                    new MenuItemDefinition(
                    DistrictAppPermissions.District,
                    L("District"),
                    "icon-star",
                    url: "Mpa/DistrictManage",
                    requiredPermissionName: DistrictAppPermissions.District))
                    .AddItem(
                    new MenuItemDefinition(
                    ProductTypeAppPermissions.ProductType,
                    L("ProductType"),
                    "icon-star",
                    url: "Mpa/ProductTypeManage",
                    requiredPermissionName: ProductTypeAppPermissions.ProductType))
                    .AddItem(
                    new MenuItemDefinition(
                    SaleOrderAppPermissions.SaleOrder,
                    L("SaleOrder"),
                    "icon-star",
                    url: "Mpa/SaleOrderManage",
                    requiredPermissionName: SaleOrderAppPermissions.SaleOrder))
                     .AddItem(
                    new MenuItemDefinition(
                    SaleFunnelAppPermissions.SaleFunnel,
                    L("SaleFunnel"),
                    "icon-star",
                    url: "Mpa/SaleFunnelManage",
                    requiredPermissionName: SaleFunnelAppPermissions.SaleFunnel))
                    )
                .AddItem(new MenuItemDefinition(
                IndentAppPermissions.Indent,
                L("Indent"),
                icon: "icon-grid"
                ).AddItem(
                    new MenuItemDefinition(
                    IndentAppPermissions.Indent,
                    L("Indent"),
                    "icon-star",
                    url: "Mpa/IndentManage",
                    requiredPermissionName: IndentAppPermissions.Indent)))
                //销售漏斗图表
                .AddItem(new MenuItemDefinition(
                    PageNames.App.Chart.ChartData,
                        L("ChartData"),
                        icon: "icon-grid"
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Chart.HistogramByMonth,
                        L("HistogramByMonth"),
                        "icon-star",
                        url: "Mpa/Highcharts",
                        requiredPermissionName: SaleFunnelAppPermissions.SaleFunnel

                        )).AddItem(new MenuItemDefinition(
                        PageNames.App.Chart.HistogramByArea,
                        L("HistogramByArea"),
                        "icon-star",
                        url: "Mpa/Highcharts/AreaIndex",
                        requiredPermissionName: SaleFunnelAppPermissions.SaleFunnel

                        )).AddItem(new MenuItemDefinition(
                        PageNames.App.Chart.HistogramByType,
                        L("HistogramByType"),
                        "icon-star",
                        url: "Mpa/Highcharts/HospitalIndex",
                        requiredPermissionName: SaleFunnelAppPermissions.SaleFunnel

                        )).AddItem(new MenuItemDefinition(
                        PageNames.App.Chart.Pie,
                        L("Pie"),
                        "icon-star",
                        url: "Mpa/Highcharts/Pie",
                        requiredPermissionName: SaleFunnelAppPermissions.SaleFunnel

                        )).AddItem(new MenuItemDefinition(
                        PageNames.App.Chart.AreaPie,
                        L("AreaPie"),
                        "icon-star",
                        url: "Mpa/Highcharts/AreaPie",
                        requiredPermissionName: SaleFunnelAppPermissions.SaleFunnel

                        )).AddItem(new MenuItemDefinition(
                        PageNames.App.Chart.ProductTypePie,
                        L("ProductTypePie"),
                        "icon-star",
                        url: "Mpa/Highcharts/ProductTypePie",
                        requiredPermissionName: SaleFunnelAppPermissions.SaleFunnel

                        )))

                ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PFConsts.LocalizationSourceName);
        }
    }
}