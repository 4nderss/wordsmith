import {inject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {getHttpConfig} from "./configuration";
import {CommonVariables} from "../commonvariables";

export class ClientContainer {

    private httpClient: HttpClient;

    static inject = [HttpClient, CommonVariables]
    constructor(httpClient: HttpClient, commonVariables: CommonVariables) {
        this.httpClient = httpClient;
        this.httpClient.configure(config => getHttpConfig(config, commonVariables.applicationSettings));
    }

    getClient(): HttpClient {        
        return this.httpClient;
    }
}