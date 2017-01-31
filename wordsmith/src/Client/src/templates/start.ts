import {inject} from 'aurelia-framework';
import {SentenceService} from "../services/sentenceservice";
import {StatisticsModel} from "../models/app/statisticsmodel";

export class Start {

    input: string = "";
    output: string = "";
    

    isLoading: boolean = false;
    isFaulted: boolean = false;

    statistics: StatisticsModel = null;
    statisticsFaulted: boolean = false;
    statisticsLoading: boolean = false;

    static inject = [SentenceService]
    constructor(public sentenceService: SentenceService) {

    }

    activate() {
        this.statisticsLoading = true;

        this.sentenceService.getStatistics().then(statistics => {
            this.statistics = statistics;
            this.statisticsLoading = false;

        }).catch(() => {
            this.statisticsLoading = false;
            this.statisticsFaulted = true;
        });
    }
    
    
    transformSentence() {
        this.isLoading =  true;
        this.isFaulted = false;

        this.sentenceService.getTransformedSentence(this.input).then(transformedSentence => {
            this.output = transformedSentence;
            this.isLoading = false;
        }).catch(err => {
            this.isLoading = false;
            this.isFaulted = true;
        });
    }

    resetSentence() {
        this.isFaulted = false;
        this.output = "";
    }
}
