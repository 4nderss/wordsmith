import {inject, TaskQueue} from 'aurelia-framework';
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



    static inject = [SentenceService, TaskQueue]
    constructor(public sentenceService: SentenceService, public taskQueue: TaskQueue) {

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

            this.taskQueue.queueTask(() => {
                //Handeheld scroll bug
                if(/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent) ) {
                    location.hash = "#tryit";
                }

            });
            


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
