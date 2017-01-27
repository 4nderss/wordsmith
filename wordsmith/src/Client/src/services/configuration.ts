import {HttpClient, HttpClientConfiguration} from 'aurelia-fetch-client';
import {IApplicationSettings} from "../models/app/iapplicationsettings";

export function getHttpConfig(config: HttpClientConfiguration, settings: IApplicationSettings): HttpClientConfiguration {

    let configChain = config
        .useStandardConfiguration()
        .withBaseUrl(settings.serviceUrl)
        .withDefaults({
            credentials: 'same-origin',
            headers: {
                'Accept': 'application/json',
                'X-Requested-With': 'Fetch'
            }
        });

    configChain.withInterceptor({
        request(request) {

            request.headers.append("Cache-control", "private, max-age=0, no-cache");          

            return request;
        },
        response(response) {         

            return response;
        },
        responseError(err: Response) {

            try {             


                let error = err.statusText;

                err.json().then(parsedError => {
                    //Do something with the structured error

                }).catch((err) => {

                });

            } catch (err) {
             
            }


            return err; // you can return a modified Response
        }
    });


    return configChain;



}