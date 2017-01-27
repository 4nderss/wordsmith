import {inject} from "aurelia-framework";
import {ClientContainer} from "./clientcontainer";
import {BaseService} from "./baseservice";
import {StatisticsModel} from "../models/app/statisticsmodel";

export class SentenceService extends BaseService {

    static inject = [ClientContainer]
    constructor(clientContainer: ClientContainer) {
        super(clientContainer);
    }

    getTransformedSentence(word: string): Promise<string> {
        return this.get("sentence?input=" + word, (result) => {
            return result;
        });
    }

    getStatistics(): Promise<StatisticsModel> {
        return this.get("statistics", (result) => {
            if (result) {
               return StatisticsModel.ConvertFromServiceModel(result);
            };
        });
    }
}