import {IApplicationSettings} from "./models/app/iapplicationsettings";


export class ApplicationSettings implements IApplicationSettings {
    public serviceUrl: string;
    private ENV: any = process.env.NODE_ENV.toLowerCase();

    constructor() {
        if (this.ENV == 'development') {
            this.serviceUrl = "http://localhost:5000/api/";
        }

        if (this.ENV == 'production') {
            this.serviceUrl = "https://service20170126123202.azurewebsites.net/api/";
        }
    }
}
