<!-- List of VSTS Releases -->
<template>
    <!-- <div class="grid">
    </div> -->
    <v-data-table 
    :headers="headers"
    :items="items"
    :loading="isLoading" 
    class="elevation-1"
    >
    <template slot="items" slot-scope="{item}">
        <tr @click="selectBuild(item, $event)">
            <td>{{item.automation_build.name }}</td>
            <td class="text-xs-left">{{item.automation_build.hashed_id }}</td>
            <td class="text-xs-left">{{item.automation_build.status }}</td>
        </tr>
    </template>
  </v-data-table>
</template>

<script lang="ts">
/// <reference types="vss-web-extension-sdk" />
import { Vue, Prop, Watch } from "vue-property-decorator";
import Component from 'vue-class-component';
import * as TestManagementClient from "TFS/TestManagement/RestClient";
import * as Grids from "VSS/Controls/Grids";
import * as Controls from "VSS/Controls";
import * as WidgetHelpers from "TFS/Dashboards/WidgetHelpers";
import Release from '../models/release';

@Component({})
export default class Grid extends Vue {
    @Prop() items!: any [];
    @Prop() isLoading!: boolean;
    selectedRelease: any;
    headers = [
        { text: "Name", value: "releaseName", align: 'left', width: "100px"},
        { text: "Id", value: "releaseId", sortable: true, width: "60px" },
        { text: "Status", value: "passedTests", width: "70px" }
    ]
    constructor() {
        super();
    }
    @Watch('items', { immediate: true, deep: true })
    onItemChange (val: any, oldVal: any) {
        this.items = val;
    }
    selectBuild(release: any, event: any) {
        this.$emit('selectBuild', release);
    }
} 
</script>

<style>
.greeting {
    font-size: 20px;
    margin-bottom: 5px;
}
.menu {
     min-width: 50px;
}

tr {
    cursor: pointer;
}
</style>