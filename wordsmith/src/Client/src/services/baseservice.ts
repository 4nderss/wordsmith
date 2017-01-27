import {json} from 'aurelia-fetch-client'
import {ClientContainer} from "./clientcontainer";

export class BaseService {


    constructor(public httpClientContainer: ClientContainer) {

    }

    protected get<T>(url: string, responseHandler: Function): Promise<T> {

        return this
            .sendRequest(url, 'GET')
            .then(response => { return responseHandler(response); });
    }

    private sendRequest(url: string, requestMethod: string, body: any = null, forceCache: boolean = true): Promise<any> {

        let request: any = { method: requestMethod };
        if (body !== null) {
            request.body = json(body);
        }

        const querystringIndex = url.indexOf("?");
        const hasQuerystring = querystringIndex !== -1;

        if (requestMethod === 'GET') {

            if (hasQuerystring) {

                const querystring = url.substring(querystringIndex + 1, url.length);
                const newQuerystring = querystring.split("&").map(queryValue => {
                    let indexOfEquals = queryValue.indexOf("=");

                    const param = queryValue.substring(0, indexOfEquals);
                    const value = queryValue.substring(indexOfEquals + 1, queryValue.length);
                    return param + "=" + encodeURIComponent(value);
                }).join("&");

                url = url.substring(0, querystringIndex + 1) + newQuerystring;
            }
        }

        if (forceCache) {
            if (hasQuerystring) {
                url += "&c=" + +new Date();
            } else {
                url += "?c=" + +new Date();
            }
        }

        return this.httpClientContainer
            .getClient()
            .fetch(url, request)
            .then(response => {

                if (response.status === 204 || response.status === 278 || response.status === 302) {
                    return true;
                }

                if (!response.ok) {
                    throw response.statusText;
                }

                return response.json();
            });
    }

}