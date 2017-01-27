import {inject} from "aurelia-framework";
import {ApplicationSettings} from "./applicationsettings";


export class CommonVariables {

    static inject = [ApplicationSettings]
    constructor(public applicationSettings: ApplicationSettings) {

    }
}