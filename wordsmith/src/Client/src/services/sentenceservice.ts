import {inject} from "aurelia-framework";
import {ClientContainer} from "./clientcontainer";
import {BaseService, QueryParams} from "./baseservice";
import {StatisticsModel} from "../models/app/statisticsmodel";


export class SentenceService extends BaseService {

    static inject = [ClientContainer]
    constructor(clientContainer: ClientContainer) {
        super(clientContainer);
    }

    getTransformedSentence(word: string): Promise<string> {
        var sentenceParam = new QueryParams();
        sentenceParam.param = "input";
        sentenceParam.value = word;



        return this.get("sentence", false, (result) => {
            return result;
        }, sentenceParam);
    }

    getStatistics(): Promise<StatisticsModel> {
        return this.get("statistics", false, (result) => {
            if (result) {
                return StatisticsModel.ConvertFromServiceModel(result);
            };
        });
    }
}