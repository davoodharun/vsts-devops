// tslint:disable-next-line
/// <reference types="vss-web-extension-sdk" />
import Vue from "vue";
import ReleaseListComponent from "./components/ReleaseList.vue";

VSS.require([
    "VSS/Controls",
    "Charts/Services",
    "VSS/Controls/Grids",
    "TFS/Dashboards/WidgetHelpers",
    "TFS/TestManagement/RestClient",
    "ReleaseManagement/Core/RestClient"
],  (Controls: any, Services: any, Grids: any, WidgetHelpers: any, TestClient: any, ReleaseClient: any) => {

    WidgetHelpers.IncludeWidgetStyles();
    VSS.register("SeleniumExplorer", () => {
        var createWidget = (widgetSettings: any, Services: any) =>  {
            var releaseListComponent = new ReleaseListComponent();
            WidgetHelpers.WidgetStatusHelper.Success();              
            // Get a WIT client to make REST calls to VSTS
            return releaseListComponent;
        }
        return {
            load: (widgetSettings: any, Services: any) => {
                return createWidget(widgetSettings, Services);
            }
        }
    });
    VSS.notifyLoadSucceeded();
});


let v = new Vue({
    el: "#app",
    template: `
    <div>
        {{title}}
        <release-list-component/>
    </div>
    `,
    data () {
        return {
            title: "Selenium Test Explorer",
            name: "World",
            selected: {
                categories: []
            },
            selectedCategory: {},
            testCategories: {}
        } 
    
    },
    components: {
        ReleaseListComponent
    }
});