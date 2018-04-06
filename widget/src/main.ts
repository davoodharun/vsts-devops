// tslint:disable-next-line
/// <reference types="vss-web-extension-sdk" />
import Vue from "vue";
import App from './App.vue';
import ReleaseListComponent from './components/ReleaseList.vue';
import Vuetify from 'vuetify';
import 'vuetify/dist/vuetify.min.css' // Ensure you are using css-loader
const moment = require('moment'); 
require('moment/locale/es');

VSS.require([
    "VSS/Controls",
    "Charts/Services",
    "VSS/Controls/Grids",
    "TFS/Dashboards/WidgetHelpers",
    "TFS/TestManagement/RestClient",
    "ReleaseManagement/Core/RestClient"
],  (Controls: any, Services: any, Grids: any, WidgetHelpers: any, TestClient: any, ReleaseClient: any) => {

    VSS.register("SeleniumExplorer", () => {
        var createWidget = function (widgetSettings: any) {
            var settings = JSON.parse(widgetSettings.customSettings.data);
            if (!settings || !settings.storageAccountUrl) {
                var $container = $('#storageAccountUrl');
                $container.empty();
                $container.text("Sorry nothing to show, please configure a query path.");
            }
            Vue.use(Vuetify, {
                theme: {
                    primary: '#106ebe',
                    secondary: '#b0bec5',
                    accent: '#8c9eff',
                    error: '#F44336',
                    success: "#33a73c"
                  }
            });

            Vue.use(require('vue-moment'), {
                moment
            })

            new Vue({
                el: '#app',
                template: '<App v-bind:storageAccountUrl="storageAccountUrl"/>',
                components: {
                    App
                },
                data () {
                    return {
                        storageAccountUrl: settings.storageAccountUrl
                    }
                }
            });

            //var releaseListComponent = new ReleaseListComponent ();

            return WidgetHelpers.WidgetStatusHelper.Success();  
        }
        return {
            load: (widgetSettings: any, Services: any) => {
                return createWidget(widgetSettings);
            },
            reload: function (widgetSettings: any) {
                return createWidget(widgetSettings);
            }
        }
    });
    VSS.notifyLoadSucceeded();
});


