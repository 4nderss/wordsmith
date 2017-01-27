import {json} from 'aurelia-fetch-client'
import {ClientContainer} from "./clientcontainer";

export class QueryParams {
    param: string;
    value: string;
}

export class BaseService {


    constructor(public httpClientContainer: ClientContainer) {
        
    }

    private buildQueryString(params: QueryParams[]) : string {

        let querystring = "";
        if (params && params.length > 0) {
            for (let i = 0; i < params.length; i++) {
                let queryParam = i == 0 ? '?' : '&';
                queryParam += params[i].param + "=" + encodeURIComponent(params[i].value);
                querystring += queryParam;
            }
        }
        return querystring;
    }

    protected get<T>(url: string, useCache: boolean, responseHandler: Function, ...queryParams: QueryParams[]): Promise<T> {


        let queryString = this.buildQueryString(queryParams);


        return this
            .sendRequest(url + queryString, 'GET', null, useCache, queryString)
            .then(response => { return responseHandler(response); });
    }


    protected post<T>(url: string, global: boolean = false, body: any = null, globalErrorHandling: boolean = true, ...queryParams: QueryParams[]): Promise<any> {
        let queryString = this.buildQueryString(queryParams);

        return this.sendRequest(url + queryString, 'POST', body, globalErrorHandling, queryString);
    }


    
    private sendRequest(url: string, requestMethod: string, body: any = null, forceCache: boolean = true, queryString: string): Promise<any> {

        let request: any = { method: requestMethod };
        if (body !== null) {
            request.body = json(body);
        }

        const querystringIndex = url.indexOf("?");
        const hasQuerystring = querystringIndex !== -1;
               

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